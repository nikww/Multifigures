using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Multifigures.Figures;
using System.Threading;

namespace Multifigures
{
    public class CustomControl : UserControl
    {
        private double prevx, prevy;
        public List<Shape> Figures = [
            new Circle(100, 100, Colors.AliceBlue),
            new Circle(250, 250, Colors.AliceBlue),
            new Circle (300, 300, Colors.AliceBlue),
             new Circle (300, 300, Colors.AliceBlue)
        ];
        

        public void Click(double cx, double cy, Avalonia.Input.PointerPoint point)
        {
            if (point.Properties.IsLeftButtonPressed)
            {
                bool found = false;
                foreach (var f in Figures)
                {
                    if (!f.IsInside(cx, cy)) continue;

                    prevx = cx; prevy = cy; found = true;
                    f.moving = true;
                }
                if (!found)
                {
                    Figures.Add(new Triangle(cx, cy, Colors.AliceBlue));
                }
            }
            if (point.Properties.IsRightButtonPressed) {
                Figures.Reverse();
                foreach (var f in Figures.ToList())
                {
                    if (!f.IsInside(cx, cy)) continue;
                    Figures.Remove(f);
                    break;
                }
                Figures.Reverse();
            }
        }

        public void Move(double cx, double cy)
        {
            foreach (var f in Figures)
            {
                if (!f.moving) continue;
                f.X += cx - prevx; f.Y += cy - prevy;
            }
            prevx = cx; prevy = cy;
            InvalidateVisual();
        }
        
        public void Release()
        {
            foreach (var f in Figures)
            {
                f.moving = false;
            }
        }

        private void DrawConvexHull(DrawingContext context)
        {
            int i = 0;
            foreach (Shape s in Figures)
            {
                int j = 0;
                foreach (Shape s2 in Figures)
                {
                    if (j <= i) { j++; continue; }
                    int m = 0, cntup = 0, cntdown = 0;
                    
                    if (s.X == s2.X)
                    {
                        foreach (Shape s3 in Figures)
                        {
                            if (i == m || j == m || (s.X == s3.Y && s.X == s3.X) || (s2.X == s3.X && s2.Y == s3.Y)) { m++; continue; }
                            if (s3.X > s.X) cntup++;
                            else if (s3.X < s.X) cntdown++; 
                            
                            m++;
                        }
                    }
                    else
                    {
                        double k = (s.Y - s2.Y) / (s.X - s2.X), b = s.Y - k * s.X; m = 0;
                        foreach (Shape s3 in Figures)
                        {
                            if (i == m || j == m || (s.X == s3.Y && s.X == s3.X) || (s2.X == s3.X && s2.Y == s3.Y)) { m++; continue; }
                            double y = k * s3.X + b;
                            if (s3.Y > y) cntup++;
                            else if (s3.Y < y) cntdown++;
                            
                            m++;
                        }
                    }

                    if (cntup == 0 || cntdown == 0)
                    {
                        Pen pen = new Pen(new SolidColorBrush(Colors.Beige), 1, lineCap: PenLineCap.Square);
                        context.DrawLine(pen, new Point(s.X, s.Y), new Point(s2.X, s2.Y));
                    }
                    j++;
                }
                i++;
            }
        }

        public override void Render(DrawingContext context)
        {
            foreach (Shape s in Figures)
            {
                s.Draw(context);
            }
            
            if (Figures.Count >= 3)
            {
                DrawConvexHull(context);
            }
        }
    }
}
