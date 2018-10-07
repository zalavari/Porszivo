using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Porszivo
{
    public class ProximitySensor
    {
        private Room Room { get; }

        public double getClosestObject(double angle)
        {
            double tang = Math.Tan(angle);
            int maxRange=10;
            int x0=Room.RobotX;
            int y0=Room.RobotY;
            int x1 = (int) Math.Round(Math.Cos(angle)*maxRange);
            int y1 = (int) Math.Round(Math.Sin(angle)*maxRange);
            
            //Legfeljebb csak az egyik síknegyeden működik egyelőre
            int xd = 1, yd = 1;
            while (xd*xd+yd*yd<maxRange*maxRange)
            {
                if (yd / xd > tang) xd++;
                else yd++;

                FieldType ft = Room.getFieldType(x0 + xd, y0 + yd);
                if (ft == FieldType.OBSTACLE)
                    return (Math.Sqrt(xd * xd + yd * yd));
            }
            return (Math.Sqrt(xd * xd + yd * yd));
        }

        public ProximitySensor(Room room)
        {
            Room = room;
        }

        public FieldType getFieldType(int x, int y)
        {
            FieldType ft = Room.getFieldType(x, y);

            //A robotot az akadályról értesítjük.
            //Ha nincs akadály, akkor a látószenzor nem tudja, hogy milyen a mező, tehát piszkosat mond
            //Ha a robot már járt ott, és valójában tiszta, azt neki kell tudnia
            if (ft == FieldType.OBSTACLE) return FieldType.OBSTACLE;
            else return FieldType.DIRTY;
        }
    }
}