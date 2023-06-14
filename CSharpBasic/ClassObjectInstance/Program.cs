namespace ClassObjectInstance
{
    // 값 타입 vs 참조 타입
    // 값 타입 - 값을 그대로 읽고 씀
    // 참조 타입 - 객체가 있는 주소참조를 통해 간접적으로 값을 읽고 씀
    internal class Program
    {
        static void Main(string[] args)
        {
            // 지역변수 
            // 특정 지역 내에서만 사용되는 변수 (일반적으로 함수, 접근자 내에서)
            int num = 1;
            //Console.WriteLine(num);

            // new 키워드
            // 동적 할당 키워드
            // 클래스 생성자 앞에 사용하면, 해당 클래스를 힙 영역에다가 동적할당
            Human human1 = new Human();
            Human human2 = new Human();

            human1.name = "Luke";
            human2.name = "Karl";
            human1.SayName();
            human2.SayName();

            // . 멤버 접근 연산자
            Console.WriteLine(human1.age);
        }
    }

    // class 키워드 
    // 클래스를 정의
    // 형식 :
    // class 클래스이름
    // {
    //      멤버들(변수, 함수, 다른 사용자정의 자료형)을 정의
    // }
    // 사용자 정의 자료형
    // 클래스는 캡슐화를 위해 탄생했다..! 
    // 외부로부터 일반적으로는 멤버들에 접근하지 못하도록 보호하는 컨셉
    // 클래스의 멤버는 default 접근제한자가 모두 private.
    //
    // 접근 제한자
    // private - 외부접근 일절 불가
    // public - 외부 접근 가능
    // protected - 상속자(자식) 만 접근 가능
    // internal - 동일 어셈블리내에서 접근 가능
    class Human
    {
        public int age = 1;
        private float height;
        private double weight;
        private char gender;
        public string name;

        // 생성자
        // 메모리공간에 해당 클래스타입의 객체를 할당하는 (생성하는) 함수
        public Human()
        {

        }

        // 소멸자
        // 객체를 메모리에서 해제하는 함수
        ~Human()
        {

        }


        public void SayName()
        {
            // this 키워드
            // 객체 자기 자신 참조 키워드(이 함수를 호출한 객체의 참조)
            Console.WriteLine(this.name);
        }
    }
}