using System.Collections.Generic;

namespace RPG.AISystems.BehaviourTree
{
	public interface IParentOfChildren
	{
		List<Node> children { get; set; }
	}
}