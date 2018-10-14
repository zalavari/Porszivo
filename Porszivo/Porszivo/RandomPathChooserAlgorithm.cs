using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Porszivo
{
    class RandomPathChooserAlgorithm : Algorithm
    {
        Random rnd = new Random(0);

        public RandomPathChooserAlgorithm(Robot robot) : base(robot)
        {
        }

        public override Direction move()
        {
            Direction direction = new Direction();
            bool moved = false;
            while (!moved)
            {
                // véletlen irány generálása
                int nextDirection = rnd.Next(0, 4);                
                Debug.WriteLine("Generated number: " + nextDirection);
                switch (nextDirection)
                {
                    case 0: direction = Direction.UP; break;
                    case 1: direction = Direction.RIGHT; break;
                    case 2: direction = Direction.DOWN; break;
                    case 3: direction = Direction.LEFT; break;
                }
                if (canMove(direction) && isPreferred(direction))
                {
                    moved = true;
                }
            }
            return direction;
      
        }


        /*
         * Hamis, ha a jelenlegi irányban tiszta mezőre lépne, pedig van koszos szomszédja. Egyébként igaz.
         * */
        private bool isPreferred(Direction direction)
        {
            int dirty = 0;
            if (robot.roomMaxY > (robot.positionY + 1))
            {
                dirty += (robot.getFieldType(robot.positionX, robot.positionY + 1) == FieldType.DIRTY) ? 1 : 0;
            }
            if (robot.roomMaxX > (robot.positionX + 1))
            {
                dirty += (robot.getFieldType(robot.positionX + 1, robot.positionY) == FieldType.DIRTY) ? 1 : 0;
            }
            if (0 < robot.positionY)
            {
                dirty += (robot.getFieldType(robot.positionX, robot.positionY - 1) == FieldType.DIRTY) ? 1 : 0;
            }
            if (0 < robot.positionX - 1) 
            {
                dirty += (robot.getFieldType(robot.positionX - 1, robot.positionY) == FieldType.DIRTY) ? 1 : 0;
            }
            return !((robot.getFieldType(getXDirection(direction), getYDirection(direction)) == FieldType.CLEAN) 
                && (dirty > 0));
        }

        private int getXDirection(Direction direction) 
        {
            switch (direction)
            {
                case Direction.UP: return robot.positionX; break;
                case Direction.DOWN: return robot.positionX; break;
                case Direction.RIGHT: 
                    if ((robot.positionX  + 1) < robot.roomMaxX) 
                        return robot.positionX + 1; 
                    else 
                        return robot.positionX;
                    break;
                case Direction.LEFT: 
                    if (robot.positionX > 0) 
                        return robot.positionX - 1; 
                    else 
                        return robot.positionX;
                    break;
            }
            return -1;
        }

        private int getYDirection(Direction direction) 
        {
            switch (direction)
            {
                case Direction.RIGHT: return robot.positionY; break;
                case Direction.LEFT: return robot.positionY; break;
                case Direction.DOWN: 
                    if ((robot.positionY  + 1) < robot.roomMaxY) 
                        return robot.positionY + 1; 
                    else 
                        return robot.positionY;
                    break;
                case Direction.UP: 
                    if (robot.positionY > 0) 
                        return robot.positionY - 1; 
                    else 
                        return robot.positionY;
                    break;
            }
            return -1;
        }

        /*
       * Képes-e lépni a robot egy adott irányba.
       **/
        private bool canMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.DOWN:
                    if (robot.roomMaxY > (robot.positionY + 1))
                    {
                        if (robot.getFieldType(robot.positionX, robot.positionY + 1) == FieldType.OBSTACLE)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.RIGHT:
                    if (robot.roomMaxX > (robot.positionX + 1))
                    {
                        if (robot.getFieldType(robot.positionX + 1, robot.positionY) == FieldType.OBSTACLE)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.UP:
                    if (0 < robot.positionY)
                    {
                        if (robot.getFieldType(robot.positionX, robot.positionY - 1) == FieldType.OBSTACLE)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.LEFT:
                    if (0 < robot.positionX - 1)
                    {
                        if (robot.getFieldType(robot.positionX - 1, robot.positionY) == FieldType.OBSTACLE)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
            }
            return false;
        }

    }
}
