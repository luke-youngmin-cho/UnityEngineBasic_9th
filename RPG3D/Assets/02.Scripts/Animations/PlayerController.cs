using UnityEngine;

public class PlayerController : CharacterController
{
    public override float horizontal => Input.GetAxis("Horizontal");

    public override float vertical => Input.GetAxis("Vertical");

    public override float moveGain => Input.GetKey(KeyCode.LeftShift) ? 3.0f : 1.0f;

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            if (isComboAvailable)
                ChangeStateForcely(State.Attack);
            else if (comboMax == 0)
                ChangeState(State.Attack);
        }
    }
}