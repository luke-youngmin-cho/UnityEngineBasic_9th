using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatum : MonoBehaviour
{
	public static ItemDatum instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Resources.Load<ItemDatum>("ItemDatum");
				_instance.Init();
			}
			return _instance;
		}
	}
	private static ItemDatum _instance;

	public ItemData this[int id] => _dictionary[id];
	private Dictionary<int, ItemData> _dictionary;
	[SerializeField] private List<ItemData> _datum;

	private void Init()
	{
		_dictionary = new Dictionary<int, ItemData>();
		foreach (var data in _datum)
		{
			_dictionary.Add(data.id.value, data);
		}
	}
}
