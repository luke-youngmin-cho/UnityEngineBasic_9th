using System;

namespace RPG.AISystems.BehaviourTree
{
	public class Attack : Node
	{
		public Attack(BlackBoard blackBoard) : base(blackBoard)
		{
		}

		public override Result Invoke()
		{
			CharacterController controller = blackBoard.controller;

			// ���� Transition ��ǥ�� Attack �� �ƴҰ��
			if (controller.next != State.Attack)
			{
				// Attack ���¿��� �ٸ� ���·� Transition �� �Ϸ��� ���̹Ƿ� 
				// Attack �� �Ϸ��ϰ� ����Ǿ����Ƿ� ������ȯ
				if (controller.IsInState(State.Attack))
				{
					return Result.Success;
				}
				// Attack �� ������ ���� �����Ƿ� Attack ���� ������ȯ
				else
				{
					controller.ChangeState(State.Attack);
				}
			}

			blackBoard.agent.isStopped = true;
			return Result.Running;
		}
	}
}