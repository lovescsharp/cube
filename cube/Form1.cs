using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace cube
{
    public partial class Form1 : Form
    {
        Pen p = new Pen(Color.White, (float)1.5);
        Brush b = new SolidBrush(Color.White);
        float cx, cy, cz;
        List<Point> cube;
        List<(int, int)> connections;
        float angle = (float) Math.PI / 120;
        Timer t;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            BackColor = Color.Black;

            cx = 150;
            cy = 150;
            cz = 150;

            cube = new List<Point>();
            /*
            cube.Add(new Point(cx -  50, cy - 50, cz - 50));
            cube.Add(new Point(cube[0].X + 100, cube[0].Y, cube[0].Z));
            cube.Add(new Point(cube[0].X + 100, cube[0].Y + 100, cube[0].Z));
            cube.Add(new Point(cube[0].X, cube[0].Y + 100, cube[0].Z));
            */
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
            cube.Add(new Point(100, 100, 100));
            cube.Add(new Point(200, 100, 100));
            cube.Add(new Point(100, 200, 100));
            cube.Add(new Point(200, 200, 100));

            cube.Add(new Point(100, 100, 200));
            cube.Add(new Point(200, 100, 200));
            cube.Add(new Point(100, 200, 200));
            cube.Add(new Point(200, 200, 200));

            t = new Timer();
            t.Interval = 20;
            t.Tick += T_Tick;
            t.Start();
        }
        private void T_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < cube.Count; i++) Rotate(cube[i], 0.01f, 0.02f, 0.04f);
        }
        private void Rotate(Point p, float x = 1, float y = 1, float z = 1)
        {
            float rad = 0;

            rad = x;
            p.Y = (float)((p.Y - cy) * Math.Cos(rad) - (p.Z - cz) * Math.Sin(rad));
            p.Z = (float)((p.Y - cy) * Math.Sin(rad) + (p.Z - cz) * Math.Cos(rad));

            rad = y;
            p.X = (float)((p.X - cx) * Math.Cos(rad) + (p.Z - cz) * Math.Sin(rad));
            p.Z = (float)(-(p.X - cx) * Math.Sin(rad) + (p.Z - cz) * Math.Cos(rad));

            rad = z;
            p.X = (float)((p.X - cx) * Math.Cos(rad) - (p.Y - cy) * Math.Sin(rad));
            p.Y = (float)((p.Y - cy) * Math.Sin(rad) + (p.Y - cy) * Math.Cos(rad));
            Invalidate();

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < connections.Count; i++)
            {
                DrawLine(cube[connections[i].Item1], cube[connections[i].Item2], e);
            }
        }
        private void DrawLine(Point p1, Point p2, PaintEventArgs e)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float dz = p2.Z - p1.Z;
            float length = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
            float a = (float)Math.Atan2(dy, dx);
            for (int i = 0; i < length; i++)
            {
                e.Graphics.FillRectangle(b, p1.X + (float)(Math.Cos(a) * i), p1.Y + (float)(Math.Sin(a) * i), 1, 1);
            }
        }
    }
}
