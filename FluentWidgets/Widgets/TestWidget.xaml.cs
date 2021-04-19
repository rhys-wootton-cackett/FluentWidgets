using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using FluentWidgets.Helpers;

namespace FluentWidgets.Widgets
{
    /// <summary>
    /// Interaction logic for TestWidget.xaml
    /// </summary>
    public partial class TestWidget : WidgetBase
    {
        public DateTime CurrentDateAndTime { get; set; }

        public TestWidget()
        {
            InitializeComponent();

            this.DataContext = this;

            DispatcherTimer dayTimer = new DispatcherTimer();
            dayTimer.Interval = TimeSpan.FromMilliseconds(500);
            dayTimer.Tick += (s, e) =>
            {
                CurrentDateAndTime = DateTime.Now;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentDateAndTime"));
            };
            dayTimer.Start();
        }

        private void WidgetBase_LocationChanged(object sender, EventArgs e)
        {
            LocationChanged(sender, e);
        }
    }
}
