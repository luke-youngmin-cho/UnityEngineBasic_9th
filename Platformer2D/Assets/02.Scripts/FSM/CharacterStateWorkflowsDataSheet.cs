using System.Collections.Generic;
using UnityEngine;

public static class CharacterStateWorkflowsDataSheet
{
    public abstract class WorkflowBase : IWorkflow<State>
    {
        public abstract State ID { get; }

        public int Current => current;
        protected int current;

        protected CharacterMachine machine;
        protected Transform transform;
        protected Rigidbody2D rigidbody;
        protected CapsuleCollider2D collider;

        public WorkflowBase(CharacterMachine machine)
        {
            this.machine = machine;
            this.transform = machine.transform;
            this.rigidbody = machine.GetComponent<Rigidbody2D>();
            this.collider = machine.GetComponent<CapsuleCollider2D>();
        }

        public abstract State MoveNext();

        public void Reset()
        {
            current = 0;
        }
    }

    public class Idle : WorkflowBase
    {
        public override State ID => State.Idle;

        public Idle(CharacterMachine machine) : base(machine)
        {
        }

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                case 0:
                    {
                        machine.isDirectionChangeable = true;
                        machine.isMovable = true;
                        current++;
                    }
                    break;
                default:
                    {
                        // X �� �Է� ������ 0���� ũ�� next = State.Move
                        if (Mathf.Abs(machine.horizontal) > 0)
                            next = State.Move;
                        // todo -> Ground �� �������� ������ next = State.Fall
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

        public override State MoveNext()
        {
            State next = ID;

            switch (current)
            {
                case 0:
                    {
                        machine.isDirectionChangeable = true;
                        machine.isMovable = true;
                        current++;
                    }
                    break;
                default:
                    {
                        // X �� �Է� ������ 0�̸� next = State.Idle
                        if (machine.horizontal == 0.0f)
                            next = State.Idle;
                        // todo -> Ground �� �������� ������ next = State.Fall
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
        };
    }

}