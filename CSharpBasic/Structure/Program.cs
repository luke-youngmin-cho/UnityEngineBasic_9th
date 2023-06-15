namespace Structure
{
    // structure (구조체) 
    // 캡슐화를 위한 사용자정의 자료형
    // 정의 형태 : 
    // struct 구조체이름
    // {
    //      멤버 정의 
    // }

    // class vs struct
    // 1. 멤버 변수의 크기 총합이 16byte 를 넘어가면 그냥 class 씀. 
    // 2. 값을 전달하고(함수의 인자 등) 연산하는 내용이 빈번히 일어날 때 struct를 씀.

    struct Vector3
    {
        private float _x;
        private float _y;
        private float _z;
    }

    struct Coord
    {
        public int x;
        public int y;

        public Coord()
        {
            x = 0;
            y = 0;
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(x * x + y * y);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Coord coord1 = new Coord(); // 구조체는 값타입, 스택영역에 할당됨.
            Coord coord2 = coord1;
            coord1.x = 3;
            coord1.y = 2;
            Console.WriteLine(coord1.GetMagnitude());
            Console.WriteLine(coord2.GetMagnitude());
        }
    }
}