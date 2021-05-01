using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Google.Apis.Calendar.v3.Data;

namespace FluentWidgets.Helpers
{
    [ValueConversion(typeof(Event), typeof(string))]
    public class CalendarTimeFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var calEvent = (Event) value ?? new Event();

            if (calEvent.Start.Date != null)
            {
                var date = DateTime.Parse(calEvent.Start.Date);
                return $"{date:ddd d MMM}";
            }
            
            if (!(calEvent.Start.DateTime ?? default).Date.Equals(calEvent.End.DateTime ?? default))
                return $"{calEvent.Start.DateTime:ddd d MMM hh:mm} - {calEvent.End.DateTime:ddd d MMM hh:mm}";
            
            return $"{calEvent.Start.DateTime:ddd d MMM hh:mm} - {calEvent.End.DateTime:hh:mm}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new CodePrimitiveExpression(value);
        }
    }
}
