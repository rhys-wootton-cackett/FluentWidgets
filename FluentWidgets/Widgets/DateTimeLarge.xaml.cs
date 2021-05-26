using FluentWidgets.WidgetModel;

namespace FluentWidgets.Widgets
{
    /// <summary>
    ///     Interaction logic for DateTimeLarge.xaml
    /// </summary>
    public partial class DateTimeLarge
    {
        private readonly DateTimeWidgetModel dtModel = new DateTimeWidgetModel();

        public DateTimeLarge()
        {
            InitializeComponent();
            DataContext = dtModel;
        }
    }
}