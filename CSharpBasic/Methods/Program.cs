// Function vs Method
// Function : 연산을 수행할 수 있는 기능
// Method : 객체/클래스 단위로 호출되는 Function
// Method ⊂ Function

// 함수 형태 3가지
// 함수 선언 -> f(x) 라는게 있다 라고 명시하는것
// 함수 정의 -> f(x) = ax + b 라고 연산부분을 명시하는 것
// 함수 호출 -> f(2) 처럼 변수자리에 인수를 넣고 함수 이름을 씀

// f(a, b) = a + b
namespace Methods
{
    internal class Program
    {
        // void : 반환 할 타입이 없다 
        static void Main(string[] args)
        {
            int sum = Sum(1, 2);
            Console.WriteLine(sum);
            SayHello();
        }

        static int Sum(int a,int b)
        {
            return a + b;
        }

        static void SayHello()
        {
            Console.WriteLine("Hi");
            Console.WriteLine("Hello");
            Console.WriteLine("How r u ?");
            return; // void 반환 함수에서 마지막 return 은 생략가능하다.
        }

    }
}