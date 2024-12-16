using Avalonia.Media;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Multifigures
{   
    public abstract class Shape
    {
        protected int x, y; 
        protected static int r;
        protected Color c;  
        
        protected Shape(int xx, int yy, Color cc) {
            x = xx; y = yy; c = cc;
        }
        static Shape()
        {
            r = 25;
        }

        public int X
        {
            get { return x;}
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public abstract void Draw(DrawingContext context);
    }
}