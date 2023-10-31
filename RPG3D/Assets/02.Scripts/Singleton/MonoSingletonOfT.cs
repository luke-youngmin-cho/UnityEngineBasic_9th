using System;
using System.Reflection;
using UnityEngine;

namespace RPG.Singleton
{
	public class MonoSingleton<T> : MonoBehaviour
		where T : MonoSingleton<T>
	{
		public static T instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<T>();
				}
				return _instance;
			}
		}
		private static T _instance;

		protected virtual void Awake()
		{
			_instance = (T)this;
		}
	}
}