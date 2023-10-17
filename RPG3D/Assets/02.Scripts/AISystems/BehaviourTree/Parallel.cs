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

		public Parallel(BlackBoard blackBoard, Policy successPolicy)
			: base(blackBoard)
		{
			_successPolicy = successPolicy;
		}

		public override Result Invoke()
		{
			int successCount = 0;
			Result result = Result.Failure;
			foreach (var child in children)
			{
				result = child.Invoke();
				if (result == Result.Success)
					successCount++;
			}

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