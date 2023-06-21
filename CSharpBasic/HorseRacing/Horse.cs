using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacing
{
    internal class Horse
    {
        public string name;
        public float distance;
        private Random random = new Random();

        public void Run()
        {
            distance += (1.0f + random.NextSingle()) * 10.0f;
        }
    }
}
