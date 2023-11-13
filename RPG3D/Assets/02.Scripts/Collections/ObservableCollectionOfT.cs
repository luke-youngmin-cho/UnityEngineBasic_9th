using System;
using System.Collections;
using System.Collections.Generic;

namespace RPG.Collections
{
	public class ObservableCollection<T> : IList<T>, INotifyCollectionChanged<T>
		where T : IEquatable<T>
	{
		public T this[int index] 
		{
			get 
			{
				if (index < 0 || index >= _count)
					throw new IndexOutOfRangeException();
				return _items[index];
			}
			set
			{
				Change(index, value);
			}
		}

		public int Count => _count;

		public bool IsReadOnly => throw new NotImplementedException();
		private int _count;
		private T[] _items;

		public event Action<int, T> onItemChanged;
		public event Action<int, T> onItemAdded;
		public event Action<int, T> onItemRemoved;
		public event Action onCollectionChanged;

		public void Change(int index, T item)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();

			_items[index] = item;
			onItemChanged?.Invoke(index, item);
			onCollectionChanged?.Invoke();
		}

		public void Add(T item)
		{
			if (_count == _items.Length)
			{
				T[] tmp = new T[_count * 2];
				Array.Copy(_items, tmp, _count);
				_items = tmp;
			}

			_items[_count++] = item;
			onItemAdded?.Invoke(_count - 1, item);
			onCollectionChanged?.Invoke();
		}

		public void Clear()
		{
			Array.Clear(_items, 0, _count);
		}

		public bool Contains(T item)
		{
			for (int i = 0; i < _count; i++)
			{
				if (_items[i].Equals(item))
					return true;
			}
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(array, _items, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public int IndexOf(T item)
		{
			for (int i = 0; i < _count; i++)
			{
				if (_items[i].Equals(item))
					return i;
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			Add(_items[_count - 1]);
			for (int i = _count - 2; i >= index ; i--)
			{
				_items[i + 1] = _items[i];
				onItemChanged?.Invoke(i + 1, _items[i + 1]);
			}
			_items[index] = item;
			onItemAdded?.Invoke(index, item);
			onCollectionChanged?.Invoke();
		}

		public bool Remove(T item)
		{
			int index = IndexOf(item);

			if (index < 0)
				return false;

			RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();

			T item = _items[index];
			for (int i = index; i < _count - 1; i++)
			{
				_items[i] = _items[i + 1];
				onItemChanged?.Invoke(i, _items[i]);
			}
			_count--;
			onItemRemoved?.Invoke(index, item);
			onCollectionChanged?.Invoke();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}