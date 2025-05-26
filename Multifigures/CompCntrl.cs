using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Multifigures;

public class CompCntrl : UserControl
{
    Dictionary<int, double> map_our, map_jarvis;
    double max_our, max_jarvis, scale;
    public void Establish(Dictionary<int, double> our, Dictionary<int, double> jarvis)
    {
        map_our = our;
        foreach (var (key, value) in map_our) {
            max_our = Math.Max(max_our, value);
        }
        map_jarvis = jarvis;
        foreach (var (key, value) in map_jarvis)
        {
            max_jarvis = Math.Max(value, max_jarvis);
        }
        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        Brush lineBrush = new SolidColorBrush(Colors.White);
        Pen pen = new(lineBrush, lineCap: PenLineCap.Square);


        int xx = 20, yy = 700;
        context.DrawLine(pen, new Point(xx, yy), new Point(1000, yy));
        context.DrawLine(pen, new Point(xx, yy), new Point(xx, 50));
        for (int y = 700; y > 50; y -= 50)
        {
            context.DrawLine(pen, new Point(18, y), new Point(22, y));
        }

        for (int x = 20; x < 1000; x += 50)
        {
            context.DrawLine(pen, new Point(x, 703), new Point(x, 697));
        }
        //DrawGraph(context, map_our);
        DrawGraph(context, map_jarvis);
    }
    //scale_exp our = 350

    private void DrawGraph(DrawingContext context, Dictionary<int, double> map)
    {
        Brush lineBr = new SolidColorBrush(Colors.Red);
        Pen pen = new Pen(lineBr, lineCap: PenLineCap.Square);

        int i = 0;
        int prevkey = 0;
        double prevvalue = 0;
        foreach (var (key, value) in map)
        {
            if (i == 0)
            {
                i++; prevkey = key; prevvalue = value; continue;
            }

            Point p1 = new Point(20 + prevkey, 700 -  350 * prevvalue / max_jarvis), p2 = new Point(20 + key, 700 - 350 * value / max_jarvis);
            context.DrawLine(pen, p1, p2);
            prevkey = key; prevvalue = value;    
        }
    }
}
