using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FluentWidgets.Helpers
{
    public class WidgetBase : Window
    {
        private readonly double SNAP_BOUNDARY = 30;
        private readonly double WIDGET_MARGIN = 15;

        protected WidgetBase()
        {
            ShowInTaskbar = false;
            Topmost = false;

            MouseDown += delegate(object sender, MouseButtonEventArgs e)
            {
                if (e.ChangedButton == MouseButton.Left)
                    DragMove();
            };
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            // Co-ordinates go from top-left, top-right, bottom-left, bottom-right
            // Check for snapping between the edges of windows
            var screen = WpfScreen.GetScreenFrom(new Point(Left, Top));
            CheckWindowCoordinates(screen.WorkingArea);

            // Check for snapping between other widgets on the screen
            foreach (var widget in Application.Current.Windows.Cast<Window>().Where(w => w is WidgetBase))
                CheckWidgetCoordinates((WidgetBase) widget);
        }

        private void CheckWindowCoordinates(Rect screen)
        {
            if (Left - screen.Left <= SNAP_BOUNDARY) Left = screen.Left + WIDGET_MARGIN;
            if (Top - screen.Top <= SNAP_BOUNDARY) Top = screen.Top + WIDGET_MARGIN;
            if (screen.Right - (Left + ActualWidth) <= SNAP_BOUNDARY)
                Left = screen.Right - ActualWidth - WIDGET_MARGIN;
            if (screen.Bottom - (Top + ActualHeight) <= SNAP_BOUNDARY)
                Top = screen.Bottom - ActualHeight - WIDGET_MARGIN;
        }

        private void CheckWidgetCoordinates(WidgetBase widgetTwo)
        {
            //Check the right to see if snapping is possible to the left of another widget
            if (Math.Abs(Left + ActualWidth - widgetTwo.Left) <= SNAP_BOUNDARY &&
                Math.Abs(Top - widgetTwo.Top) <= SNAP_BOUNDARY &&
                Math.Abs(Top - ActualHeight - (widgetTwo.Top - widgetTwo.ActualHeight)) <= SNAP_BOUNDARY)
            {
                Left = widgetTwo.Left - ActualWidth - WIDGET_MARGIN;
                Top = widgetTwo.Top;
            }

            // Check the bottom to see if snapping is possible to the top of another widget
            if (Math.Abs(Top + ActualHeight - widgetTwo.Top) <= SNAP_BOUNDARY &&
                Math.Abs(Left - widgetTwo.Left) <= SNAP_BOUNDARY &&
                Math.Abs(Left + ActualWidth - (widgetTwo.Left + widgetTwo.ActualWidth)) <= SNAP_BOUNDARY)
            {
                Left = widgetTwo.Left;
                Top = widgetTwo.Top - ActualHeight - WIDGET_MARGIN;
            }

            // Check the left to see if snapping is possible to the right of another widget
            if (Math.Abs(Left - (widgetTwo.Left + widgetTwo.ActualWidth)) <= SNAP_BOUNDARY &&
                Math.Abs(Top - widgetTwo.Top) <= SNAP_BOUNDARY &&
                Math.Abs(Top - ActualHeight - (widgetTwo.Top - widgetTwo.ActualHeight)) <= SNAP_BOUNDARY)
            {
                Left = widgetTwo.Left + widgetTwo.ActualWidth + WIDGET_MARGIN;
                Top = widgetTwo.Top;
            }

            // Check the top to see if snapping is possible to the bottom of another widget
            if (Math.Abs(Top - (widgetTwo.Top + widgetTwo.ActualHeight)) <= SNAP_BOUNDARY &&
                Math.Abs(Left - widgetTwo.Left) <= SNAP_BOUNDARY &&
                Math.Abs(Left + ActualWidth - (widgetTwo.Left + widgetTwo.ActualWidth)) <= SNAP_BOUNDARY)
            {
                Left = widgetTwo.Left;
                Top = widgetTwo.Top + ActualHeight + WIDGET_MARGIN;
            }
        }
    }
}