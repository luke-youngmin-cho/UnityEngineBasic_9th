using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class HpBar
    {
        public float hp
        {
            set
            {
                _hp = Math.Round(value, 2).ToString();
                Console.WriteLine($"[HpBar] : {_hp}");
            }
        }

        private string _hp;

        public void Refresh(float value)
        {
            _hp = Math.Round(value, 2).ToString();
            Console.WriteLine($"[HpBar] : {_hp}");
        }
    }
}
