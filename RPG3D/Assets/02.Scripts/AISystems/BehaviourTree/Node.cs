namespace RPG.AISystems.BehaviourTree
{
	public enum Result
	{
		Failure,
		Success,
		Running,
	}

	public abstract class Node
	{
		protected Tree owner;
		protected BlackBoard blackBoard;

		public Node(BlackBoard blackBoard)
		{
			this.owner = blackBoard.owner;
			this.blackBoard = blackBoard;
		}

		public abstract Result Invoke();
	}
}