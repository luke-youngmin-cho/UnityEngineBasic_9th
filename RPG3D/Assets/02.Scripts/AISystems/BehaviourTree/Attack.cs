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

			// 다음 Transition 목표가 Attack 이 아닐경우
			if (controller.next != State.Attack)
			{
				// Attack 상태에서 다른 상태로 Transition 을 하려는 것이므로 
				// Attack 을 완료하고 종료되었으므로 성공반환
				if (controller.IsInState(State.Attack))
				{
					return Result.Success;
				}
				// Attack 을 실행한 적이 없으므로 Attack 으로 상태전환
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