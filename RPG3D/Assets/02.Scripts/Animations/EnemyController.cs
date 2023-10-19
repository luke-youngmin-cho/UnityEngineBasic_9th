using RPG.AISystems.BehaviourTree;
using Tree = RPG.AISystems.BehaviourTree.Tree;
using UnityEngine;

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

		aiTree.StartBuild()
				.Selector()
					.Execution(() => Result.Failure)
					.Sequence()
						.Execution(() => Result.Success)
						.Condition(() => true)
							.Execution(() => Result.Success)
					.ExitCurrentComposite()
					.Execution(() => Result.Success);
					
	}
}