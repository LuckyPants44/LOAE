using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CAP
{
    public partial class Form1 : Form
    {
        int scale = 40;
        Bitmap bmp;
        List<Point> points;
        List<Edge> edges;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bmp;
            DrawGrid();
        }

        private void DrawGrid()
        {
            Graphics gr = Graphics.FromImage(bmp);
            Pen axesPen = new Pen(Brushes.Blue, 4);
            //Axes
            gr.DrawLine(axesPen, new Point(0, pictureBox.Height / 2), new Point(pictureBox.Width, pictureBox.Height / 2));
            gr.DrawLine(axesPen, new Point(pictureBox.Width / 2, 0), new Point(pictureBox.Width / 2, pictureBox.Height));
            //Grid
            for (int i = scale; i < pictureBox.Width / 2; i += scale)
            {
                gr.DrawLine(Pens.Black, new Point(pictureBox.Width / 2 + i, 0), new Point(pictureBox.Width / 2 + i, pictureBox.Height));
                gr.DrawLine(Pens.Black, new Point(pictureBox.Width / 2 - i, 0), new Point(pictureBox.Width / 2 - i, pictureBox.Height));
            }
            for (int i = scale; i < pictureBox.Height / 2; i += scale)
            {
                gr.DrawLine(Pens.Black, new Point(0, pictureBox.Height / 2 + i), new Point(pictureBox.Width, pictureBox.Height / 2 + i));
                gr.DrawLine(Pens.Black, new Point(0, pictureBox.Height / 2 - i), new Point(pictureBox.Width, pictureBox.Height / 2 - i));
            }
            pictureBox.Image = bmp;
        }

        private void InsertPointsButton_Click(object sender, EventArgs e)
        {
            string[] buf;
            points = new List<Point>();
            try
            {
                FileStream file = new FileStream("c:\\users\\1\\documents\\visual studio 2015\\Projects\\CAP\\CAP\\Points.txt", FileMode.Open);
                StreamReader sr = new StreamReader(file);
                buf = sr.ReadToEnd().Split(' ', '\n', '\r');
                sr.Close();
                for (int i = 0; i < buf.Length - 1; i += 3)
                {
                    points.Add(new Point(Convert.ToInt32(buf[i]), Convert.ToInt32(buf[i + 1])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия файла!" + ex.Message);
            }
            DrawFigure();
        }

        private void DrawFigure()
        {
            Graphics gr = Graphics.FromImage(bmp);
            Pen pen = new Pen(Brushes.Red, 2);
            edges = new List<Edge>();

            for (int i = 0; i < points.Count; i++)
            {
                if (i != points.Count - 1)
                {
                    edges.Add(new Edge(points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y));
                    gr.DrawLine(new Pen(Brushes.Green, 2), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i + 1].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i + 1].Y * scale)));
                }
                else
                {
                    edges.Add(new Edge(points[i].X, points[i].Y, points[0].X, points[0].Y));
                    gr.DrawLine(new Pen(Brushes.Green, 2), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[0].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[0].Y * scale)));
                }
            }
            pictureBox.Image = bmp;

        }

        //Метод САР
        //ToDO: Растеризовать
        private void FillingFigureButton_Click(object sender, EventArgs e)
        {
            Graphics gr = Graphics.FromImage(bmp);
            List<Edge> ActiveEdge = new List<Edge>();
            List<Edge> newEdges = new List<Edge>();
            //Первая сортировка(Точки граней от верхней к нижней в одной грани)
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].y2 > edges[i].y1)
                {
                    newEdges.Add(new Edge(edges[i].x2, edges[i].y2, edges[i].x1, edges[i].y1));
                }
                else
                {
                    newEdges.Add(edges[i]);
                }
            }
            //Грани отсартированы от верхних к нижним
            for (int i = 0; i < edges.Count - 1; i++)
            {
                Edge bufEdge;
                for (int j = 0; j < edges.Count - i; j++)
                {
                    if (newEdges[i].y1 < newEdges[i + 1].y1)
                    {
                        bufEdge = new Edge(newEdges[i]);
                        newEdges[i] = newEdges[i + 1];
                        newEdges[i + 1] = bufEdge;
                    }
                }
            }
            //Нужны преобразования!!!
            for (int i = 0; i < pictureBox.Height; i++)
            {
                for (int j = 0; j < newEdges.Count; j++)
                {
                    //Добавление
                    if (Convert.ToInt32(pictureBox.Height / 2 - newEdges[j].y1 * scale) == i)
                    {
                        ActiveEdge.Add(newEdges[j]);
                    }
                    //Удаление
                    if (Convert.ToInt32(pictureBox.Height / 2 - newEdges[j].y2 * scale) == i)
                    {
                        foreach (Edge edge in ActiveEdge)
                        {
                            if (edge == newEdges[j])
                            {
                                ActiveEdge.Remove(edge);
                                break;
                            }
                        }
                    }
                }
                double[] vector1 = new double[2];
                double[] vector2 = new double[2];
                for (int t = 0; t < ActiveEdge.Count; t += 2)
                {
                    vector1 = ActiveEdge[t].GetVector();
                    vector2 = ActiveEdge[t + 1].GetVector();
                    Point p1 = new Point();
                    Point p2 = new Point();
                    for (int k = 1; k < 100; k++)
                    {
                        if (Math.Abs(pictureBox.Height / 2 - (ActiveEdge[t].y1-k*vector1[1]/100) * scale - i)<=1)
                        {
                            p1 = new Point(Convert.ToInt32(pictureBox.Width / 2 + (ActiveEdge[t].x1 - k*vector1[0] /100) * scale), i);
                        }
                        if (Math.Abs(pictureBox.Height / 2 - (ActiveEdge[t+1].y1 - k * vector2[1] / 100) * scale - i) <= 1)
                        {
                            p2 = new Point(Convert.ToInt32(pictureBox.Width / 2 + (ActiveEdge[t+1].x1 - k*vector2[0] /100) * scale), i);
                        }
                    }
                    if((p1.X !=0 && p1.Y !=0) && (p2.X !=0 && p2.Y!=0))
                        gr.DrawLine(new Pen(Brushes.Black, 1), p1, p2);
                }
            }
            pictureBox.Image = bmp;
        }
    }
}
