namespace Statement_SwitchCase
{
    // enum 열거형 사용자정의 자료형
    enum StateType
    {
        Idle,  // ...00000000
        Move,  // ...00000001
        Jump,  // ...00000010
        Attack,// ...00000011
    }

    enum LayerType
    {
        Default = 0 << 0, // ...00000000
        Player  = 1 << 0, // ...00000001
        Enemy   = 1 << 1, // ...00000010
        Ground  = 1 << 2, // ...00000100
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "요네";
            switch (name)
            {
                case "말파이트":
                case "가렌":
                    Console.WriteLine("탑 1 티어");
                    break;
                case "클레드":
                    Console.WriteLine("탑 2 티어");
                    break;
                case "요네":
                    Console.WriteLine("탑 3 티어");
                    break;
                default:
                    Console.WriteLine("티어를 검색할 수 없습니다");
                    break; // 현재 switch 문을 빠져나간다는뜻
            }

            // PlayerState
            // 0 : idle
            // 1 : move
            // 2 : jump
            // 3 : attack
            int playerState = 0;
            switch (playerState)
            {
                case 0:
                    {
                        Console.WriteLine("가만히 있기 로직 실행");
                        break;
                    }
                case 1:
                    Console.WriteLine("걷기 로직 실행");
                    break;
                case 2:
                    Console.WriteLine("점프 로직 실행");
                    break;
                case 3:
                    Console.WriteLine("공격 로직 실행");
                    break;
                default:
                    break;
            }

            StateType state = StateType.Idle;
            switch (state)
            {
                case StateType.Idle:
                    break;
                case StateType.Move:
                    break;
                case StateType.Jump:
                    break;
                case StateType.Attack:
                    break;
                default:
                    break;
            }
        }
    }
}