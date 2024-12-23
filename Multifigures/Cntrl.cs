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
            new Circle(100, 100, Colors.Aqua),
            new Square(200, 200, Colors.Azure),
            new Triangle(300, 300, Colors.Green)
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
                    Figures.Add(new Circle(cx, cy, Colors.AliceBlue));
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

        public override void Render(DrawingContext context)
        {
            foreach (Shape s in Figures)
            {
                s.Draw(context);
            }
        }
    }
}
