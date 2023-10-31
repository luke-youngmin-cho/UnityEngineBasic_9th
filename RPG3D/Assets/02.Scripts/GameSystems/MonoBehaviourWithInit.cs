using UnityEngine;

namespace RPG.GameSystems
{
	public abstract class MonoBehaviourWithInit : MonoBehaviour, IInitializable
	{
		public abstract void Init();
	}
}