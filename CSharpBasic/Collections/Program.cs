﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Queue
            Queue<int> queue1 = new Queue<int>();
            queue1.Enqueue(3);
            queue1.Enqueue(2);
            queue1.Enqueue(5);

            while (queue1.Count > 0)
            {
                if (queue1.Peek() > 1)
                    Console.WriteLine(queue1.Dequeue());
            }
            #endregion

            #region Stack
            Stack<float> stack1 = new Stack<float>();
            stack1.Push(3);
            stack1.Push(2);
            stack1.Push(5);

            while (stack1.Count > 0)
            {
                if (stack1.Peek() > 1)
                    Console.WriteLine(stack1.Pop());
            }
            #endregion

            #region HashSet
            HashSet<int> visited = new HashSet<int>();
            if (visited.Add(3))
            {
                Console.WriteLine("Added 3 in hashset");
            }
            if (visited.Remove(2))
            {
                Console.WriteLine("Removed 2 in hashset");
            }
            #endregion

            #region Dynamic Array
            MyDynamicArray myDynamicArray = new MyDynamicArray();
            myDynamicArray.Add(3);
            myDynamicArray.Add(2);
            myDynamicArray.Add(5);
            myDynamicArray.Add(7);

            IEnumerator e1 = myDynamicArray.GetEnumerator();
            while (e1.MoveNext())
            {
                Console.WriteLine(e1.Current);
            }

            MyDynamicArray<int> intDA = new MyDynamicArray<int>();
            intDA.Add(3);
            intDA.Add(2);
            intDA.Add(5);
            intDA.Add(7);

            IEnumerator<int> intDAEnum = intDA.GetEnumerator();
            while (intDAEnum.MoveNext())
            {
                Console.WriteLine(intDAEnum.Current);
            }
            intDAEnum.Reset();
            intDAEnum.Dispose();
            
            // IDisposable 객체의 Dispose() 함수의 호출을 보장하는 구문.
            using (IEnumerator<int> intDAEnum2 = intDA.GetEnumerator())
            {
                while (intDAEnum.MoveNext())
                {
                    Console.WriteLine(intDAEnum.Current);
                }
                intDAEnum.Reset();
            }

            // foreach 문
            // IEnumerable 을 순회하는 구문
            foreach (int item in intDA)
            {
                Console.WriteLine(item);
            }

            List<int> list = new List<int>();
            list.Add(3);
            list.Remove(3);
            list.IndexOf(3);
            list.Contains(3);
            list.Find(x => x > 1);
            list.Insert(0, 2);
            #endregion
        }
    }
}