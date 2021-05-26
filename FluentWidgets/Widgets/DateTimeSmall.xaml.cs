using FluentWidgets.WidgetModel;

namespace FluentWidgets.Widgets
{
    /// <summary>
    ///     Interaction logic for TestWidget.xaml
    /// </summary>
    public partial class DateTimeSmall
    {
        private readonly DateTimeWidgetModel dtModel = new DateTimeWidgetModel();

        public DateTimeSmall()
        {
            InitializeComponent();
            DataContext = dtModel;
        }
    }
}