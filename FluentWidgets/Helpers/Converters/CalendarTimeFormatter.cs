using System;
using System.CodeDom;
using System.Globalization;
using System.Windows.Data;
using Google.Apis.Calendar.v3.Data;

namespace FluentWidgets.Helpers.Converters
{
    [ValueConversion(typeof(Event), typeof(string))]
    public class CalendarTimeFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var calEvent = (Event) value;

            if (calEvent.Start.Date == null)
                return !calEvent.Start.DateTime.Value.Date.Date.Equals(calEvent.End.DateTime.Value.Date)
                    ? $"{calEvent.Start.DateTime:ddd d MMM HH:mm} - {calEvent.End.DateTime:ddd d MMM HH:mm}"
                    : $"{calEvent.Start.DateTime:ddd d MMM HH:mm} - {calEvent.End.DateTime:HH:mm}";

            var date = DateTime.Parse(calEvent.Start.Date);
            return $"{date:ddd d MMM}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new CodePrimitiveExpression(value);
        }
    }
}