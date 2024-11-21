using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cube
{
    internal class Point
    {
        public float X {  get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString() => $"({X.ToString()}, {Y.ToString()}, {Z.ToString()})";
    }
}
