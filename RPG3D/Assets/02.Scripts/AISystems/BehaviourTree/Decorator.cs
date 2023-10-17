using System;

namespace RPG.AISystems.BehaviourTree
{
	public abstract class Decorator : Node, IParentOfChild
	{
		public Node child { get; set; }

		protected Decorator(BlackBoard blackBoard) : base(blackBoard)
		{
		}

		public override Result Invoke()
		{
			return Decorate();
		}

		public abstract Result Decorate();
	}
}