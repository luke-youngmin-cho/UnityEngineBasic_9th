using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // 클래스 상속 
    // 자식클래스이름 : 부모클래스이름
    internal class NPC : Creature
    {
        public int Id;

        public void Interaction()
        {
            Console.WriteLine("NPC 와의 상호작용 시작");
        }

        // override 재정의 키워드 
        // 가상/추상 멤버를 재정의 할 때 씀
        public override void Breath()
        {
            // base 키워드 
            // 기반타입 참조 키워드
            Console.Write("NPC가");
            base.Breath();
        }
    }
}
