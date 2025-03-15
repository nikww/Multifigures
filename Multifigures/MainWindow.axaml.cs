using Avalonia.Controls;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace Multifigures
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Shapes.SelectedIndex = 0;
            Shapes.ItemsSource = new string[] { "Circle", "Square", "Triange" };
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

            int index = Shapes.SelectedIndex;
            CC.ChangeShape(index);
        }
    }
}   