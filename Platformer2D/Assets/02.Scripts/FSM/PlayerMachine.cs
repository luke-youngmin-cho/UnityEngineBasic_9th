using UnityEngine;

public class PlayerMachine : CharacterMachine
{
    public override float horizontal 
    {
        get => Input.GetAxisRaw("Horizontal");
        set => base.horizontal = value; 
    }

    private void Start()
    {
        Initialize(CharacterStateWorkflowsDataSheet.GetWorkflowsForPlayer(this));
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

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeState(State.Crouch);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (current == State.Crouch)
                ChangeState(State.Idle);
        }
    }
}