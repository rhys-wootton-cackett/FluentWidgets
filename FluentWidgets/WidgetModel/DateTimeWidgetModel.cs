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
        public DateTime CurrentTime { get; set; } = DateTime.Now;
        public ObservableCollection<Event> CalcEventList { get; set; }

        public DateTimeWidgetModel()
        {
            var dateTimer = new System.Threading.Timer(DateTimerTick, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0.1));
            var calTimer = new System.Threading.Timer(CalenderTimerTick, null, TimeSpan.FromSeconds(0),
                TimeSpan.FromMinutes(5));
        }

        private void DateTimerTick(object sender)
        {
            this.CurrentTime = DateTime.Now;
        }

        private async void CalenderTimerTick(object sender)
        {
            this.CalcEventList = await _dtModel.UpdateUpcomingEvents();
        }
    }
}
