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
using System.IO.Pipelines;

namespace Multifigures
{
    public class CustomControl : UserControl
    {
        private bool click_hull = false;
        public List<Shape> Figures = [
            
        ];
        public List<Shape> Hull = [];
        
        

        public void Click(double cx, double cy, Avalonia.Input.PointerPoint point)
        {
            if (point.Properties.IsLeftButtonPressed)
            {
                bool found = false;
                foreach (var f in Figures)
                {
                    if (!f.IsInside(cx, cy)) continue;

                    f.prevx = cx; f.prevy = cy; found = true;
                    f.moving = true;
                }
                if (!found)
                {
                    Triangle t = new Triangle(cx, cy, Colors.AliceBlue);
                    Figures.Add(t); ConvexHull();
                    if (!Hull.Contains(t) && Figures.Count >= 3) click_hull = true;
                    else click_hull = false;
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

        public void Move(double cx, double cy) {
            if (click_hull)
            {
                foreach (var f in Figures) f.moving = true;
            }
            foreach (var f in Figures)
            {
                if (f.moving)
                {
                    f.X += cx - f.prevx;
                    f.Y += cy - f.prevy;
                }      
            }
            foreach(var f in Figures) { 
                f.prevx = cx; f.prevy = cy;
            }
            InvalidateVisual();

        }
        
        public void Release()
        {
            click_hull = false;
            foreach (var f in Figures)
            {
                f.moving = false;
            }
            ConvexHull();
        }


        private void ConvexHull()
        {
            Hull.Clear();
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
                            if (i == m || j == m) { m++; continue; }
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
                            if (i == m || j == m) { m++; continue; }
                            double y = k * s3.X + b;
                            if (s3.Y > y) cntup++;
                            else if (s3.Y < y) cntdown++;
                            m++;
                        }
                    }

                    if (cntup == 0 || cntdown == 0)
                    {
                        if (!Hull.Contains(s)) Hull.Add(s);
                        if (!Hull.Contains(s2)) Hull.Add(s2);
                    }

                    j++;
                }
                i++;
            }

            foreach(var s in Figures.ToList())
            {
                if (!Hull.Contains(s) && Figures.Count >= 4) Figures.Remove(s);
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
                            if (i == m || j == m) { m++; continue; }
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
                            if (i == m || j == m) { m++; continue; }                          
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

            if (Figures.Count >= 3) {
                DrawConvexHull(context);
            } 
        }
    }
}
    