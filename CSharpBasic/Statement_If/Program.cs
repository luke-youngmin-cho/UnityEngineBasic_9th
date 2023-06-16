namespace Statement_If
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool condition1 = true;
            bool condition2 = false;

            if (condition1)
            {
                Console.WriteLine("조건 1 이 참");
            }
            else if (condition2)
            {
                Console.WriteLine("위 조건들이 모두 거짓이면서 조건 2가 참");
            }
            else
            {
                Console.WriteLine("위 조건들이 모두 거짓");
            }
        }
    }
}