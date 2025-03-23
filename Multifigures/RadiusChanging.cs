using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multifigures
{
    internal class RadiusEventArgs : EventArgs
    {
        public double r;
        public RadiusEventArgs(double radius) => r = radius;
    }
}
