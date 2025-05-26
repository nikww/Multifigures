using System;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multifigures.Figures
{
    public sealed class Triangle : Shape 
    {
        public Triangle(double xx, double yy) : base(xx, yy)
        { }

        private Point point1, point2, point3;
        public override void Draw(DrawingContext context)
        {
            Pen pen = new Pen(new SolidColorBrush(c), 1, lineCap: PenLineCap.Square);

            point1 = new Point(x - r * Math.Sqrt(3) / 2, y + r / 2);
            point2 = new Point(x + r * Math.Sqrt(3) / 2, y + r / 2);
            point3 = new Point(x, y - r);
            context.DrawLine(pen, point1, point2);
            context.DrawLine(pen, point2, point3);
            context.DrawLine(pen, point3, point1);
        }
        private double Get_Area(Point p1, Point p2, Point curs)
        {
            double a = Point.Distance(p1, p2), b = Point.Distance(p1, curs), c = Point.Distance(p2, curs);
            double p = (a + b + c) / 2;
              return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        public override bool IsInside(double curs_x, double curs_y)
        {
            double Area = 0.75 * r * r * Math.Sqrt(3);
            Point curs = new Point(curs_x, curs_y);
            if (Math.Abs(Area - (Get_Area(point1, point2, curs) + Get_Area(point2, point3, curs) + Get_Area(point1, point3, curs))) <= 50)
            {
                return true;
            }
            return false;
        }
        
    }
}
