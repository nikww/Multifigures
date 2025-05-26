using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Multifigures.Figures;

using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Multifigures
{
    public class CustomControl : UserControl
    {
        private bool click_hull = false;
        private int shape_index, algo_index;
        public List<Shape> Figures = [
        ];
        public List<Shape> Hull = [];
        private int[] counter = new int[12];
        
        private void fill()
        {
            counter[0] = 5; counter[1] = 25;
            for (int i = 50, j = 2; i <= 500; i += 50, j++)
            {
                counter[j] = i;
            }
        }
        


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
                    AddFigure(cx, cy);
                }
                InvalidateVisual();
                
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

        private void AddFigure(double cx, double cy) {
            Shape t;
            if (shape_index == 0)
            {
                t = new Circle(cx, cy);
            }
            else if (shape_index == 1) {
                t = new Square(cx, cy);
            }
            else
            {
                t = new Triangle(cx, cy);
            }
            
            Figures.Add(t); CheckHull();
            if (!Hull.Contains(t) && Figures.Count >= 3) click_hull = true;
            else click_hull = false;
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
            CheckHull();
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
        private void JarvisHull()
        {

            Hull.Clear();
            Pen pen = new Pen(new SolidColorBrush(Colors.Beige), 1, lineCap: PenLineCap.Square);

            if (Figures.Count <= 2) return;
            Shape cur = Figures.Where(p => p.X == Figures.Min(min => min.X)).First(), next;
          
            do
            {
                Hull.Add(cur);
                next = Figures.First();

                foreach (var p in Figures)
                {
                    if (p == Figures.First()) continue;
                    if (cur == next || rotate(cur, next, p) < 0)
                    {
                        next = p;
                    }
                }
                cur = next;
            }
            while (next != Hull.First());

            
            foreach (var s in Figures.ToList())
            {
                if (!Hull.Contains(s) && Figures.Count >= 4) Figures.Remove(s);
            }
        }
        private void DrawJarvisHull(DrawingContext context)
        {
            Hull.Clear(); 
            Pen pen = new Pen(new SolidColorBrush(Colors.Beige), 1, lineCap: PenLineCap.Square);

            Shape cur = Figures.Where(p => p.X == Figures.Min(min => min.X)).First(), next;

            do
            {
                Hull.Add(cur);
                next = Figures.First();

                foreach (var p in Figures)
                {
                    if (p == Figures.First()) continue;
                    if (cur == next || rotate(cur, next, p) < 0)
                    {
                        next = p;
                    } 
                }
                cur = next;
            }
            while (next != Hull.First());

            Shape prev = Hull.First();
            foreach (var p in Hull) {
                if (p == Hull.First()) continue;
                context.DrawLine(pen, new Point(prev.X, prev.Y), new Point(p.X, p.Y));
                prev = p;
                //context.DrawLine(pen, new Point(100, 100), new Point(p.X, p.Y));
            }
            context.DrawLine(pen, new Point(Hull.Last().X, Hull.Last().Y), new Point(Hull.First().X, Hull.First().Y));  
        }

        double rotate (Shape p1, Shape p2, Shape p)
        {
            return (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);
        }

        private void DrawHull(DrawingContext context)
        {
            if (algo_index == 0) DrawConvexHull(context);
            else if (algo_index == 1) DrawJarvisHull(context);
        }
        private void CheckHull()
        {
            if (algo_index == 0) ConvexHull();
            else if (algo_index == 1) JarvisHull();
        }



        public void ChangeShape(int index) => shape_index = index;
        public void ChangeAlgo(int index) => algo_index = index;

        public override void Render(DrawingContext context)
        {
            foreach (Shape s in Figures)
            {
                s.Draw(context);
            }

            if (Figures.Count >= 3) {
                DrawHull(context);
            } 
        }
    


        public Dictionary<int, double> GetCharsOur()
        {
            fill();
            Dictionary<int, double> map = new Dictionary<int, double>();
            var rndm = new Random();
            var timer = new Stopwatch();
            List<Shape> prev_figures = Figures;
            Figures.Clear();

            for (int i = 0; i < counter.Length; i++)
            {
                timer.Reset(); Figures.Clear();
                for (int j = 0; j < counter[i]; j++)
                {
                    Figures.Add(new Circle(rndm.Next(1, 1000), rndm.Next(1, 1000)));
                }
                timer.Start();
                ConvexHull();
                timer.Stop();
                map.Add(counter[i], timer.Elapsed.TotalMilliseconds);
            }
 
            Figures.Clear();
            Figures = prev_figures;
            InvalidateVisual();
            return map;     
        }
        public Dictionary<int, double> GetCharsJarvis()
        {
            fill();
            Dictionary<int, double> map = new Dictionary<int, double>();
            var rndm = new Random();
            var timer = new Stopwatch();
            List<Shape> prev_figures = Figures;
            Figures.Clear();

            for (int i = 0; i < counter.Length; i++)
            {
                timer.Reset(); Figures.Clear();
                for (int j = 0; j < counter[i]; j++)
                {
                    Figures.Add(new Circle(rndm.Next(1, 1000), rndm.Next(1, 1000)));
                }
                timer.Start();
                JarvisHull();
                timer.Stop();
                map.Add(counter[i], timer.Elapsed.TotalMilliseconds);
            }

            Figures.Clear();
            Figures = prev_figures;
            InvalidateVisual();
            return map;
        }


        public void UpdateRadius(object sender, RadiusEventArgs e)
        {
            Shape.r = e.r;
            InvalidateVisual();
        }

        public void UpdateColor(Color c)
        {
            Shape.c = c;
        }
    }
}
    