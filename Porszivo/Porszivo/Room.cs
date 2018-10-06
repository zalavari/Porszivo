using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Porszivo
{
    public class Room
    {
        public int MaxX { get; set; }

        public int MaxY { get; set; }

        public int RobotX { get; set; }

        public int RobotY { get; set; }

        private FieldType[,] room;

        public Room(String input) 
        {
            /**
             * Szoba beolvasása fájlból.
             * Fájl leírása:
             * MaxX MaxY
             * RobotX RobotY
             * Szoba leírása MaxY sorban soronként MaxX karakter: 1 - akadály, 0 üres terület
             **/

            string[] lines = System.IO.File.ReadAllLines("room.txt");


            // Pálya méretének beolvasása
            string[] row = lines[0].Split(' ');
            MaxX = Int32.Parse(row[0]);
            MaxY = Int32.Parse(row[1]);

            // Robot helyének beolvasása
            row = lines[1].Split(' ');
            RobotX = Int32.Parse(row[0]);
            RobotY = Int32.Parse(row[1]);

            // Pálya beolvasása
            for(int i = 0; i < MaxY; i++)
            {
                row = lines[i + 2].Split(' ');
                for(int j = 0; j < MaxX; j++)
                {
                    if(row[j] == "0")
                    {
                        room[i, j] = FieldType.DIRTY;
                    }
                    else
                    {
                        room[i, j] = FieldType.OBSTACLE;
                    }
                }
            }

            /*
            // Amíg ez nincs meg, létrehozok egy statikus pályát.
            MaxX = 30;
            MaxY = 30;
            
            room = new FieldType[MaxX, MaxY];
            // A pálya széle végig akadály, ezen kívül egy csík akadály van y=15nél 5 szélességben
            for (int i = 0; i < MaxX; ++i) 
            {
                for (int j = 0; j < MaxY; ++j)
                {
                    if (i == 0 || j == 0 || i == (MaxX - 1) || j == (MaxY - 1)) 
                    {
                        room[i, j] = FieldType.OBSTACLE;
                    }
                    else
                    {
                        room[i, j] = FieldType.DIRTY;
                    }
                }
            }

            for (int i = 1; i < 6; ++i)
            {
                room[i, 15] = FieldType.OBSTACLE;
            }
            */
        }

        public FieldType getFieldType(int x, int y)
        {
            return room[x,y];
        }

        public Direction moveRobot(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    if (RobotX + 1 <= room.GetLength(0) && room[RobotX + 1,RobotY] != FieldType.OBSTACLE)
                    {
                        RobotX += 1;
                    }
                    break;
                case Direction.DOWN:
                    if (RobotX - 1 >= 0 && room[RobotX - 1, RobotY] != FieldType.OBSTACLE)
                    {
                        RobotX -= 1;
                    }
                    break;
                case Direction.RIGHT:
                    if (RobotY + 1 <= room.GetLength(1) && room[RobotX, RobotY + 1] != FieldType.OBSTACLE)
                    {
                        RobotY += 1;
                    }
                    break;
                case Direction.LEFT:
                    if (RobotX - 1 >= 0 && room[RobotX, RobotY - 1] != FieldType.OBSTACLE)
                    {
                        RobotY -= 1;
                    }
                    break;
            }        
            return direction;
        }

        public void setFieldType(int x, int y, FieldType ft)
        {
            room[x, y] = ft;
        }
    }
}
