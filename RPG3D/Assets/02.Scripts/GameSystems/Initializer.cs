using UnityEngine;

namespace RPG.GameSystems
{
	public class Initializer : MonoBehaviour
	{
		[SerializeField] private MonoBehaviourWithInit[] _targets;

		private void Awake()
		{
			for (int i = 0; i < _targets.Length; i++)
			{
				_targets[i].Init();
			}
		}
	}
}