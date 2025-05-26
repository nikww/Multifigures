using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Multifigures
{
    public partial class MainWindow : Window
    {
        private RadiusWindow RadWin;                                                                                                                              

        public MainWindow()
        {
            InitializeComponent();
            Shape.c = Colors.White;
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
            if (RadWin is { IsLoaded: true })
            {
                RadWin.Activate();
                RadWin.WindowState = WindowState.Normal;
            }
            else
            {
                RadWin = new RadiusWindow();
                RadWin.SetRadius(Shape.r);
                RadWin.RadiusChanged += CC.UpdateRadius;
                RadWin.Show();
            }
        }

        private void Win_OnClickOur(object sender, RoutedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            ComparasionWindow comparasionWindow = new ComparasionWindow(CC.GetCharsOur(), CC.GetCharsJarvis());
            comparasionWindow.Show();
        }
        private void Win_OnClickJarvis()
        {
           
        }
        private void Win_OnClickBoth()
        {

        }

        public async void Win_ColorChanged(object sender, RoutedEventArgs e)
        {
            ColorWindow ColorWin = new ColorWindow();
            ColorWin.SetColor(Shape.c);
            var color = await ColorWin.ShowDialog<Color>(this);

            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.UpdateColor(color);
        }

        public void Win_OnStart(object sender, RoutedEventArgs e)
        {

        }
        public void Win_OnStop(object sender, RoutedEventArgs e)
        {

        }
    }
}   