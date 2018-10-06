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

        
        private Room Room { get; }
        
        // robot koordinátái - TODO: valószínűleg külön objektumban kellene tárolni őket 
        private int positionX;
        private int positionY;


        public Robot() 
        {
            radius = 1;
            positionX = 1;
            positionY = 1;
        }                   

        private ProximitySensor ProximitySensor { get; set; }

        private DrivingUnit DrivingUnit { get; set; }

        public Robot(ProximitySensor proximitySensor, DrivingUnit drivingUnit, Room room)
        {
            ProximitySensor = proximitySensor;
            DrivingUnit = drivingUnit;
            Room = room;
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
            throw new System.NotImplementedException();
        }

        /*
         * A robotnak lépnie kell. A rendelkezésre álló adatok alapján eldönti merre akar lépni, és végül a DrivingUnit-on keresztül lép.
         **/
        public void move() 
        {
            Direction direction = Direction.DOWN;
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
                    moved = true;
                }
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
                    if (Room.MaxY < (positionY + 1)) 
                    {
                        if (Room.getFieldType(positionX, positionY + 1) == FieldType.UNKNOWN) 
                        {
                            // Ha nem ismerjük még a kérdéses területet, akkor megnézzük mi van abban az irányban
                            double distance = ProximitySensor.getClosestObject(Math.PI * 0.5);
                            if (distance > 1) 
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else 
                        {
                            if (Room.getFieldType(positionX, positionY + 1) == FieldType.OBSTACLE)
                            {
                                return false;
                            }
                            else 
                            {
                                return true;
                            }
                        }
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                    break;
                case Direction.RIGHT:
                    if (Room.MaxX < (positionX + 1)) 
                    {
                        if (Room.getFieldType(positionX + 1, positionY) == FieldType.UNKNOWN) 
                        {
                            double distance = ProximitySensor.getClosestObject(Math.PI * 0);
                            if (distance > 1) 
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else 
                        {
                            if (Room.getFieldType(positionX + 1, positionY) == FieldType.OBSTACLE)
                            {
                                return false;
                            }
                            else 
                            {
                                return true;
                            }
                        }
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                    break;
                case Direction.DOWN:
                    if (0 < positionY) 
                    {
                        if (Room.getFieldType(positionX, positionY - 1) == FieldType.UNKNOWN) 
                        {
                            double distance = ProximitySensor.getClosestObject(Math.PI * 1.5);
                            if (distance > 1) 
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else 
                        {
                            if (Room.getFieldType(positionX, positionY - 1) == FieldType.OBSTACLE)
                            {
                                return false;
                            }
                            else 
                            {
                                return true;
                            }
                        }
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                    break;
                case Direction.LEFT:
                    if (0 < positionX - 1) 
                    {
                        if (Room.getFieldType(positionX - 1, positionY) == FieldType.UNKNOWN) 
                        {
                            double distance = ProximitySensor.getClosestObject(Math.PI * 1);
                            if (distance > 1) 
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else 
                        {
                            if (Room.getFieldType(positionX - 1, positionY) == FieldType.OBSTACLE)
                            {
                                return false;
                            }
                            else 
                            {
                                return true;
                            }
                        }
                        return true;
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
            Room.getFieldType(x, y);
        }

        public void setFieldType(int x, int y, FieldType ft)
        {
            Room.setFieldType(x ,y ,ft);
        }
    }
}