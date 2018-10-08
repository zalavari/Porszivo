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
        Room room;
        public Form1()
        {
            InitializeComponent();
            InitializeSimulation();
        }

        private void InitializeSimulation()
        {
            room = new Room("Room.txt");
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
                Invalidate();
                // TODO - kirajzolós függvény meghívása
                Debug.WriteLine("Rajzolás");
                Thread.Sleep(tickTime);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int TileSize = 10;
            for (int i = 0; i < room.MaxX; i++)
                for (int j = 0; j < room.MaxY; j++)
                {
                    Rectangle Rect = new Rectangle((i * TileSize), (j * TileSize), TileSize, TileSize);
                    if (room.getFieldType(i,j)==FieldType.OBSTACLE)
                        e.Graphics.FillRectangle(Brushes.Red,Rect);
                    else
                        e.Graphics.FillRectangle(Brushes.Gray, Rect);
                }

            Rectangle RectPorszivo=new Rectangle((room.RobotX * TileSize), (room.RobotY * TileSize), TileSize, TileSize);
            e.Graphics.FillRectangle(Brushes.Blue, RectPorszivo);


            int offsetX = 33 * TileSize;

            for (int i = 0; i < room.MaxX; i++)
                for (int j = 0; j < room.MaxY; j++)
                {
                    Rectangle Rect = new Rectangle((i * TileSize)+offsetX, (j * TileSize), TileSize, TileSize);
                    if (robot.getFieldType(i, j) == FieldType.OBSTACLE)
                        e.Graphics.FillRectangle(Brushes.Red, Rect);
                    else if (robot.getFieldType(i, j) == FieldType.DIRTY)
                        e.Graphics.FillRectangle(Brushes.Gray, Rect);
                    else if (robot.getFieldType(i, j) == FieldType.CLEAN)
                        e.Graphics.FillRectangle(Brushes.White, Rect);
                    else
                        e.Graphics.FillRectangle(Brushes.Black, Rect);
                }

            RectPorszivo = new Rectangle((robot.positionX * TileSize)+offsetX, (robot.positionY * TileSize), TileSize, TileSize);
            e.Graphics.FillRectangle(Brushes.Blue, RectPorszivo);


        }
    }
}
