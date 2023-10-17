using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.AISystems.BehaviourTree
{
	public class Tree : MonoBehaviour
	{
		public BlackBoard blackBoard;
		private Root _root;
		public Stack<Node> stack;

		private void Awake()
		{
			blackBoard = new BlackBoard(this);
			_root = new Root(blackBoard);
			stack = new Stack<Node>();
		}


		private IEnumerator Tick()
		{
			stack.Push(_root);

			while (stack.Count > 0)
			{
				Node current = stack.Peek();
				Result result = current.Invoke();

				if (result == Result.Running)
				{
					yield return null;
				}
				else
				{
					stack.Pop();

					if (current is IParentOfChild)
					{
						stack.Push(((IParentOfChild)current).child);
					}
				}
			}
		}
	}
}