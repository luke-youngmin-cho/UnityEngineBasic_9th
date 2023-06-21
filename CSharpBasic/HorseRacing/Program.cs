using System.Threading;

namespace HorseRacing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Horse[] horses = new Horse[5];
            for (int i = 0; i < horses.Length; i++)
            {
                horses[i] = new Horse();
                horses[i].name = $"경주마{i}";
            }
            string[] rankArray = new string[5];
            int currentGrade = 0;

            while (currentGrade < horses.Length)
            {
                // 말달리는 내용 작성

                Thread.Sleep(1000);
            }

            for (int i = 0; i < rankArray.Length; i++)
            {
                Console.WriteLine($"{rankArray[i]} 가 {i + 1} 등");
            }
        }
    }
}