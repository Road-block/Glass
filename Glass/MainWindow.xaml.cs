using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Glass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int WsExTransparent = 0x00000020;
        public const int GwlExstyle = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        public MainWindow()
        {
            InitializeComponent();
            const float refresh = (float)1000 / 60; // 60 FPS
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
            GlassImage.Width = Math.Max(330, SystemParameters.PrimaryScreenWidth / 8);
            GlassImage.Height = SystemParameters.PrimaryScreenHeight * ((float)5 / 6);
            GlassImage.Opacity = 0.8;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void OnWindowSourceInitialized(object sender, EventArgs e)
        {
            // Get this window's handle         
            var hwnd = new WindowInteropHelper(this).Handle;
            // Change the extended window style to include WS_EX_TRANSPARENT         
            var extendedStyle = GetWindowLong(hwnd, GwlExstyle);
            SetWindowLong(hwnd, GwlExstyle, extendedStyle | WsExTransparent);
        }
    }
}
