using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Porszivo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeSimulation();
        }

        private void InitializeSimulation()
        {
            Room room = new Room();            
            ProximitySensor prSens = new ProximitySensor(room);
            DrivingUnit drivingUnit = new DrivingUnit(room);

            Robot robot = new Robot(prSens, drivingUnit);
        }
    }
}
