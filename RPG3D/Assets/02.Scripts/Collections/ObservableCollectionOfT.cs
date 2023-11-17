using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Collections
{
	public class ObservableCollection<T> : IList<T>, INotifyCollectionChanged<T>
		where T : IEquatable<T>
	{
		public ObservableCollection()
		{
			items = new T[4];
		}

		public ObservableCollection(int capacity)
		{
			items = new T[capacity];
		}

		public ObservableCollection(T[] copy)
		{
			items = new T[copy.Length];
			copy.CopyTo(items, 0);
			_count = items.Length;
		}

		public ObservableCollection(IEnumerable<T> copy)
		{
			items = copy.ToArray();
			_count = items.Length;
		}


		public T this[int index] 
		{
			get 
			{
				if (index < 0 || index >= _count)
					throw new IndexOutOfRangeException();
				return items[index];
			}
			set
			{
				Change(index, value);
			}
		}

		public int Count => _count;

		public bool IsReadOnly => throw new NotImplementedException();
		private int _count;
		public T[] items;

		public event Action<int, T> onItemChanged;
		public event Action<int, T> onItemAdded;
		public event Action<int, T> onItemRemoved;
		public event Action onCollectionChanged;

		public void Change(int index, T item)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();

			items[index] = item;
			onItemChanged?.Invoke(index, item);
			onCollectionChanged?.Invoke();
		}

		public void Swap(int index1, int index2)
		{
			if (index1 < 0 || index1 >= _count ||
				index2 < 0 || index2 >= _count)
				throw new IndexOutOfRangeException();

			if (index1 == index2)
				return;

			T tmp = items[index1];
			items[index1] = items[index2];
			items[index2] = tmp;
			onItemChanged?.Invoke(index1, items[index1]);
			onItemChanged?.Invoke(index2, items[index2]);
			onCollectionChanged?.Invoke();
		}


		public void Add(T item)
		{
			if (_count == items.Length)
			{
				T[] tmp = new T[_count * 2];
				Array.Copy(items, tmp, _count);
				items = tmp;
			}

			items[_count++] = item;
			onItemAdded?.Invoke(_count - 1, item);
			onCollectionChanged?.Invoke();
		}

		public void Clear()
		{
			Array.Clear(items, 0, _count);
		}

		public bool Contains(T item)
		{
			for (int i = 0; i < _count; i++)
			{
				if (items[i].Equals(item))
					return true;
			}
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(array, items, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new Enumerator(this);
		}

		public int IndexOf(T item)
		{
			for (int i = 0; i < _count; i++)
			{
				if (items[i].Equals(item))
					return i;
			}
			return -1;
		}

		public int FindIndex(Predicate<T> match)
		{
			for (int i = 0; i < _count; i++)
			{
				if (match.Invoke(items[i]))
					return i;
			}
			return -1;
		}

		public List<int> FindAllIndex(Predicate<T> match)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < _count; i++)
			{
				if (match.Invoke(items[i]))
					list.Add(i);
			}
			return list;
		}

		public void Insert(int index, T item)
		{
			Add(items[_count - 1]);
			for (int i = _count - 2; i >= index ; i--)
			{
				items[i + 1] = items[i];
				onItemChanged?.Invoke(i + 1, items[i + 1]);
			}
			items[index] = item;
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

			T item = items[index];
			for (int i = index; i < _count - 1; i++)
			{
				items[i] = items[i + 1];
				onItemChanged?.Invoke(i, items[i]);
			}
			_count--;
			onItemRemoved?.Invoke(index, item);
			onCollectionChanged?.Invoke();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		public struct Enumerator : IEnumerator<T>
		{
			public T Current => _data[_index];

			object IEnumerator.Current => _data[_index];

			private readonly ObservableCollection<T> _data;
			private int _index;

			public Enumerator(ObservableCollection<T> data)
			{
				_data = data;
				_index = -1;
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (_index < _data._count)
					_index++;

				return _index < _data.Count;
			}

			public void Reset()
			{
				_index = -1;
			}
		}
	}
}