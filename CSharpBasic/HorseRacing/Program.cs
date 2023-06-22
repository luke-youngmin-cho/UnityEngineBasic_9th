using System.Threading;

namespace HorseRacing
{
    internal class Program
    {
        // const : 상수 키워드 
        const float GOAL_DISTANCE = 200.0f;
        const int TOTAL_HORSES = 5;

        static void Main(string[] args)
        {
            Horse[] horses = new Horse[TOTAL_HORSES];
            for (int i = 0; i < TOTAL_HORSES; i++)
            {
                horses[i] = new Horse();
                horses[i].name = $"경주마{i}";
            }
            string[] rankArray = new string[TOTAL_HORSES];
            int currentGrade = 0;
            int elapsedSec = 0;

            while (currentGrade < TOTAL_HORSES)
            {
                Console.WriteLine($"========================== {elapsedSec++} 초 경과 ================================");
                for (int i = 0; i < TOTAL_HORSES; i++)
                {
                    if (horses[i].distance < GOAL_DISTANCE)
                    {
                        horses[i].Run();
                        Console.WriteLine($"{horses[i].name} (이)가 달린 거리 : {horses[i].distance}");

                        if (horses[i].distance >= GOAL_DISTANCE)
                        {
                            rankArray[currentGrade] = horses[i].name;
                            currentGrade++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{horses[i].name} 는 이미 도착함");
                    }
                }

                Thread.Sleep(1000);
            }
            Console.WriteLine($"========================== 경기 끝 ================================");
            for (int i = 0; i < rankArray.Length; i++)
            {
                Console.WriteLine($"{rankArray[i]} 가 {i + 1} 등");
            }
        }
    }
}