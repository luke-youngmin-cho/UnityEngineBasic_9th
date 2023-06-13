// 변수 (Variable) 
// 아직 정해지지 않은 값

// bit : 정보처리의 최소 단위 ( 1자리 이진수 )
// byte : 데이터 처리의 최소 단위 ( 8 bit )


namespace Variables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 자료형 변수이름
            int number1 = 5; // 부호가 있는 4byte 정수형 , 표현범위 : -2^31 ~ 2^31 - 1
            uint number2 = 5; // 부호가 없는 4byte 정수형, 표현범위 : 0 ~ 2^32 - 1
            short short1 = 0; // 부호가 있는 2byte 정수형
            ushort short2 = 0; // 부호가 없는 2byte 정수형
            long long1 = 1; // 부호가 있는 8byte 정수형

            long1 = number1; // 자료형의 승격
                             // (작은 레지스터 연산 결과를 큰 레지스터로 쓸때는 자료의 손실이 없으므로 데이터를 쓰는데 문제가 없다)
                             // 암시적 형변환(캐스팅)
            long1 = number1 + number2; 
            number1 = (int)long1; // 명시적 형변환(캐스팅)

            float float1 = 3.5f; // 4byte 실수형
            double double1 = 3.0;
            bool bool1 = true; // 1byte 논리형 (boolean) 0일 경우 false, 0아 아닐경우 true
            char char1 = 'A'; // 2byte 문자형, ASCII 코드표에따른 정수형을 사용
            string string1 = "Hello?"; // 2byte * 문자수 + 1byte (null)
        }
    }
}