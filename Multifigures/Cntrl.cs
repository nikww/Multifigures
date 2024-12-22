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
        public List<Shape> Dragged_figures = []; 

        public void Click(double cx, double cy)
        {
            foreach (var f in Figures)
            {
                if (!f.IsInside(cx, cy)) continue;

                prevx = cx; prevy = cy;
                f.moving = true;
                Dragged_figures.Add(f);
            }
        }

        public void Move(double cx, double cy)
        {
            foreach (var f in Figures)
            {
                if (!Dragged_figures.Contains(f)) continue;
                f.X += cx - prevx; f.Y += cy - prevy;
            }
            prevx = cx; prevy = cy;
            InvalidateVisual();
        }
        
        public void Release(double cx, double cy)
        {
            foreach (var f in Figures)
            {
                if (!Dragged_figures.Contains(f)) continue;

                f.moving = false;
                Dragged_figures.Remove(f);
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
