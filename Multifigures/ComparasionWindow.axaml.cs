using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using Tmds.DBus.Protocol;

namespace Multifigures;

public partial class ComparasionWindow : Window
{
    public ComparasionWindow(Dictionary<int, double> our, Dictionary<int, double> jarvis)
    {
        InitializeComponent();

        CompCntrl comp_cont = this.Find<CompCntrl>("myCompCC");
        comp_cont.Establish(our, jarvis);
    }
}