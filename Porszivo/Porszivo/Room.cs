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
        }

        public FieldType getFieldType(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public Direction moveRobot(Direction direction)
        {
            throw new System.NotImplementedException();
        }

        public void setFieldType()
        {
            throw new System.NotImplementedException();
        }
    }
}
