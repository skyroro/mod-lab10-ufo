using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace mod_lab10_ufo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Ststistica();
            this.Invalidate();           
        }

        List<double> statisticError = new List<double>();
        double x1 = 50;
        double y1 = 50;
        double x2 = 550;
        double y2 = 650;

        private List<Point> Line(double x1, double y1, double x2, double y2, int accuracy)
        {
            List<Point> points = new List<Point>();

            double angle = -ImplementFunc.Arctg(accuracy, Math.Abs(y2 - y1) / Math.Abs(x2 - x1));
            double x = x1;
            double y = y1;
            double distance = Math.Sqrt(Math.Pow(Math.Abs(y2 - y1), 2) + Math.Pow(Math.Abs(x2 - x1), 2));
            double value = Math.Sqrt(Math.Pow(Math.Abs(y - y1), 2) + Math.Pow(Math.Abs(x - x1), 2));
            double step = 1;

            while (distance > value)
            {
                x += step * ImplementFunc.Cos(accuracy, angle);
                y -= step * ImplementFunc.Sin(accuracy, angle);

                Point point = new Point((int)x, (int)y);
                points.Add(point);

                distance = Math.Sqrt(Math.Pow(Math.Abs(y2 - y1), 2) + Math.Pow(Math.Abs(x2 - x1), 2));
                value = Math.Sqrt(Math.Pow(Math.Abs(y - y1), 2) + Math.Pow(Math.Abs(x - x1), 2));
            }

            return points;
        }

        private void Ststistica()
        {
            for (int i = 2; i < 10; i++)
            {
                int accuracy = i;

                List<Point> points = new List<Point>();
                points = Line(x1, y1, x2, y2, accuracy);

                double x = points[points.Count - 1].X;
                double y = points[points.Count - 1].Y;

                double error = Math.Sqrt(Math.Pow(Math.Abs(y2 - y), 2) + Math.Pow(Math.Abs(x2 - x), 2));
                statisticError.Add(error);
            }

            chart1.Legends.Clear();
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series.Add(".");
            chart1.Series[1].ChartType = SeriesChartType.Point;

            chart1.ChartAreas[0].AxisX.Title = "погрешность";
            chart1.ChartAreas[0].AxisY.Title = "количество членов ряда";
            
            for (int i = 0; i < statisticError.Count; i++)
            {
                chart1.Series[0].Points.AddXY((int)statisticError[i], i+2);
                chart1.Series[1].Points.AddXY((int)statisticError[i], i + 2);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.ScaleTransform(0.5f, 0.5f);

            Pen BluePen = new Pen(Color.DodgerBlue, 5);
            Pen RedPen = new Pen(Color.Red, 10);

            int error = 1;
            int accuracy = 0;

            for(int i = 0;i < statisticError.Count;i++)
            {
                if (statisticError[i] == error) accuracy = i;
            }

            g.DrawEllipse(RedPen, (int)x1, (int)y1, 2, 2);
            g.DrawEllipse(RedPen, (int)x2, (int)y2, 2, 2);

            List<Point> points = new List<Point>();
            points = Line(x1, y1, x2, y2, accuracy);

            foreach (Point point in points)
            {
                g.DrawEllipse(BluePen, point.X, point.Y, 1, 1);
            }

            double x = points[points.Count - 1].X;
            double y = points[points.Count - 1].Y;

            textBox1.Text = "Требуемая точность: " + error.ToString() + 
                "\r\nКоличесвто членов ряда: " + accuracy.ToString();
        }
    }
}