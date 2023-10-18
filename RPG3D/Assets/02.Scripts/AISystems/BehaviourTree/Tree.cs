using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.AISystems.BehaviourTree
{
	public class Tree : MonoBehaviour
	{
		public BlackBoard blackBoard;
		public Root _root;
		public Stack<Node> stack;
		private bool _isTicking;

		private void Awake()
		{
			blackBoard = new BlackBoard(this);
			_root = new Root(blackBoard);
			stack = new Stack<Node>();
		}

		private void Update()
		{
			if (_isTicking == false)
			{
				_isTicking = true;
				StartCoroutine(Tick());
			}
		}

		private IEnumerator Tick()
		{
			stack.Push(_root);

			while (stack.Count > 0)
			{
				Node current = stack.Pop();

				Debug.Log($"[Tree] : Invoking ... {current}");

				Result result = current.Invoke();

				Debug.Log($"[Tree] : Invoked ... {current} , result : {result}");

				if (result == Result.Running)
				{
					stack.Push(current);
					yield return null;
				}
			}

			_isTicking = false;
		}
	}
}