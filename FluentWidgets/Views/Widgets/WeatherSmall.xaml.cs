using FluentWidgets.ViewModels.Widgets;

namespace FluentWidgets.Views.Widgets
{
    /// <summary>
    ///     Interaction logic for TestWidget.xaml
    /// </summary>
    public partial class WeatherSmall
    {
        private readonly WeatherWidgetModel _wModel = new WeatherWidgetModel();

        public WeatherSmall()
        {
            InitializeComponent();
            DataContext = _wModel;
        }
    }
}