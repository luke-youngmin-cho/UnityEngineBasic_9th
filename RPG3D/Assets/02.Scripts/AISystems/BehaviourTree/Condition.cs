using System;

namespace RPG.AISystems.BehaviourTree
{
	public class Condition : Node, IParentOfChild
	{
		public Node child { get; set; }
		private Func<bool> _func;

		public Condition(Func<bool> func)
		{
			_func = func;
		}

		public override Result Invoke()
		{
			if (_func.Invoke())
				return child.Invoke();

			return Result.Failure;
		}
	}
}