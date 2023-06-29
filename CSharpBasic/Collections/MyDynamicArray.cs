using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyDynamicArray
    {
        public int Length => _length;
        public int Capacity => _items.Length;
        private object[] _items;
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
    }
}
