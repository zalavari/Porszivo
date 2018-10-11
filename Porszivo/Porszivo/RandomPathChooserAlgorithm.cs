using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porszivo
{
    class RandomPathChooserAlgorithm
    {
        readonly Robot robot;

        public RandomPathChooserAlgorithm(Robot robot)
        {
            this.robot = robot;
        }

        public Direction move()
        {
            Direction direction = new Direction();
            bool moved = false;
            Random rnd = new Random(0);
            while (!moved)
            {
                // véletlen irány generálása
                int nextDirection = rnd.Next(0, 3);
                switch (nextDirection)
                {
                    case 0: direction = Direction.UP; break;
                    case 1: direction = Direction.RIGHT; break;
                    case 2: direction = Direction.DOWN; break;
                    case 3: direction = Direction.LEFT; break;
                }
                if (canMove(direction))
                {
                    moved = true;
                }
            }
            return direction;
      
        }

        /*
       * Képes-e lépni a robot egy adott irányba.
       **/
        private bool canMove(Direction direction)
        {
            // Egyelőre csak az egyszerű esetet nézzük meg, ha a robot 1 egység nagy --> csak az előtte álló mező érdekes
            // TODO: ki kell egészíteni, hogy a teljes porszívó előtti területet vizsgálja
            //    erre ötlet (ha fölfelé megy): 
            //    i = (posX - robotRadius)-tól (posX + robotRadius)-ig minden mezőn posX - i mezővel előre nézünk
            switch (direction)
            {
                case Direction.DOWN:
                    if (robot.roomMaxY > (robot.positionY + 1))
                    {
                        //if (Room.getFieldType(positionX, positionY + 1) == FieldType.OBSTACLE)
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
                        //if (Room.getFieldType(positionX + 1, positionY) == FieldType.OBSTACLE)
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
                        //if (Room.getFieldType(positionX, positionY - 1) == FieldType.OBSTACLE)
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
                        //if (Room.getFieldType(positionX - 1, positionY) == FieldType.OBSTACLE)
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
            throw new NotImplementedException();
        }

    }
}
