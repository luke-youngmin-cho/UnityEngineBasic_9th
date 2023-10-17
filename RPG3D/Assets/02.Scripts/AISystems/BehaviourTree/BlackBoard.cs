using UnityEngine;
using UnityEngine.AI;

namespace RPG.AISystems.BehaviourTree
{
	public class BlackBoard
	{
		// Owner
		public Tree owner;
		public Transform transform;
		public NavMeshAgent agent;
		public CharacterController controller;

		// Target
		public Transform target;

		public BlackBoard(Tree owner)
		{
			this.owner = owner;
			this.transform = owner.transform;
			this.agent = owner.GetComponent<NavMeshAgent>();
			this.controller = owner.GetComponent<CharacterController>();
		}
	}
}