using Avalonia.Controls;
using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace Multifigures
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Win_PointerMoved(object sender, Avalonia.Input.PointerEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Move(e.GetPosition(CC).X, e.GetPosition(CC).Y);
        }

        private void Win_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Click(e.GetPosition(CC).X, e.GetPosition(CC).Y);
        }

        private void Win_PointerReleased(object sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Release(e.GetPosition(CC).X, e.GetPosition(CC).Y);
        }
        
    }
}   