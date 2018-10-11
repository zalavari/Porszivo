using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Porszivo
{
    public class ProximitySensor
    {
        public const int maxRange = 10;
        private Room Room { get; }

        private FieldType[,] roomMtx;

        public void scanRoom()
        {

            double delta = 0.01;
            for (double angle = 0; angle < 2 * Math.PI; angle += delta)
            {

                double tang = Math.Tan(angle);
                int x0 = Room.RobotX;
                int y0 = Room.RobotY;
                int x1 = (int)Math.Round(Math.Cos(angle) * maxRange);
                int y1 = (int)Math.Round(Math.Sin(angle) * maxRange);
                int x = 0, y = 0;
                int iranyX = Math.Sign(x1);
                int iranyY = Math.Sign(y1);
                x1 = Math.Abs(x1);
                y1 = Math.Abs(y1);
                bool endMemory = false;
                while (!endMemory && (x < x1 || y < y1))
                {
                    double xRatio = (double)x / x1;
                    double yRatio = (double)y / y1;
                    if (xRatio > yRatio)
                        y++;
                    else
                        x++;


                    if (x0 + x * iranyX < 0 || x0 + x * iranyX >= Room.MaxX || y0 + y * iranyY < 0 || y0 + y * iranyY >= Room.MaxY)
                        endMemory = true;
                    else
                    {

                        FieldType ft =  Room.getFieldType(x0 + x * iranyX, y0 + y * iranyY);
                        if (ft == FieldType.OBSTACLE)
                        {
                            endMemory = true;
                            roomMtx[x0 + x * iranyX, y0 + y * iranyY] = ft;
                        }
                        else
                            roomMtx[x0 + x * iranyX, y0 + y * iranyY] = FieldType.DIRTY;

                    }
                }
            }


        }

        public ProximitySensor(Room room)
        {
            Room = room;

            roomMtx = new FieldType[room.MaxX, room.MaxY];
            for (int i = 0; i < room.MaxX; ++i) for (int j = 0; j < room.MaxY; ++j) { roomMtx[i, j] = FieldType.UNKNOWN; }

        }

        public FieldType getFieldType(int x, int y)
        {
            return roomMtx[x, y];

        }


    }
}