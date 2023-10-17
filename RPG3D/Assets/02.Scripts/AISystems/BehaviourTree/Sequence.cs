namespace RPG.AISystems.BehaviourTree
{
	/// <summary>
	/// �ڽĵ��� ���ʴ�� ��ȸ�ϸ鼭 �ڽ��� Failure/Running ��ȯ�ϸ�
	/// �ش� ��� ��ȯ. ��� ������ Success ��ȯ
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