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
		public abstract Result Invoke();
	}
}