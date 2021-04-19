using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using FluentWidgets.Annotations;
using FluentWidgets.Helpers;
using FluentWidgets.Model;
using Google.Apis.Calendar.v3.Data;

namespace FluentWidgets.WidgetModel
{
    public class DateTimeWidgetModel : INotifyPropertyChanged
    {
        private DateTimeModel _dtModel = new DateTimeModel();
        
        public event PropertyChangedEventHandler PropertyChanged;
        public DispatcherTimer _dateTimer, _calcTimer;
        public DateTime CurrentTime { get; set; } = DateTime.Now;
        public ObservableCollection<Event> CalcEventList { get; set; }

        public DateTimeWidgetModel()
        {
            _dateTimer = new DispatcherTimer(DispatcherPriority.Render) { Interval = TimeSpan.FromSeconds(0.1) };
            _dateTimer.Tick += (sender, args) =>
            {
                CurrentTime = DateTime.Now;
            };
            _dateTimer.Start();

            _calcTimer = new DispatcherTimer(DispatcherPriority.Render) { Interval = TimeSpan.FromMinutes(5) };
            _calcTimer.Tick += async (sender, args) =>
            {
                this.CalcEventList = await _dtModel.UpdateUpcomingEvents();
            };
            _calcTimer.Start();
        }

    }
}
