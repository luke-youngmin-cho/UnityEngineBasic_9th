using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class Node<T>
    {
        public T Value;
        public Node<T> Prev;
        public Node<T> Next;

        public Node(T value) => Value = value;
    }

    internal class MyLinkedList<T> : IEnumerable<T>
    {
        public Node<T> First => _first;
        public Node<T> Last => _last;
        private Node<T> _first, _last, _tmp;

        public void AddFirst(T value)
        {
            _tmp = new Node<T>(value);

            if (_first != null)
            {
                _tmp.Next = _first;
                _first.Prev = _tmp;
            }
            else
            {
                _last = _tmp;
            }

            _first = _tmp;
        }

        public void AddLast(T value)
        {
            _tmp = new Node<T>(value);

            if (_last != null)
            {
                _last.Next = _tmp;
                _tmp.Prev = _last;
            }
            else
            {
                _first = _tmp;
            }

            _last = _tmp;
        }

        public void AddBefore(Node<T> node, T value)
        {
            _tmp = new Node<T>(value);

            if (node.Prev != null)
            {
                node.Prev.Next = _tmp;
                _tmp.Prev = node.Prev;
            }
            else
            {
                _first = _tmp;
            }

            node.Prev = _tmp;
            _tmp.Next = node;
        }

        public void AddAfter(Node<T> node, T value)
        {
            _tmp = new Node<T>(value);

            if (node.Next != null)
            {
                node.Next.Prev = _tmp;
                _tmp.Next = node.Next;
            }
            else
            {
                _last = _tmp;
            }

            node.Next = _tmp;
            _tmp.Prev = node;
        }

        public Node<T> Find(Predicate<T> match)
        {

        }

        public Node<T> FindLast(Predicate<T> match)
        {

        }

        public bool Remove(T value)
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public struct Enumerator : IEnumerator<T>
        {
            public T Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
