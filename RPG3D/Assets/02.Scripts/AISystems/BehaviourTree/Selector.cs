using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.AISystems.BehaviourTree
{
	/// <summary>
	/// �ڽĵ��� ���ʴ�� ��ȸ�ϸ鼭 �ڽ��� Success/Running ��ȯ�ϸ�
	/// �ش� ��� ��ȯ. ��� ���н� Failure ��ȯ.
	/// </summary>
	public class Selector : Composite
	{
		public Selector(BlackBoard blackBoard) : base(blackBoard)
		{
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
						currentIndex++;
						break;
					case Result.Success:
						currentIndex = 0;
						return Result.Success;
					case Result.Running:
						owner.stack.Push(children[i]);
						return Result.Running;
					default:
						break;
				}
			}

			currentIndex = 0;
			return result;
		}
	}
}