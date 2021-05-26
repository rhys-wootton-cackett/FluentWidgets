using FluentWidgets.WidgetModel;

namespace FluentWidgets.Widgets
{
    public partial class WeatherLarge
    {
        private readonly WeatherWidgetModel wModel = new WeatherWidgetModel();

        public WeatherLarge()
        {
            InitializeComponent();
            DataContext = wModel;
        }
    }
}