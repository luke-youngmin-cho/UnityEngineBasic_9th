using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.DataModel
{
	[Serializable]
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

	[Serializable]
	public abstract class DataModelBase<T> : IDataModel<T>
		where T : IEquatable<T>
	{
		public IEnumerable<int> itemIDs => m_items.Select(pair => pair.id);

		public IEnumerable<T> items => m_items.Select(pair => pair.item);

		IEnumerable IDataModel.items => m_items.Select(pair => pair.item);

		public List<Pair<T>> m_items;


		public void RequestRead(int itemID, Action<int, T> onSuccess)
		{
			int index = m_items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			onSuccess?.Invoke(m_items[index].id, m_items[index].item);
		}

		public void RequestWrite(int itemID, T item, Action<int, T> onSuccess)
		{
			int index = m_items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			m_items[index] = new Pair<T>(itemID, item);
			onSuccess?.Invoke(m_items[index].id, m_items[index].item);
		}

		public void RequestRead(int itemID, Action<int, object> onSuccess)
		{
			int index = m_items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			onSuccess?.Invoke(m_items[index].id, m_items[index].item);
		}

		public void RequestWrite(int itemID, object item, Action<int, object> onSuccess)
		{
			int index = m_items.FindIndex(item => item.id == itemID);

			if (index < 0)
			{
				// onFailure?.invoke~~
				return;
			}

			m_items[index] = new Pair<T>(itemID, (T)item);
			onSuccess?.Invoke(m_items[index].id, m_items[index].item);
		}

		public abstract void SetDefaultItems();
	}
}