using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.DataModel
{
	public struct Pair<T> : IComparable<Pair<T>>, IEquatable<Pair<T>>
		where T : IEquatable<T>
	{
		public int id;
		public T item;

		public Pair(int id, T item)
		{
			this.id = id;
			this.item = item;
		}

		public int CompareTo(Pair<T> other)
		{
			return id.CompareTo(other.id);
		}

		public bool Equals(Pair<T> other)
		{
			return id.Equals(other.id) && item.Equals(other.item);
		}

		public static bool operator ==(Pair<T> op1, Pair<T> op2)
			=> (op1.id == op2.id) && (op1.item.Equals(op2.item));

		public static bool operator !=(Pair<T> op1, Pair<T> op2)
			=> !(op1 == op2);
	}

	public abstract class DataModelBase<T> : IDataModel<T>
		where T : IEquatable<T>
	{
		public IEnumerable<int> itemIDs => _items.Select(pair => pair.id);

		public IEnumerable<T> items => _items.Select(pair => pair.item);

		IEnumerable IDataModel.itemIDs => _items.Select(pair => pair.id);

		IEnumerable IDataModel.items => _items.Select(pair => pair.item);

		private List<Pair<T>> _items;


		public void RequestRead(int itemID, Action<int, T> onSuccess)
		{
			int index = _items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			onSuccess?.Invoke(_items[index].id, _items[index].item);
		}

		public void RequestWrite(int itemID, T item, Action<int, T> onSuccess)
		{
			int index = _items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			_items[index] = new Pair<T>(itemID, item);
			onSuccess?.Invoke(_items[index].id, _items[index].item);
		}

		public void RequestRead(int itemID, Action<int, object> onSuccess)
		{
			int index = _items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			onSuccess?.Invoke(_items[index].id, _items[index].item);
		}

		public void RequestWrite(int itemID, object item, Action<int, object> onSuccess)
		{
			int index = _items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			_items[index] = new Pair<T>(itemID, (T)item);
			onSuccess?.Invoke(_items[index].id, _items[index].item);
		}

		public abstract void SetDefaultItems();
	}
}