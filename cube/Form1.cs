using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cube
{
    public partial class Form1 : Form
    {
        Pen p = new Pen(Color.White, (float)1.5);
        Brush b = new SolidBrush(Color.White);
        float cx, cy, cz;
        List<Point> cube;
        float angle = (float) Math.PI / 120;
        Timer t;
        public Form1()
        {
            InitializeComponent();
            //DoubleBuffered = true;
            BackColor = Color.Black;

            cx = Width / 2;
            cy = Height / 2;
            cz = 0;

            cube = new List<Point>();
            cube.Add(new Point(cx, cy, 0));
            cube.Add(new Point(cube[0].X + 100, cube[0].Y, cube[0].Z));

            cube.Add(new Point(cube[0].X + 100, cube[0].Y + 100, cube[0].Z));
            cube.Add(new Point(cube[0].X, cube[0].Y + 100, cube[0].Z));
            
            
            t = new Timer();
            t.Interval = 20;
            t.Tick += T_Tick;
            t.Start();
        }
        private void T_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < cube.Count; i++)
            {
                float X = (float)((cube[i].X - cx) * Math.Cos(angle) - (cube[i].Y - cy) * Math.Sin(angle));
                float Y = (float)((cube[i].X - cx) * Math.Sin(angle) + (cube[i].Y - cy) * Math.Cos(angle));

                cube[i] = new Point(X + cx, Y + cy, 0);
            }
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < cube.Count - 1; i++)
            {
                DrawLine(cube[i], cube[i + 1], e);
            }
            DrawLine(cube[0], cube[3], e);
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
