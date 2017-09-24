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
                FileStream file = new FileStream("C://Users//1//Desktop//Лабы Быкова//5//CAP//CAP//Points.txt", FileMode.Open);
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
            //Добавление и удаление активных ребер
            for (int y = 0; y < pictureBox.Height; y++)
            {
                for (int j = 0; j < newEdges.Count; j++)
                {
                    //Добавление
                    if (Convert.ToInt32(pictureBox.Height / 2 - newEdges[j].y1 * scale) == y)
                    {
                        ActiveEdge.Add(newEdges[j]);
                    }
                    //Удаление
                    if (Convert.ToInt32(pictureBox.Height / 2 - newEdges[j].y2 * scale) == y)
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
                if (ActiveEdge.Count != 0)
                {
                    List<Point> resultPoints = new List<Point>();
                    foreach (Edge ed in ActiveEdge)
                    {
                        Point bufPoint1 = new Point(Convert.ToInt32(pictureBox.Width / 2 + ed.x1 * scale), Convert.ToInt32(pictureBox.Height / 2 - ed.y1 * scale));
                        Point bufPoint2 = new Point(Convert.ToInt32(pictureBox.Width / 2 + ed.x2 * scale), Convert.ToInt32(pictureBox.Height / 2 - ed.y2 * scale));
                        for (int x = 0; x < pictureBox.Width; x++)
                        {
                            //Уравнение прямой проходящей через 2 точки
                            if (((y - bufPoint1.Y) * (bufPoint2.X - bufPoint1.X) - (x - bufPoint1.X) * (bufPoint2.Y - bufPoint1.Y))<0.001)
                            {
                                resultPoints.Add(new Point(x, y));
                                break;
                            }
                        }
                    }
                    //Сортировка по X
                    for (int i = 0; i < resultPoints.Count; i++)
                    {
                        for (int j = 0; j < resultPoints.Count - i - 1; j++)
                        {
                            if (resultPoints[j].X > resultPoints[j + 1].X)
                            {
                                Point p = new Point(resultPoints[j].X, resultPoints[j].Y);
                                resultPoints[j] = resultPoints[j + 1];
                                resultPoints[j + 1] = p;
                            }
                        }
                    }
                    //Вывод
                    for (int i = 0; i < resultPoints.Count; i += 2)
                    {
                        if (i + 1 != resultPoints.Count)
                            gr.DrawLine(Pens.Red, resultPoints[i], resultPoints[i + 1]);
                    }
                }
                DrawFigure();
                pictureBox.Image = bmp;
            }
        }
    }
}
