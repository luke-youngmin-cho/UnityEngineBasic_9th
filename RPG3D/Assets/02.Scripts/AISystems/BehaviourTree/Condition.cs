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
				//owner.stack.Push(child);
				//return Result.Success;
				UnityEngine.Debug.Log($"[Tree] : Invoking ... {child}");
				Result result = child.Invoke();
				UnityEngine.Debug.Log($"[Tree] : Invoked ... {child}, result : {result}");
				return result;
			}

			return Result.Failure;
		}
	}
}