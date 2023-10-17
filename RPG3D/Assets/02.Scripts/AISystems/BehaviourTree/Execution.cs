using System;

namespace RPG.AISystems.BehaviourTree
{
	public class Execution : Node
	{
		private Func<Result> _execute;

		public Execution(BlackBoard blackBoard, Func<Result> execute)
			: base(blackBoard)
		{
			_execute = execute;
		}

		public override Result Invoke()
		{
			return _execute.Invoke();
		}
	}
}