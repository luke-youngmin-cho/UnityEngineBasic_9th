using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal abstract class Enemy : Creature, IDamageable
    {
        public float hp
        {
            get
            {
                return _hp;
            }
            set
            {
                if (value < 0)
                    value = 0;

                _hp = value;
            }
        }

        private float _hp;

        public void Damage(float amount)
        {
            hp -= amount;
        }
    }
}
