using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class AVLTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public T Value;
            public Node Left;
            public Node Right;
            public int Depth;
        }

        private Node _root;

        public void Add(T value)
        {
            _root = Add(_root, value);
        }

        private Node Add(Node node, T value)
        {
            if (node == null)
            {
                return new Node { Value = value, Depth = 1 };
            }

            int compare = value.CompareTo(node.Value);
            if (compare < 0)
            {
                node.Left = Add(node.Left, value); 
            }
        }
    }
}
