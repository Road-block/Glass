using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Glass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            const float refresh = (float)1000 / 60;
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(refresh)
            };
            timer.Tick += TimerTick;
            timer.Start();

            void TimerTick(object sender, EventArgs e)
            {
                GlassImage.InvalidateVisual();
                GlassImage.UpdateLayout();
            }
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            GlassImage.Width = SystemParameters.PrimaryScreenWidth / 5;
            GlassImage.Opacity = 0.4;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}
