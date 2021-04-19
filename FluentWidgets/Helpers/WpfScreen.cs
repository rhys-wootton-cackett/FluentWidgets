using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Point = System.Windows.Point;

namespace FluentWidgets.Helpers
{
    public class WpfScreen
    {
        public static IEnumerable<WpfScreen> AllScreens()
        {
            return System.Windows.Forms.Screen.AllScreens.Select(screen => new WpfScreen(screen));
        }

        public static WpfScreen GetScreenFrom(Window window)
        {
            var windowInteropHelper = new WindowInteropHelper(window);
            var screen = System.Windows.Forms.Screen.FromHandle(windowInteropHelper.Handle);
            var wpfScreen = new WpfScreen(screen);
            return wpfScreen;
        }

        public static WpfScreen GetScreenFrom(Point point)
        {
            var x = (int)Math.Round(point.X);
            var y = (int)Math.Round(point.Y);

            // are x,y device-independent-pixels ??
            var drawingPoint = new System.Drawing.Point(x, y);
            var screen = System.Windows.Forms.Screen.FromPoint(drawingPoint);
            var wpfScreen = new WpfScreen(screen);

            return wpfScreen;
        }

        public static WpfScreen Primary => new WpfScreen(System.Windows.Forms.Screen.PrimaryScreen);

        private readonly Screen _screen;

        internal WpfScreen(System.Windows.Forms.Screen screen)
        {
            this._screen = screen;
        }

        public Rect DeviceBounds => GetRect(this._screen.Bounds);

        public Rect WorkingArea => GetRect(this._screen.WorkingArea);

        private static Rect GetRect(Rectangle value)
        {
            // should x, y, width, height be device-independent-pixels ??
            return new Rect
            {
                X = value.X,
                Y = value.Y,
                Width = value.Width,
                Height = value.Height
            };
        }

        public bool IsPrimary => this._screen.Primary;

        public string DeviceName => this._screen.DeviceName;
    }
}
