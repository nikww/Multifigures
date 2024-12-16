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
        public override void Render(DrawingContext context)
        {
            List<Shape> Figures = [
                new Circle(100, 100, Colors.Aqua),
                new Square(200, 200, Colors.Pink),
                new Triangle(300, 300, Colors.DarkCyan)
            ];
            
            foreach (Shape s in Figures)
            {
                s.Draw(context);
            }
        }
    }
}
