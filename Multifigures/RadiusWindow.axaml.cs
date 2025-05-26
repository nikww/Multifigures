using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Multifigures.Figures;
using Avalonia.Markup.Xaml;
using System.Security.Cryptography.X509Certificates;

namespace Multifigures;

public partial class RadiusWindow : Window
{
    public RadiusWindow()
    {
        InitializeComponent();
    }

    public void SetRadius(double r) => Slider.Value = r;

    public event RadiusDelegate RadiusChanged;

    void OnRadiusValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (RadiusChanged != null) RadiusChanged(this, new RadiusEventArgs(Slider.Value));
    }
}