using System.Diagnostics;
using System.Linq;

namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random random = new Random();
            int[] arr //= { 1, 4, 3, 3, 9, 8, 7, 2, 5, 0};
                      = Enumerable
                            .Repeat(0, 10000)
                            .Select(i => random.Next(0, 10000))
                            .ToArray();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //ArraySort.BubbleSort(arr);
            //ArraySort.SelectionSort(arr);
            //ArraySort.InsertionSort(arr);
            //ArraySort.MergeSort(arr);
            //ArraySort.RecursiveMergeSort(arr);
            //ArraySort.RecursiveQuickSort(arr);
            //ArraySort.QuickSort(arr);
            ArraySort.HeapSort(arr);

            // -> 중복된 경우의 수가 많으면 MergeSort의 성능이 QuickSort 보다 좋아지는 경향이있다.
            // -> 공간복잡도에대해 문제가 없으면 해당 경우에서는 MergeSort를 사용하는것을 고려해야한다.

            stopwatch.Stop();
            Console.WriteLine($"소요시간 : {stopwatch.ElapsedMilliseconds}");

            Console.Write("Result : ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}, ");
            }
        }
    }
}