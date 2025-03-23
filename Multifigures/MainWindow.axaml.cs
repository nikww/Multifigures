using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace Multifigures
{
    public partial class MainWindow : Window
    {
        public RadiusWindow RadWin = new RadiusWindow()                                                                                                                               ;

        public MainWindow()
        {
            InitializeComponent();
            Shapes.ItemsSource = new string[] { "Circle", "Square", "Triange" };
            Shapes.SelectedIndex = 0;
            Algo.ItemsSource = new string[] { "Our", "Jarvis" };
            Algo.SelectedIndex = 0;
        }

        private void Win_PointerMoved(object sender, Avalonia.Input.PointerEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Move(e.GetPosition(CC).X, e.GetPosition(CC).Y);
        }

        private void Win_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            var point = e.GetCurrentPoint(sender as CustomControl);
            CC.Click(e.GetPosition(CC).X, e.GetPosition(CC).Y, point);
        }

        private void Win_PointerReleased(object sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Release();
        }


        private void Win_ShapeChanged (object sender,  SelectionChangedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.ChangeShape(Shapes.SelectedIndex);
        }


        private void Win_AlgoChanged(object sedner,  SelectionChangedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.ChangeAlgo(Shapes.SelectedIndex);
        }
        private void Win_RadiusChanged(object sender, RoutedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            RadWin.Show();
        }
    }
}   