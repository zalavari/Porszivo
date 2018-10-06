using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Porszivo
{
    public partial class Form1 : Form
    {
        Robot robot;
        public Form1()
        {
            InitializeComponent();
            InitializeSimulation();
        }

        private void InitializeSimulation()
        {
            Room room = new Room("Room.txt");
            ProximitySensor prSens = new ProximitySensor(room);
            DrivingUnit drivingUnit = new DrivingUnit(room);

            robot = new Robot(prSens, drivingUnit, room);
            Console.WriteLine("asd");
            Thread t = new Thread(new ThreadStart(runSimulation));
            t.Start();
        }

        private void runSimulation()
        {
            const int tickTime = 1000;

            // Később lehet feltételhez kötni, pl szoba x %-a ki van-e takarítva
            while (true) 
            {
                // Robot léptetése
                robot.move();
                Debug.WriteLine("Robot léptetése");

                // Aktuális állapot kirakzolása
                // TODO - kirajzolós függvény meghívása
                Debug.WriteLine("Rajzolás");
                Thread.Sleep(tickTime);
            }
        }
    }
}
