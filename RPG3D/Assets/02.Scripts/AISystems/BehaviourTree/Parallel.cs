namespace RPG.AISystems.BehaviourTree
{
	/// <summary>
	/// 자식들을 차례대로 모두 순회하고, 정책에 따라 결과 취합후 반환
	/// </summary>
	public class Parallel : Composite
	{
		public enum Policy
		{
			RequireOne,
			RequireAll,
		}
		private Policy _successPolicy;
		private int successCount;

		public Parallel(BlackBoard blackBoard, Policy successPolicy)
			: base(blackBoard)
		{
			_successPolicy = successPolicy;
		}

		public override Result Invoke()
		{
			Result result = Result.Failure;

			for (int i = 0; i < children.Count; i++)
			{
				if (i < currentIndex)
					continue;

				UnityEngine.Debug.Log($"[Tree] : Invoking ... {children[i]}");

				result = children[i].Invoke();

				UnityEngine.Debug.Log($"[Tree] : Invoked ... {children[i]}, result : {result}");

				switch (result)
				{
					case Result.Failure:
						break;
					case Result.Success:
						successCount++;
						break;
					case Result.Running:
						owner.stack.Push(children[i]);
						return Result.Running;
					default:
						break;
				}
				currentIndex++;
			}

			currentIndex = 0;
			successCount = 0;
			switch (_successPolicy)
			{
				case Policy.RequireOne:
					return successCount > 0 ? Result.Success : Result.Failure;
				case Policy.RequireAll:
					return successCount > children.Count ? Result.Success : Result.Failure;
				default:
					throw new System.Exception();
			}
		}
	}
}