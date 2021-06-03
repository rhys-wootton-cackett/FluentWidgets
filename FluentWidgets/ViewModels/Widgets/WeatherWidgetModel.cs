using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using FluentWidgets.Helpers;
using FluentWidgets.Helpers.MVVM;
using FluentWidgets.Helpers.Objects;
using FluentWidgets.Models;
using FluentWidgets.Models.Widgets;

namespace FluentWidgets.ViewModels.Widgets
{
    public class WeatherWidgetModel : INotifyPropertyChanged
    {
        private Timer _weatherTimer;
        private readonly WeatherModel _wModel = new WeatherModel();

        public WeatherWidgetModel()
        {
            _weatherTimer = new Timer(GetWeatherForecast, null, TimeSpan.FromSeconds(0), TimeSpan.FromMinutes(30));
        }

        public OpenWeather Weather { get; set; }
        public Visibility IsRefreshing { get; set; }
        public ICommand RefreshWeatherCommand => new DelegateCommand(GetWeatherForecast);

        public event PropertyChangedEventHandler PropertyChanged;

        private async void GetWeatherForecast(object state)
        {
            this.IsRefreshing = Visibility.Visible;
            Weather = await _wModel.GetWeatherForecast();
            this.IsRefreshing = Visibility.Collapsed;
        }
    }
}