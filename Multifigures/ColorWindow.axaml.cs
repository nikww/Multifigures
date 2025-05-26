using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Markup.Xaml;

namespace Multifigures;

public partial class ColorWindow : Window
{
    public ColorWindow()
    {
        InitializeComponent();
    }

    public void SetColor(Color c) => Choose.Color = c;

    void OnClick(object sender, RoutedEventArgs e)
    {
        Color c = Choose.Color;
        Close(c);
    }
}