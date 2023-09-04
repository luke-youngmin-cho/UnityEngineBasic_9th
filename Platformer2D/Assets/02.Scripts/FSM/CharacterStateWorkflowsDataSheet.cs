using System.Collections.Generic;
using UnityEngine;

public static class CharacterStateWorkflowsDataSheet
{
    public abstract class WorkflowBase : IWorkflow<State>
    {
        public abstract State ID { get; }

        public int Current => current;

        public virtual bool CanExecute => true;

        protected int current;

        protected CharacterMachine machine;
        protected Transform transform;
        protected Rigidbody2D rigidbody;
        protected CapsuleCollider2D[] colliders;
        protected Animator animator;

        public WorkflowBase(CharacterMachine machine)
        {
            this.machine = machine;
            this.transform = machine.transform;
            this.animator = machine.animator;
            this.rigidbody = machine.GetComponent<Rigidbody2D>();
            this.colliders = machine.GetComponentsInChildren<CapsuleCollider2D>();
        }

        public abstract State MoveNext();

        public void Reset()
        {
            current = 0;
        }

        public virtual void OnEnter() { Reset(); }
        public virtual void OnExit() { }
    }

    public class Idle : WorkflowBase
    {
        public override State ID => State.Idle;

        public Idle(CharacterMachine machine) : base(machine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.hasJumped = false;
            machine.hasSecondJumped = false;
            machine.isDirectionChangeable = true;
            machine.isMovable = true;
            animator.Play("Idle");
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                default:
                    {
                        // X 축 입력 절댓값이 0보다 크면 next = State.Move
                        if (Mathf.Abs(machine.horizontal) > 0)
                            next = State.Move;
                        // Ground 가 감지되지 않으면 next = State.Fall
                        if (machine.isGrounded == false)
                            next = State.Fall;
                    }
                    break;
            }

            return next;
        }
    }

    public class Move : WorkflowBase
    {
        public override State ID => State.Move;

        public Move(CharacterMachine machine) : base(machine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.isDirectionChangeable = true;
            machine.isMovable = true;
            animator.Play("Move");
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                default:
                    {
                        // X 축 입력 절댓값이 0이면 next = State.Idle
                        if (machine.horizontal == 0.0f)
                            next = State.Idle;
                        // Ground 가 감지되지 않으면 next = State.Fall
                        if (machine.isGrounded == false)
                            next = State.Fall;
                    }
                    break;
            }

            return next;
        }
    }

    public class Jump : WorkflowBase
    {
        public override State ID => State.Jump;
        public override bool CanExecute => base.CanExecute &&
                                           machine.hasJumped == false &&
                                           (machine.current == State.Idle ||
                                            machine.current == State.Move) &&
                                            machine.isGrounded;

        private float _force;

        public Jump(CharacterMachine machine, float force) : base(machine)
        {
            _force = force;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.hasJumped = true;
            machine.isDirectionChangeable = true;
            machine.isMovable = false;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            animator.Play("Jump");
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                default:
                    {
                        if (rigidbody.velocity.y <= 0.0f)
                        {
                            next = machine.isGrounded ? State.Idle : State.Fall;
                        }
                    }
                    break;
            }

            return next;
        }
    }
    public class SecondJump : WorkflowBase
    {
        public override State ID => State.SecondJump;
        public override bool CanExecute => base.CanExecute &&
                                           machine.hasSecondJumped == false &&
                                           (machine.current == State.Jump ||
                                            machine.current == State.Fall) &&
                                            machine.isGrounded == false;

        private float _force;

        public SecondJump(CharacterMachine machine, float force) : base(machine)
        {
            _force = force;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.hasSecondJumped = true;
            machine.isDirectionChangeable = true;
            machine.isMovable = false;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            animator.Play("SecondJump");
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                default:
                    {
                        if (rigidbody.velocity.y <= 0.0f)
                        {
                            next = machine.isGrounded ? State.Idle : State.Fall;
                        }
                    }
                    break;
            }

            return next;
        }
    }
    public class Fall : WorkflowBase
    {
        public override State ID => State.Fall;
        private float _landingDistance;
        private float _startPosY;

        public Fall(CharacterMachine machine, float landingDistance) : base(machine)
        {
            _landingDistance = landingDistance;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.isDirectionChangeable = true;
            machine.isMovable = false;
            _startPosY = rigidbody.position.y;
            animator.Play("Fall");
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                default:
                    {
                        if (machine.isGrounded)
                        {
                            next = (_startPosY - rigidbody.position.y) < _landingDistance ? State.Idle : State.Land;
                        }
                    }
                    break;
            }

            return next;
        }

    }

    public class Land : WorkflowBase
    {
        public override State ID => State.Land;

        public Land(CharacterMachine machine) : base(machine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.isDirectionChangeable = true;
            machine.isMovable = false;
            machine.move = Vector2.zero;
            rigidbody.velocity = Vector2.zero;
            animator.Play("Land");
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                default:
                    {
                        // 현재 애니메이터의 재생중인 상태의 정보에서 일반화된 시간이 1.0f 이된다 
                        // == 현재 상태 애니메이션 클립 재생 끝났다 
                        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        {
                            next = State.Idle;
                        }
                    }
                    break;
            }

            return next;
        }
    }

    public class Crouch : WorkflowBase
    {
        public override State ID => State.Crouch;

        public override bool CanExecute => base.CanExecute &&
                                           (machine.current == State.Idle ||
                                            machine.current == State.Move) && 
                                           machine.isGrounded;

        private Vector2 _offsetCrouched;
        private Vector2 _sizeCrouched;
        private Vector2 _offsetOrigin;
        private Vector2 _sizeOrigin;

        public Crouch(CharacterMachine machine, Vector2 offsetCrouched, Vector2 sizeCrouched) : base(machine)
        {
            _offsetCrouched = offsetCrouched;
            _sizeCrouched = sizeCrouched;
            _offsetOrigin = colliders[0].offset;
            _sizeOrigin = colliders[0].size;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            machine.isDirectionChangeable = true;
            machine.isMovable = false;
            machine.move = Vector2.zero;
            rigidbody.velocity = Vector2.zero;
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].offset = _offsetCrouched;
                colliders[i].size = _sizeCrouched;
            }
            animator.Play("Crouch");
        }

        public override void OnExit()
        {
            base.OnExit();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].offset = _offsetOrigin;
                colliders[i].size = _sizeOrigin;
            }
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                case 0:
                    { 
                        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        {
                            animator.Play("CrouchIdle");
                            current++;
                        }
                    }
                    break;
                default:
                    {
                        if (machine.isGrounded == false)
                        {
                            next = State.Fall;
                        }
                    }
                    break;
            }

            return next;
        }
    }

    public static IEnumerable<KeyValuePair<State, IWorkflow<State>>> GetWorkflowsForPlayer(CharacterMachine machine)
    {
        return new Dictionary<State, IWorkflow<State>>()
        {
            { State.Idle, new Idle(machine) },
            { State.Move, new Move(machine) },
            { State.Jump, new Jump(machine, 3.0f) },
            { State.SecondJump, new SecondJump(machine, 3.0f) },
            { State.Fall, new Fall(machine, 1.0f) },
            { State.Land, new Land(machine) },
            { State.Crouch, new Crouch(machine, new Vector2(0.0f, 0.06f), new Vector2(0.12f, 0.12f )) },
        };
    }

}