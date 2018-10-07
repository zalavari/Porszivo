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
        
        private Room Room { get; }
        
        // robot koordinátái - TODO: valószínűleg külön objektumban kellene tárolni őket 
        public int positionX;
        public int positionY;
        public int roomMaxX;
        public int roomMaxY;

        
        private ProximitySensor ProximitySensor { get; set; }

        private DrivingUnit DrivingUnit { get; set; }

        public Robot(ProximitySensor proximitySensor, DrivingUnit drivingUnit, Room room_)
        {
            ProximitySensor = proximitySensor;
            DrivingUnit = drivingUnit;
            Room = room_;

            room = new FieldType[room_.MaxX, room_.MaxY];
            for (int i = 0; i < room_.MaxX; ++i) for (int j = 0; j < room_.MaxY; ++j) {room[i,j] = FieldType.UNKNOWN;}
            roomMaxX = room_.MaxX;
            roomMaxY = room_.MaxY;
            
            radius = 1;
            positionX = 1;
            positionY = 1;
        }

        private void scanRoom()
        {
            double delta = 0.1;
            for (double angle = 0; angle < Math.PI/2; angle+=delta)
            {
                double distance=ProximitySensor.getClosestObject(angle);
                int xd = 1, yd = 1;
                double tang = Math.Tan(angle);
                while (xd * xd + yd * yd < distance * distance)
                {
                    if (yd / xd > tang) xd++;
                    else yd++;

                    if (getFieldType(xd, yd) == FieldType.UNKNOWN)
                        setFieldType(xd, yd, FieldType.DIRTY);               
                }
            }
            //throw new System.NotImplementedException();
        }

        /*
         * A robotnak lépnie kell. A rendelkezésre álló adatok alapján eldönti merre akar lépni, és végül a DrivingUnit-on keresztül lép.
         **/
        public void move() 
        {
            Direction direction = new Direction();
            scanRoom();
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
                if (canMove(direction)) {
                    DrivingUnit.Move(direction);
                    updatePosition(direction);
                    moved = true;
                }
            }
        }

        public void updatePosition(Direction direction) 
        {
            switch (direction)
            {
                case Direction.UP: positionY += 1; break;
                case Direction.RIGHT: positionX += 1; break;
                case Direction.DOWN: positionY -= 1; break;
                case Direction.LEFT: positionX -= 1; break;
            }
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
                case Direction.UP:
                    if (roomMaxY > (positionY + 1)) 
                    {
                        //if (Room.getFieldType(positionX, positionY + 1) == FieldType.OBSTACLE)
                        if (room[positionX, positionY + 1] == FieldType.OBSTACLE)
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
                    if (roomMaxX > (positionX + 1)) 
                    {
                        //if (Room.getFieldType(positionX + 1, positionY) == FieldType.OBSTACLE)
                        if (room[positionX + 1, positionY] == FieldType.OBSTACLE)
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
                case Direction.DOWN:
                    if (0 < positionY) 
                    {
                        //if (Room.getFieldType(positionX, positionY - 1) == FieldType.OBSTACLE)
                        if (room[positionX, positionY - 1] == FieldType.OBSTACLE)
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
                    if (0 < positionX - 1) 
                    {
                        //if (Room.getFieldType(positionX - 1, positionY) == FieldType.OBSTACLE)
                        if (room[positionX - 1, positionY] == FieldType.OBSTACLE)
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

        public FieldType getFieldType(int x, int y)
        {
            return room[x, y];
            return Room.getFieldType(x, y);
        }

        public void setFieldType(int x, int y, FieldType ft)
        {
            room[x, y] = ft;
            Room.setFieldType(x ,y ,ft);
        }
    }
}