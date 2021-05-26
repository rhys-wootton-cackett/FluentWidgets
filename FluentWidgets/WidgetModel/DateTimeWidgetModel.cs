using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using FluentWidgets.Helpers;
using FluentWidgets.Model;
using Google.Apis.Calendar.v3.Data;

namespace FluentWidgets.WidgetModel
{
    public class DateTimeWidgetModel : INotifyPropertyChanged
    {
        private Timer _dateTimer, _calTimer;
        private readonly DateTimeModel _dtModel = new DateTimeModel();

        public DateTimeWidgetModel()
        {
            _dateTimer = new Timer(DateTimerTick, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0.1));
            _calTimer = new Timer(CalenderTimerTick, null, TimeSpan.FromSeconds(0),
                TimeSpan.FromMinutes(5));
        }

        public DateTime CurrentTime { get; set; }
        public Visibility IsRefreshing { get; set; }
        public ObservableCollection<Event> CalcEventList { get; set; }
        public ICommand RefreshCalendarNowCommand => new DelegateCommand(CalenderTimerTick);

        public event PropertyChangedEventHandler PropertyChanged;

        private void DateTimerTick(object sender)
        {
            CurrentTime = DateTime.Now;
        }

        private async void CalenderTimerTick(object sender)
        {
            IsRefreshing = Visibility.Visible;
            CalcEventList = await _dtModel.UpdateUpcomingEvents();
            IsRefreshing = Visibility.Hidden;
        }
    }
}