using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porszivo
{
    public class Room
    {
        public int MaxX
        {
            get => default(int);
            private set
            {
            }
        }

        public int MaxY
        {
            get => default(int);
            private set
            {
            }
        }

        public int RobotX
        {
            get => default(int);
            private set
            {
            }
        }

        public int RobotY
        {
            get => default(int);
            private set
            {
            }
        }

        public FieldType getFieldType(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void moveRobot(Direction direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
