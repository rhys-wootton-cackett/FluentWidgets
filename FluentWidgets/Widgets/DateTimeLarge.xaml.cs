using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentWidgets.WidgetModel;

namespace FluentWidgets.Widgets
{
    /// <summary>
    /// Interaction logic for DateTimeLarge.xaml
    /// </summary>
    public partial class DateTimeLarge
    {
        private DateTimeWidgetModel dtModel = new DateTimeWidgetModel();

        public DateTimeLarge()
        {
            InitializeComponent();
            this.DataContext = dtModel;
        }

        private void DateTimeLarge_LocationChanged(object sender, EventArgs e)
        {
            //LocationChanged(sender, e);
        }
    }
}
