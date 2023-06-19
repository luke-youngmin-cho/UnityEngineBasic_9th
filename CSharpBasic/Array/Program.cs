using System.Text;

namespace Array
{
    // 배열
    // 연속적인 데이터 타입 (특정 타입이 메모리상에 연속적으로 붙어있는 형태)
    internal class Program
    {
        static char[] buffer = new char[20];

        static void Main(string[] args)
        {
            // 배열은 참조타입. 
            // new 자료형[배열크기] 하게되면 힙영역에 배열을 할당하고 해당 배열 참조를 반환.
            int[] intArr = new int[3];
            intArr = new int[3] { 1, 2, 3 };

            int[] intArr2 = { 1, 2, 3};

            // 인덱서 []
            // 인덱스 접근용 프로퍼티
            // 타입의 크기 * 인덱스 (인덱서인자) 뒤의 주소를 참조하는 연산자
            // 호출방법 : 변수이름[인덱스];
            Console.WriteLine(intArr[0]);
            Console.WriteLine(intArr[1]);
            Console.WriteLine(intArr[2]);
            //Console.WriteLine(intArr[3]);

            for (int i = 0; i < intArr.Length; i++)
            {
                Console.WriteLine(intArr[i]);
            }

            intArr.CopyTo(intArr2, 0);
            System.Array.Copy(intArr, intArr2, 2);
            intArr.Clone();

            int num = 3;
            String name = "Luke";
            string name2 = "Kai";
            Console.WriteLine(name[0]);
            for (int i = 0; i < name.Length; i++)
            {
                Console.WriteLine(name[i]);
            }

            name2 = "A" + "B" + "C"; // "ABC"
            string string1 = "A";
            string string2 = "B";
            string string3 = "C";
            


            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Clear();
            stringBuilder.Append("A");
            stringBuilder.Replace("A", "B");
        }

        public string SumStrings(string[] strings)
        {
            int current = 0;
            int totalLength = 0;
            for (int i = 0; i < strings.Length; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    buffer[current++] = strings[i][j];
                }

                totalLength += strings[i].Length;
            }

            return new string(buffer, 0, totalLength);
        }

    }
}