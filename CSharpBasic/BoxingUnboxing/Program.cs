using System.Linq.Expressions;

namespace BoxingUnboxing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            object obj1 = new object();
            Object obj2 = new Object();
            obj1 = 1;
            int a = 1;
            long long1 = (long)obj1;
            int b = a;
            // boxing
            // object 타입으로 변환하는 과정 (힙영역에 object 타입 객체 만들고 데이터 씀)

            // unboxing
            // 힙영역의 object 객체에서 데이터를 원래 데이터로 바꾸는 과정
            Console.WriteLine(Compare(1, 1));
        }

        //static bool Compare(int a, int b)
        //{
        //    return a == b;
        //}

        static bool Compare(object a , object b)
        {
            return a == b;
        }

        //static bool Compare(object a, object b)
        //{
        //    return (int)a == (int)b;
        //}
    }
}