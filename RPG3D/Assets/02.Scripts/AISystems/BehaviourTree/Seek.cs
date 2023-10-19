using UnityEngine;
using System.Linq;

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
			Collider[] cols =
				Physics.OverlapCapsule(blackBoard.transform.position,
									   blackBoard.transform.position + _offset,
									   _radius,
									   _targetMask);

			Collider col = cols.First(x => IsInSight(x.transform.position));

			if (col)
			{
				blackBoard.target = col.transform;
				blackBoard.agent.SetDestination(col.transform.position);
				return Result.Success;
			}
			else
			{
				blackBoard.target = null;
				blackBoard.agent.isStopped = true;
				return Result.Failure;
			}
		}

		private bool IsInSight(Vector3 target)
		{
			Vector3 origin = blackBoard.transform.position;
			Vector3 forward = blackBoard.transform.forward;
			Vector3 lookDir = (target - origin).normalized;
			float theta = Mathf.Acos(Vector3.Dot(forward, lookDir));
			if (theta < _angle)
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