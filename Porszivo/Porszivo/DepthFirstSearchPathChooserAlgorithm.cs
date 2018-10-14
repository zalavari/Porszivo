using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Porszivo
{
    class DepthFirstSearchPathChooserAlgorithm
    {
        readonly Robot robot;

        Stack<Direction> directionStack = new Stack<Direction>();

        public DepthFirstSearchPathChooserAlgorithm(Robot robot)
        {
            this.robot = robot;
        }

        public Direction move()
        {
            Direction direction = Direction.STAY;
            
            if (isDirty(robot.positionX, robot.positionY - 1))
                direction = Direction.UP;
            else if (isDirty(robot.positionX - 1, robot.positionY))
                direction = Direction.LEFT;
            else if (isDirty(robot.positionX, robot.positionY + 1))
                direction = Direction.DOWN;
            else if (isDirty(robot.positionX + 1, robot.positionY))
                direction = Direction.RIGHT;

            if (direction == Direction.STAY && directionStack.Count != 0)
                direction = reverseDirection(directionStack.Pop());
            else
                directionStack.Push(direction);

            return direction;
        }

        public bool isDirty(int x, int y)
        {
            return (robot.getFieldType(x, y) == FieldType.DIRTY);
        }

        public Direction reverseDirection(Direction d)
        {
            switch (d)
            {
                case Direction.UP: return Direction.DOWN;
                case Direction.DOWN: return Direction.UP;
                case Direction.LEFT: return Direction.RIGHT;
                case Direction.RIGHT: return Direction.LEFT;
            }
            return Direction.STAY;
        }
    }
}
