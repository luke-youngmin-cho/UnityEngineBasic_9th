using System;
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


		#region Build
		private Node _current;
		private Stack<Composite> _compositeStack;

		public Tree StartBuild()
		{
			_root = new Root(blackBoard);
			_current = _root;
			_compositeStack = new Stack<Composite>();
			return this;
		}

		public Tree Selector()
		{
			Composite node = new Selector(blackBoard);
			Attach(_current, node);
			_compositeStack.Push(node);
			_current = node;
			return this;
		}

		public Tree Sequence()
		{
			Composite node = new Sequence(blackBoard);
			Attach(_current, node);
			_compositeStack.Push(node);
			_current = node;
			return this;
		}

		public Tree Parallel(Parallel.Policy successPolicy)
		{
			Composite node = new Parallel(blackBoard, successPolicy);
			Attach(_current, node);
			_compositeStack.Push(node);
			_current = node;
			return this;
		}

		public Tree Condition(Func<bool> func)
		{
			Node node = new Condition(blackBoard, func);
			Attach(_current, node);
			_current = node;
			return this;
		}

		public Tree Execution(Func<Result> func)
		{
			Node node = new Execution(blackBoard, func);
			Attach(_current, node);
			_current = _compositeStack.Count > 0 ? _compositeStack.Peek() : null;
			return this;
		}

		public Tree Seek(float radius, float angle, LayerMask targetMask, Vector3 offset)
		{
			Node node = new Seek(blackBoard, radius, angle, targetMask, offset);
			Attach(_current, node);
			_current = _compositeStack.Count > 0 ? _compositeStack.Peek() : null;
			return this;
		}
		public Tree Attack()
		{
			Node node = new Attack(blackBoard);
			Attach(_current, node);
			_current = _compositeStack.Count > 0 ? _compositeStack.Peek() : null;
			return this;
		}

		public void Attach(Node parent, Node child)
		{
			if (parent is IParentOfChild)
			{
				((IParentOfChild)parent).child = child;
			}
			else if (parent is IParentOfChildren)
			{
				((IParentOfChildren)parent).children.Add(child);
			}
			else
			{
				throw new System.Exception($"[Tree] : You cannot attach child to {parent.GetType()}");
			}
		}

		public Tree ExitCurrentComposite()
		{
			if (_compositeStack.Count > 0)
			{
				_compositeStack.Pop();
				_current = _compositeStack.Count > 0 ? _compositeStack.Peek() : null;
			}
			else
			{
				throw new Exception($"[Tree] : Failed to exit composite. stack is empty");
			}
			return this;
		}


		#endregion
	}
}