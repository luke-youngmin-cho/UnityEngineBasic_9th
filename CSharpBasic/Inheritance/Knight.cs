using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class Knight : Character
    {
        public int lv;
        public Knife knife;

        public void Equip(Knife knife)
        {
            this.knife = knife;
        }

        public override void UniqueSkill()
        {
            Console.WriteLine("Knight 고유스킬 발동!");
            knife.Skill();
        }
    }

    interface Knife
    {
        void Skill();
    }

    interface NoobKnife : Knife
    {
    }

    interface ProperKnife : Knife
    {
    }

    interface HighKnife : Knife
    {
    }

    interface legendKnife : Knife
    {
    }
}
