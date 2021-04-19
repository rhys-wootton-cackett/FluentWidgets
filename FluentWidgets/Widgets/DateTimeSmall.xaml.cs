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
using FluentWidgets.WidgetModel;

namespace FluentWidgets.Widgets
{
    /// <summary>
    /// Interaction logic for TestWidget.xaml
    /// </summary>
    public partial class DateTimeSmall
    {
        private DateTimeWidgetModel dtModel = new DateTimeWidgetModel();

        public DateTimeSmall()
        {
            InitializeComponent();
            this.DataContext = dtModel;
        }

        private void DateTimeSmall_LocationChanged(object sender, EventArgs e)
        {
            LocationChanged(sender, e);
        }
    }
}
