using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace cube
{
    public partial class Form1 : Form
    {
        Brush b = new SolidBrush(Color.BlueViolet);

        List<Point> cube;
        int s = 120; //define size of the cube
        (int, int) size;

        float cx, cy, cz;
        float dx, dy, dz;
        float length;
        float m;
        float tempx, tempy = 0;

        List<(int, int)> connections;
        float rad;
        Timer t;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            BackColor = Color.Black;

            size = (s, s);

            //center of window
            cx = Width / 2;
            cy = Height / 2;
            cz = 0;

            //map of connections betweem vertices
            cube = new List<Point>();
            connections = new List<(int, int)> { 
                (0, 1), 
                (0, 2), 
                (0, 4), 
                (1, 3), 
                (1, 5), 
                (2, 3), 
                (2, 6),
                (3, 7),
                (4, 5),
                (4, 6),
                (5, 7),
                (6, 7)
            };

            //actuall points
            cube.Add(new Point(cx - size.Item1 / 2, cy + size.Item2 / 2, cz + size.Item1 / 2));
            cube.Add(new Point(cube[0].X + size.Item1, cube[0].Y, cube[0].Z));
            cube.Add(new Point(cube[0].X, cube[0].Y - size.Item2, cube[0].Z));
            cube.Add(new Point(cube[0].X + size.Item1, cube[0].Y - size.Item2, cube[0].Z));
            
            cube.Add(new Point(cube[0].X, cube[0].Y, cube[0].Z - size.Item1));
            cube.Add(new Point(cube[0].X + size.Item1, cube[0].Y, cube[0].Z - size.Item1));
            cube.Add(new Point(cube[0].X, cube[0].Y - size.Item2, cube[0].Z - size.Item1));
            cube.Add(new Point(cube[0].X + size.Item1, cube[0].Y - size.Item2, cube[0].Z - size.Item1));

            //instead of using delay, timer would make it possible to "rotate a cube throughout the time"
            t = new Timer();
            t.Interval = 100;
            t.Tick += tick;

            Resize += resize;

            t.Start();
        }
        private void tick(object sender, EventArgs e) //what happens after 1ms of timer lasted
        {
            for (int i = 0; i < cube.Count; i++)
            {
                rotate(cube[i], 0.01f, 0.04f, 0.02f);
            }
            Invalidate();
        }
        private void rotate(Point p, float ax = 1, float ay = 1, float az = 1) // the rotation function using rotation matrices in 3d
        {
            tempy = p.Y;
            rad = ax;
            p.Y = (float)((p.Y - cy) * Math.Cos(rad) - (p.Z - cz) * Math.Sin(rad) + cy);
            p.Z = (float)((tempy - cy) * Math.Sin(rad) + (p.Z - cz) * Math.Cos(rad) + cz);

            tempx = p.X;
            rad = ay;
            p.X = (float)((p.X - cx) * Math.Cos(rad) + (p.Z - cz) * Math.Sin(rad) + cx);
            p.Z = (float)((p.Z - cz) * Math.Cos(rad) - (tempx - cx) * Math.Sin(rad) + cz);

            tempx = p.X;
            rad = az;
            p.X = (float)((p.X - cx) * Math.Cos(rad) - (p.Y - cy) * Math.Sin(rad) + cx);
            p.Y = (float)((tempx - cx) * Math.Sin(rad) + (p.Y - cy) * Math.Cos(rad) + cy);
        }
        protected override void OnPaint(PaintEventArgs e) //draw pixels
        {
            base.OnPaint(e);
            for (int i = 0; i < connections.Count; i++) draw_line(cube[connections[i].Item1], cube[connections[i].Item2], e);
        }
        private void draw_line(Point p1, Point p2, PaintEventArgs e) //
        {
            dx = p2.X - p1.X;
            dy = p2.Y - p1.Y;
            dz = p2.Z - p1.Z;
            length = (float)Math.Sqrt(dx * dx + dy * dy);
            m = (float)Math.Atan2(dy, dx);
            for (int i = 0; i < length; i++) e.Graphics.FillRectangle(b, p1.X + (float)(Math.Cos(m) * i), p1.Y + (float)(Math.Sin(m) * i), 1, 1);
        }
        private void resize(object sender, EventArgs e)
        {
            cx = Width / 2;
            cy = Height / 2;
            cz = 0;

            cube[0] = (new Point(cx - size.Item1 / 2, cy + size.Item2 / 2, cz + size.Item1 / 2));
            cube[1] = (new Point(cube[0].X + size.Item1, cube[0].Y, cube[0].Z));
            cube[2] = (new Point(cube[0].X, cube[0].Y - size.Item2, cube[0].Z));
            cube[3] = (new Point(cube[0].X + size.Item1, cube[0].Y - size.Item2, cube[0].Z));
                
            cube[4] = (new Point(cube[0].X, cube[0].Y, cube[0].Z - size.Item1));
            cube[5] = (new Point(cube[0].X + size.Item1, cube[0].Y, cube[0].Z - size.Item1));
            cube[6] = (new Point(cube[0].X, cube[0].Y - size.Item2, cube[0].Z - size.Item1));
            cube[7] = (new Point(cube[0].X + size.Item1, cube[0].Y - size.Item2, cube[0].Z - size.Item1));
        }
    }
}
