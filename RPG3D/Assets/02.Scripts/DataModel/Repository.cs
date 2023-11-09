using RPG.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace RPG.DataModel
{
	public class Repository : Singleton<Repository>
	{
		private Dictionary<Type, IDataModel> _mapper = new Dictionary<Type, IDataModel>();

		public T Get<T>()
			where T : IDataModel
		{
			Type type = typeof(T);
			if (_mapper.ContainsKey(type))
			{
				return (T)_mapper[type];
			}
			else
			{
				T model = Load<T>();
				_mapper.Add(type, model);
				return model;
			}
		}

		public T Load<T>()
			where T : IDataModel
		{
			string path = $"{Application.persistentDataPath}/{nameof(T)}.json";

			T dataModel;

			if (File.Exists(path))
			{
				dataModel = JsonUtility.FromJson<T>(File.ReadAllText(path));
			}
			else
			{
				dataModel = Activator.CreateInstance<T>();
				dataModel.SetDefaultItems();
				Save<T>();
			}

			if (_mapper.TryAdd(typeof(T), dataModel))
				_mapper[typeof(T)] = dataModel;

			return dataModel;
		}

		public void Save<T>()
			where T : IDataModel
		{
			string path = $"{Application.persistentDataPath}/{nameof(T)}.json";
			File.WriteAllText(path, JsonUtility.ToJson(_mapper[typeof(T)]));
		}
	}
}