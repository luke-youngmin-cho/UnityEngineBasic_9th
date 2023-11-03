using RPG.Singleton;
using RPG.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.EventSystems
{
	public class InputManager : MonoSingleton<InputManager>
	{
		public class Map
		{
			private Dictionary<KeyCode, Action> _keyDownActions = new Dictionary<KeyCode, Action>();
			private Dictionary<KeyCode, Action> _keyPressActions = new Dictionary<KeyCode, Action>();
			private Dictionary<KeyCode, Action> _keyUpActions = new Dictionary<KeyCode, Action>();
			private Dictionary<string, Action<float>> _axisActions = new Dictionary<string, Action<float>>();

			public void DoActions()
			{
				foreach (var item in _keyDownActions)
				{
					if (Input.GetKeyDown(item.Key))
						item.Value.Invoke();
				}

				foreach (var item in _keyPressActions)
				{
					if (Input.GetKey(item.Key))
						item.Value.Invoke();
				}

				foreach (var item in _keyUpActions)
				{
					if (Input.GetKeyUp(item.Key))
						item.Value.Invoke();
				}

				foreach (var item in _axisActions)
				{
					item.Value.Invoke(Input.GetAxis(item.Key));
				}
			}
		}

		public Dictionary<string, Map> _keyMaps = new Dictionary<string, Map>();
		public string current;

		private void Update()
		{
			_keyMaps[current].DoActions();
		}
	}
}