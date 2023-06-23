using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class SwordMan : Character
    {
        public override void UniqueSkill()
        {
            Console.WriteLine("SwordMan의 고유스킬 발동!");
        }
    }
}
