using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multifigures;

    public delegate void RadiusDelegate(object sender, RadiusEventArgs e);
    public class RadiusEventArgs : EventArgs
    {
        public double r;
        public RadiusEventArgs(double radius) => r = radius;
    }

