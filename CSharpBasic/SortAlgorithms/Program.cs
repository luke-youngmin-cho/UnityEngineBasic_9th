namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 4, 3, 3, 9, 8, 7, 2, 5, 0};

            ArraySort.BubbleSort(arr);

            Console.Write("Result : ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}, ");
            }
        }
    }
}