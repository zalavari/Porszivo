using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Porszivo
{
    public class Robot
    {
        private ProximitySensor ProximitySensor
        {
            get => default(ProximitySensor);
            set
            {
            }
        }

        private DrivingUnit DrivingUnit
        {
            get => default(DrivingUnit);
            set
            {
            }
        }

        public Robot(ProximitySensor proximitySensor, DrivingUnit drivingUnit)
        {
            ProximitySensor = proximitySensor;
            DrivingUnit = drivingUnit;
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

        public FieldType getFieldType(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void setFieldType(int x, int y, FieldType ft)
        {
            throw new System.NotImplementedException();
        }
    }
}