using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;

namespace FluentWidgets.Helpers
{
    public class WidgetBase : Window
    {
        private readonly double SNAP_BOUNDARY = 30;
        private readonly double WIDGET_MARGIN = 15;

        protected WidgetBase()
        {
            this.ShowInTaskbar = false;
            this.Topmost = false;

            this.MouseDown += delegate (object sender, MouseButtonEventArgs e)
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            };
        }

        public new void LocationChanged(object sender, EventArgs e)
        {
            // Co-ordinates go from top-left, top-right, bottom-left, bottom-right

            // Check for snapping between the edges of windows
            var screen = WpfScreen.GetScreenFrom(new Point(this.Left, this.Top));
            CheckWindowCoordinates(screen.WorkingArea);

            // Check for snapping between other widgets on the screen
            foreach (var widget in Application.Current.Windows.Cast<Window>().Where(w => w is WidgetBase && w != sender))
            {
                CheckWidgetCoordinates((WidgetBase) widget);
            }
        }

        private void CheckWindowCoordinates(Rect screen)
        {
            if (this.Left - screen.Left <= SNAP_BOUNDARY) this.Left = screen.Left + WIDGET_MARGIN;
            if (this.Top - screen.Top <= SNAP_BOUNDARY) this.Top = screen.Top + WIDGET_MARGIN;
            if (screen.Right - (this.Left + this.ActualWidth) <= SNAP_BOUNDARY)
                this.Left = screen.Right - this.ActualWidth - WIDGET_MARGIN;
            if (screen.Bottom - (this.Top + this.ActualHeight) <= SNAP_BOUNDARY)
                this.Top = screen.Bottom - this.ActualHeight - WIDGET_MARGIN;
        }

        private void CheckWidgetCoordinates(WidgetBase widgetTwo)
        {
            //Check the right to see if snapping is possible to the left of another widget
            if (Math.Abs((this.Left + this.ActualWidth) - widgetTwo.Left) <= SNAP_BOUNDARY &&
                Math.Abs(this.Top - widgetTwo.Top) <= SNAP_BOUNDARY && 
                Math.Abs((this.Top - this.ActualHeight) - (widgetTwo.Top - widgetTwo.ActualHeight)) <= SNAP_BOUNDARY)
            {
                this.Left = widgetTwo.Left - this.ActualWidth - WIDGET_MARGIN;
                this.Top = widgetTwo.Top;
            }

            // Check the bottom to see if snapping is possible to the top of another widget
            if (Math.Abs((this.Top + this.ActualHeight) - widgetTwo.Top) <= SNAP_BOUNDARY &&
                Math.Abs(this.Left - widgetTwo.Left) <= SNAP_BOUNDARY &&
                Math.Abs((this.Left + this.ActualWidth) - (widgetTwo.Left + widgetTwo.ActualWidth)) <= SNAP_BOUNDARY)
            {
                this.Left = widgetTwo.Left;
                this.Top = widgetTwo.Top - this.ActualHeight - WIDGET_MARGIN;
            }

            // Check the left to see if snapping is possible to the right of another widget
            if (Math.Abs(this.Left - (widgetTwo.Left + widgetTwo.ActualWidth)) <= SNAP_BOUNDARY &&
                Math.Abs(this.Top - widgetTwo.Top) <= SNAP_BOUNDARY &&
                Math.Abs((this.Top - this.ActualHeight) - (widgetTwo.Top - widgetTwo.ActualHeight)) <= SNAP_BOUNDARY)
            {
                this.Left = widgetTwo.Left + widgetTwo.ActualWidth + WIDGET_MARGIN;
                this.Top = widgetTwo.Top;
            }

            // Check the top to see if snapping is possible to the bottom of another widget
            if (Math.Abs(this.Top - (widgetTwo.Top + widgetTwo.ActualHeight)) <= SNAP_BOUNDARY &&
                Math.Abs(this.Left - widgetTwo.Left) <= SNAP_BOUNDARY &&
                Math.Abs((this.Left + this.ActualWidth) - (widgetTwo.Left + widgetTwo.ActualWidth)) <= SNAP_BOUNDARY)
            {
                this.Left = widgetTwo.Left;
                this.Top = widgetTwo.Top + this.ActualHeight + WIDGET_MARGIN;
            }
        }
    }
}
