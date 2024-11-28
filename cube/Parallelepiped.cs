using System;
using System.Collections.Generic;

namespace engine
{
    internal class Parallelepiped
    {
        private List<Point> vertices;
        private List<(int, int)> edges;
        private List<(int, int, int, int)> faces;
        private float size;
        private float rad; //rotating angle
        private float cx, cy, cz;
        public Parallelepiped() {
            
        }
        private void On_Tick()
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Rotate(vertices[i], 0, 0.04f, 0);
            }
        }
        private void Rotate(Point p, float ax, float ay, float az)
        {
            float tempy = p.Y;
            rad = ax;
            p.Y = (float)((p.Y - cy) * Math.Cos(rad) - (p.Z - cz) * Math.Sin(rad) + cy);
            p.Z = (float)((tempy - cy) * Math.Sin(rad) + (p.Z - cz) * Math.Cos(rad) + cz);

            float tempx = p.X;
            rad = ay;
            p.X = (float)((p.X - cx) * Math.Cos(rad) + (p.Z - cz) * Math.Sin(rad) + cx);
            p.Z = (float)((p.Z - cz) * Math.Cos(rad) - (tempx - cx) * Math.Sin(rad) + cz);

            tempx = p.X;
            rad = az;
            p.X = (float)((p.X - cx) * Math.Cos(rad) - (p.Y - cy) * Math.Sin(rad) + cx);
            p.Y = (float)((tempx - cx) * Math.Sin(rad) + (p.Y - cy) * Math.Cos(rad) + cy);
        }
    }
}
