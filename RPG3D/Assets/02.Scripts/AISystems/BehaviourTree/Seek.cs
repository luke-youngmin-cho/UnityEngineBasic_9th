using UnityEngine;
using System.Linq;
using UnityEngine.AI;

namespace RPG.AISystems.BehaviourTree
{
	public class Seek : Node
	{
		private float _radius;
		private float _angle;
		private LayerMask _targetMask;
		private Vector3 _offset;

		public Seek(BlackBoard blackBoard,
					float radius,
					float angle,
					LayerMask targetMask,
					Vector3 offset) 
			: base(blackBoard)
		{
			_radius = radius;
			_angle = angle;
			_targetMask = targetMask;
			_offset = offset;
		}

		public override Result Invoke()
		{
			if (blackBoard.target != null)
			{
				float distance = Vector3.Distance(blackBoard.target.position,
												  blackBoard.agent.nextPosition);
				if (distance <= blackBoard.agent.stoppingDistance)
				{
					return Result.Success;
				}
				else
				{
					if (distance > _radius)
					{
						blackBoard.target = null;
						blackBoard.agent.SetDestination(blackBoard.transform.position);
						return Result.Failure;
					}
					else
					{
						blackBoard.agent.isStopped = false;
						blackBoard.agent.SetDestination(blackBoard.target.position);
						return Result.Running;
					}
				}
			}

			Collider[] cols =
				Physics.OverlapCapsule(blackBoard.transform.position,
									   blackBoard.transform.position + _offset,
									   _radius,
									   _targetMask);

			Collider col = cols.FirstOrDefault(x => IsInSight(x.transform.position));

			if (col)
			{
				if (NavMesh.SamplePosition(col.transform.position,
										   out NavMeshHit hit,
										   float.PositiveInfinity,
										   NavMesh.AllAreas))
				{
					blackBoard.target = col.transform;
					blackBoard.agent.SetDestination(col.transform.position);
					return Result.Success;
				}
			}

			blackBoard.target = null;
			blackBoard.agent.isStopped = true;
			return Result.Failure;
		}

		private bool IsInSight(Vector3 target)
		{
			Vector3 origin = blackBoard.transform.position;
			Vector3 forward = blackBoard.transform.forward;
			Vector3 lookDir = (target - origin).normalized;
			float theta = Mathf.Acos(Vector3.Dot(forward, lookDir)) * Mathf.Rad2Deg;
			if (theta < _angle / 2.0f)
			{
				if (Physics.Raycast(origin, 
									lookDir, 
									out RaycastHit hit,
									Vector3.Distance(origin, target),
									_targetMask))
				{
					return true;
				}
			}
			return false;
		}

	}
}