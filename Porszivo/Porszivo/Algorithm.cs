using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porszivo
{
    abstract class Algorithm
    {
        protected readonly Robot robot;

        public Algorithm(Robot r)
        {
            robot = r;
        }

        public abstract Direction move();
    }
}
