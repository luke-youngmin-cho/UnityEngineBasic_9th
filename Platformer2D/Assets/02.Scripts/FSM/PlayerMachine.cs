using UnityEngine;

public class PlayerMachine : CharacterMachine
{
    public override float horizontal 
    {
        get => Input.GetAxisRaw("Horizontal");
        set => base.horizontal = value; 
    }

    public override float vertical
    {
        get => Input.GetAxisRaw("Vertical");
        set => base.vertical = value; 
    }

    private void Start()
    {
        Initialize(CharacterStateWorkflowsDataSheet.GetWorkflowsForPlayer(this));
        onHpDepleted += (amount) => ChangeState(State.Hurt);
        onHpDepleted += (amount) =>
        {
            isInvincible = true;

            if (hpValue > hpMin)
                Invoke("ResetInvincible", 0.8f);
        };
        onHpMin += () => ChangeState(State.Die);
    }

    private void ResetInvincible()
    {
        isInvincible = false;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (ChangeState(State.JumpDown) == false)
            if (ChangeState(State.Jump) == false)
                ChangeState(State.SecondJump);
        }

        if (Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.LeftArrow))
        {
            ChangeState(State.WallSlide);
        }
        else if (current == State.WallSlide)
        {
            ChangeState(State.Idle);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeState(State.LedgeClimb);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (canLadderUp)
                ChangeState(State.LadderClimbing, new object[] { upLadder, DIRECTION_UP });
            ChangeState(State.Ledge);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (canLadderDown && current == State.Idle && (upLadder != downLadder))
                ChangeState(State.LadderClimbing, new object[] { downLadder, DIRECTION_DOWN });
            else
                ChangeState(State.Crouch);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (canLadderDown && current == State.Idle && (upLadder != downLadder))
                ChangeState(State.LadderClimbing, new object[] { downLadder, DIRECTION_DOWN });
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (current == State.Crouch)
                ChangeState(State.Idle);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeState(State.Attack);
        }
    }
}