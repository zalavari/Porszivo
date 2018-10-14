using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Porszivo
{
    public class Robot
    {
        // robot sugara
        private int radius;

        private FieldType[,] room;
                
        // robot koordinátái - TODO: valószínűleg külön objektumban kellene tárolni őket 
        public int positionX;
        public int positionY;
        public int roomMaxX;
        public int roomMaxY;

        private Algorithm algorithm1;
        private Algorithm algorithm2;
        
        private ProximitySensor ProximitySensor { get; set; }

        private DrivingUnit DrivingUnit { get; set; }

        public Robot(ProximitySensor proximitySensor, DrivingUnit drivingUnit, Room room_)
        {
            ProximitySensor = proximitySensor;
            DrivingUnit = drivingUnit;

            room = new FieldType[room_.MaxX, room_.MaxY];
            for (int i = 0; i < room_.MaxX; ++i) for (int j = 0; j < room_.MaxY; ++j) {room[i,j] = FieldType.UNKNOWN;}
            roomMaxX = room_.MaxX;
            roomMaxY = room_.MaxY;
            
            radius = 1;
            positionX = room_.RobotX;
            positionY = room_.RobotY;

            algorithm1 = new RandomPathChooserAlgorithm(this);
            algorithm2 = new DepthFirstSearchPathChooserAlgorithm(this);
        }

        private void scanRoom()
        {
            ProximitySensor.scanRoom();

            for (int i = 0; i < roomMaxX; i++)
                for (int j = 0; j < roomMaxY; j++)
                {

                    if (getFieldType(i, j) == FieldType.UNKNOWN)
                        setFieldType(i, j, ProximitySensor.getFieldType(i, j));
                }
        }


        /*
         * A robotnak lépnie kell. A rendelkezésre álló adatok alapján eldönti merre akar lépni, és végül a DrivingUnit-on keresztül lép.
         **/
        public void move() 
        {
            setFieldType(positionX, positionY, FieldType.CLEAN);
            
            scanRoom();
            Direction direction = algorithm2.move();

            DrivingUnit.Move(direction);
            updatePosition(direction);
        }

        public void updatePosition(Direction direction) 
        {
            switch (direction)
            {
                case Direction.UP: positionY -= 1; break;
                case Direction.RIGHT: positionX += 1; break;
                case Direction.DOWN: positionY += 1; break;
                case Direction.LEFT: positionX -= 1; break;
                case Direction.STAY: break;
            }
        }

      

        public FieldType getFieldType(int x, int y)
        {
            return room[x, y];
        }

        public void setFieldType(int x, int y, FieldType ft)
        {
            room[x, y] = ft;
        }
    }
}