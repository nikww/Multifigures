using System;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multifigures.Figures
{
    sealed class Triangle : Shape 
    {
        public Triangle(int xx, int yy, Color cc) : base(xx, yy, cc)
        { }


        public override void Draw(DrawingContext context)
        {
            Pen pen = new Pen(new SolidColorBrush(c), 1, lineCap: PenLineCap.Square);

            Point p1 = new Point(x - r * Math.Sqrt(3) / 2, y + r / 2);
            Point p2 = new Point(x + r * Math.Sqrt(3) / 2, y + r / 2);
            Point p3 = new Point(x, y - r);
            context.DrawLine(pen, p1, p2);
            context.DrawLine(pen, p2, p3);
            context.DrawLine(pen, p3, p1);
        }
        public override bool IsInside(double curs_x, double curs_y)
        {
            throw new NotImplementedException();
        }
    }
}
