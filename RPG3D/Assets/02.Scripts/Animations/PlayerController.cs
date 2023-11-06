using RPG.EventSystems;
using UnityEngine;

public class PlayerController : CharacterController
{
	public override float horizontal => _horizontal;
	public override float vertical => _vertical;

    public override float moveGain => Input.GetKey(KeyCode.LeftShift) ? 3.0f : 1.0f;

	private float _horizontal;
	private float _vertical;

	private void Start()
	{
		InputManager.instance.keyMaps["Player"]
			.AddAxisAction("Horizontal", (value) => _horizontal = value);
		InputManager.instance.keyMaps["Player"]
			.AddAxisAction("Vertical", (value) => _vertical = value);
		InputManager.instance.keyMaps["PopUpUI"]
			.AddAxisAction("Horizontal", (value) => _horizontal = value);
		InputManager.instance.keyMaps["PopUpUI"]
			.AddAxisAction("Vertical", (value) => _vertical = value);
	}

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