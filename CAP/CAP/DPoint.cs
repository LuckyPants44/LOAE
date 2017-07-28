using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace CAP
{
    class DPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public DPoint(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
            Z = 1;
        }

        public DPoint()
        {
            this.X = 0;
            this.Y = 0;
            Z = 1;
        }

        public DPoint(Point p)
        {
            X = p.X;
            Y = p.Y;
            Z = 1;
        }
    }
}
