namespace Statement_Loop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // while 문
            // 특정조건에서 특정내용을 반복수행 
            int count = 0;
            while (count < 5)
            {
                Console.WriteLine("Repeating!!");
                count++;
            }

            // do while 문
            // 일단 한번 반복내용 수행하고, 그 뒤
            // 특정조건에서 특정내용을 반복수행
            do
            {
                Console.WriteLine("do while");
            } while (false);

            // for 문
            //for (처음 한번 실행할 내용; 반복 조건; 루프의 마지막에 실행할 내용)
            //{
            //
            //}
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("For looping!");
            }
        }
    }
}