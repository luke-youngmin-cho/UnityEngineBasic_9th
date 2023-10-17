using System;

namespace RPG.AISystems.BehaviourTree
{
	public class Condition : Node, IParentOfChild
	{
		public Node child { get; set; }
		private Func<bool> _func;

		public Condition(BlackBoard blackBoard, Func<bool> func)
			: base(blackBoard)
		{
			_func = func;
		}

		public override Result Invoke()
		{
			if (_func.Invoke())
			{
				return Result.Success;
			}

			return Result.Failure;
		}
	}
}