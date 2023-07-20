using System.Diagnostics;
using System.Linq;

namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random random = new Random();
            int[] arr // = { 1, 4, 3, 3, 9, 8, 7, 2, 5, 0};
                      = Enumerable
                            .Repeat(0, 100000)
                            .Select(i => random.Next(0, 100000))
                            .ToArray();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //ArraySort.BubbleSort(arr);
            //ArraySort.SelectionSort(arr);
            ArraySort.InsertionSort(arr);

            stopwatch.Stop();
            Console.WriteLine($"소요시간 : {stopwatch.ElapsedMilliseconds}");

            //Console.Write("Result : ");
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.Write($"{arr[i]}, ");
            //}
        }
    }
}