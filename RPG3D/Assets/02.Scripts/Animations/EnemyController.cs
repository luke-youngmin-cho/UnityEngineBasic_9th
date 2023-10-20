using RPG.AISystems.BehaviourTree;
using Tree = RPG.AISystems.BehaviourTree.Tree;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Tree))]
public class EnemyController : CharacterController
{
	public override float horizontal => _horizontal;

	public override float vertical => _vertical;

	public override float moveGain => _moveGain;

	private float _horizontal;
	private float _vertical;
	private float _moveGain;
	[HideInInspector] public Tree aiTree;

	[Header("AI")]
	[SerializeField] private float _seekRadius;
	[SerializeField] private float _seekAngle;
	[SerializeField] private LayerMask _seekMask;
	[SerializeField] private Vector3 _seekOffset;


	private void Start()
	{
		//aiTree = GetComponent<Tree>();
		//aiTree._root.child = new Selector(aiTree.blackBoard);
		//((Selector)aiTree._root.child).children.Add(new Execution(aiTree.blackBoard, () => Result.Failure));
		//((Selector)aiTree._root.child).children.Add(new Sequence(aiTree.blackBoard));
		//((Selector)aiTree._root.child).children.Add(new Execution(aiTree.blackBoard, () => Result.Success));
		//((Sequence)((Selector)aiTree._root.child).children[1]).children
		//	.Add(new Execution(aiTree.blackBoard, () => Result.Success));
		//((Sequence)((Selector)aiTree._root.child).children[1]).children
		//	.Add(new Condition(aiTree.blackBoard, () => true));
		//((Condition)((Sequence)((Selector)aiTree._root.child).children[1]).children[1])
		//	.child = new Execution(aiTree.blackBoard, () => Result.Success);

		aiTree = GetComponent<Tree>();
		aiTree.StartBuild()
			.Seek(_seekRadius, _seekAngle, _seekMask, _seekOffset);
	}


	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		DrawArcGizmo(_seekRadius, _seekAngle, Color.yellow);
	}

	private void DrawArcGizmo(float radius, float angle, Color color)
	{
		Gizmos.color = color;

		Vector3 left = Quaternion.Euler(0.0f, -angle / 2.0f, 0.0f) * transform.forward;
		Vector3 right = Quaternion.Euler(0.0f, angle / 2.0f, 0.0f) * transform.forward;

		int segments = 10;
		Vector3 prev = transform.position + left * radius;
		for (int i = 0; i < segments; i++)
		{
			float ratio = (float)(i + 1) / segments;
			float theta = Mathf.Lerp(-angle / 2.0f, angle / 2.0f, ratio);
			Vector3 dir = Quaternion.Euler(0.0f, theta, 0.0f) * transform.forward;
			Vector3 point = transform.position + dir * radius;
			Gizmos.DrawLine(prev, point);
			prev = point;
		}

		Gizmos.DrawLine(transform.position, transform.position + left * radius);
		Gizmos.DrawLine(transform.position, transform.position + right * radius);

	}


}