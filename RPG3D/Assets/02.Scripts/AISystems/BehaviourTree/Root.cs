namespace RPG.AISystems.BehaviourTree
{
	public class Root : Node, IParentOfChild
	{
		public Node child { get; set; }

		public Root(BlackBoard blackBoard) : base(blackBoard)
		{
		}

		public override Result Invoke()
		{
			owner.stack.Push(child);
			return Result.Success;
		}
	}
}