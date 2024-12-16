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
        public List<Shape> Figures = [
            new Circle(100, 100, Colors.Aqua)
        ];
        public void Click(int x, int y)
        {
            Circle circle = (Circle)Figures[0];
            circle.X = x - (circle.X - x); circle.Y = y - (circle.Y - y); 
            InvalidateVisual();

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
