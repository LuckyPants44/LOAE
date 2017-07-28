using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP
{
    class Edge
    {
        public double x1 { get; private set; }
        public double y1 { get; private set; }
        public double x2 { get; private set; }
        public double y2 { get; private set; }

        private double[] vector = new double[2];

        public Edge(Edge e)
        {
            x1 = e.x1;
            x2 = e.x2;
            y1 = e.y1;
            y2 = e.y2;
            vector[0] = x2 - x1;
            vector[1] = y2 - y1;
        }

        public Edge(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            vector[0] = x2 - x1;
            vector[1] = y2 - y1;
        }

        public double[] GetVector()
        {
            return vector;
        }
    }
}
