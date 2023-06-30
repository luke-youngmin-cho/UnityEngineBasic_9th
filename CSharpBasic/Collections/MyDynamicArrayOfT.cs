using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class A
{
    public int name;

    public void SayHi()
    {
        Console.WriteLine("A");
    }
}

public class B : A
{
    new public int name;

    new public void SayHi()
    {
        Console.WriteLine("B");
    }
}

public class Tester
{
    public void Test()
    {
        A a = new A();
        B b = new B();
        a.SayHi(); // -> A
        b.SayHi(); // -> B

        a = b;
        a.SayHi(); // -> A
        ((B)a).SayHi(); // -> B
    }
}


namespace Collections
{
    internal class MyDynamicArray<T> : IEnumerable<T>
    {
        private static int DEFAULT_SIZE = 1;
        public int Length => _length;
        public int Capacity => _items.Length;
        private object[] _items = new object[DEFAULT_SIZE];
        private int _length;

        /// <summary>
        /// 삽입 알고리즘
        /// </summary>
        /// <param name="item"></param>
        public void Add(object item)
        {
            // 공간이 모자라다면 더 큰배열을 만들고 아이템 복제
            if (_length == _items.Length)
            {
                object[] tmpItems = new object[_length * 2];
                Array.Copy(_items, 0, tmpItems, 0, _length);
                _items = tmpItems;
            }

            _items[_length++] = item;
        }

        /// <summary>
        /// 탐색 알고리즘
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(object item)
        {
            Comparer comparer = Comparer.Default; 
            // Comparer.Default : 해당 타입의 default 비교연산자를 가지고 비교해서 결과를 반환하는 기능을 가진 객체
            for (int i = 0; i < _length; i++)
            {
                if (comparer.Compare(_items[i], item) == 0)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 삭제 알고리즘
        /// </summary>
        /// <returns> 삭제 여부 </returns>
        public bool Remove(object item)
        {
            int index = IndexOf(item);

            if (index < 0)
                return false;

            for (int i = index; i < _length - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _length--;
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _length; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
