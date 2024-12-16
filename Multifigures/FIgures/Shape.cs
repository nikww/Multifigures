using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Platform;

namespace Multifigures
{   
    public abstract class Shape
    {
        public bool moving;
        protected double x, y; 
        protected static int r;
        protected Color c;  
        
        protected Shape(double xx, double yy, Color cc) {
            x = xx; y = yy; c = cc; moving = false;
        }
        static Shape()
        {
            r = 25;
        }

        public double X
        {
            get { return x;}
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public abstract void Draw(DrawingContext context);
        public abstract bool IsInside(double curs_x, double curs_y);
    }
}