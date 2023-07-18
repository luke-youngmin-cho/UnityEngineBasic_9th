using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// using 의 사용처
// 1. namespace 를 가져다쓸때
// 2. namespace 간 멤버 호출이 모호할때 (ex.다른 namespace 에 동일한 이름의 타입이 정의되어있을 때)
// 3. IDisposable 객체의 Dispose 함수 호출을 보장할 때

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
            Console.WriteLine(intDA[0]);

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

            ArrayList arrList = new ArrayList();
            arrList.Add(3);
            arrList.Add("Carl");
            arrList.Contains(3); // 결과는 false.
                                 // 왜? Add(3) 호출시 Boxing 으로인해 만들어진 객체와
                                 // Contains(3) 호출시 Boxing 으로 인해 만들어진 객체는 다르기때문.

            List<int> list = new List<int>();
            list.Add(3);
            list.Remove(3);
            list.IndexOf(3);
            list.Contains(3);
            list.Find(x => x > 1);
            list.Insert(0, 2);
            Console.WriteLine(list[0]);
            #endregion

            #region Hashtable

            Hashtable ht = new Hashtable();
            ht.Add(1, "철수");

            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            dictionary1.Add("철수", 10);
            if (dictionary1.TryGetValue("철수", out int grade))
            {
                Console.WriteLine("철수 점수 : " + grade);
            }

            MyHashtable<string, int> dictionary2 = new MyHashtable<string, int>(1000);
            dictionary2.Add("철수", 40);
            if (dictionary2.TryGetValue("철수", out grade))
            {
                Console.WriteLine("철수 점수 : " + grade);
            }

            #endregion


            #region BinaryTree

            AVLTree<int> bt = new AVLTree<int>();

            bt.Add(3);
            bt.Add(6);
            bt.Add(7);
            bt.Add(4);
            bt.Remove(3);

            #endregion
        }
    }
}
