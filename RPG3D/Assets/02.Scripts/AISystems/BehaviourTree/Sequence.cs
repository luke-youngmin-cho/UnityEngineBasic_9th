namespace RPG.AISystems.BehaviourTree
{
	/// <summary>
	/// 자식들을 차례대로 순회하면서 자식이 Failure/Running 반환하면
	/// 해당 결과 반환. 모두 성공시 Success 반환
	/// </summary>
	public class Sequence : Composite
	{
		public Sequence(BlackBoard blackBoard) : base(blackBoard)
		{
		}

		public override Result Invoke()
		{
			Result result = Result.Success;
			foreach (var child in children)
			{
				result = child.Invoke();
				if (result != Result.Success)
					return result;
			}

			return result;
		}
	}
}