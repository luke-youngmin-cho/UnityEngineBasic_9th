using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class Creature
    {
        public int DnaCode;

        // virtual 가상키워드
        // 함수를 자식이 재정의할수 있도록 하는 키워드
        public virtual void Breath()
        {
            Console.WriteLine("숨쉬기!!");
        }
    }
}
