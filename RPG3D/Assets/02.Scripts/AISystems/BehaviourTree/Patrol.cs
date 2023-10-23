using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace RPG.AISystems.BehaviourTree
{
	public class Patrol : Node
	{
		private float _radius;
		private float _periodMin;
		private float _periodMax;
		private float _period;
		private float _timeMark;
		private int _behaviour; // -1 : reset, 0 : rest, 1 : move

		public Patrol(BlackBoard blackBoard, float radius, float periodMin, float periodMax) : base(blackBoard)
		{
			_radius = radius;
			_periodMin = periodMin;
			_periodMax = periodMax;
			_behaviour = -1;
		}

		public override Result Invoke()
		{
			CharacterController controller = blackBoard.controller;

			// Move 상태일때만 Patrol 가능
			if (controller.next != State.Move)
			{
				return Result.Failure;
			}
			else if (controller.IsInState(State.Move))
			{
				switch (_behaviour)
				{
					case -1:
						{
							_timeMark = Time.time;
							_behaviour = Random.Range(0, 2);
							_period = Random.Range(_periodMin, _periodMax);
							if (_behaviour == 1)
							{
								Vector3 expected = blackBoard.transform.position
												+ new Vector3(Random.Range(0.0f, _radius), 0.0f, Random.Range(0.0f, _radius));

								if (NavMesh.SamplePosition(expected, out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas))
								{
									blackBoard.agent.SetDestination(hit.position);
								}
								else
								{
									_behaviour = 0;
								}
							}
						}
						break;
					case 0:
					case 1:
						if (Time.time - _timeMark > _period)
						{
							_behaviour = -1;
							_period = 0.0f;
							_timeMark = 0.0f;
							return Result.Success;
						}
						break;
					default:
						break;
				}
			}

			return Result.Running;
		}
	}
}