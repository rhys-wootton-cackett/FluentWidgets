using System;
using System.Globalization;
using System.Windows.Data;

namespace FluentWidgets.Helpers.Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    public class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (double) value;

            if (bool.Parse(parameter.ToString()))
            {
                temp -= 273.15;
                return System.Convert.ToInt32(temp) + "°C";
            }
            
            temp = (temp - 273.15) * 1.8 + 32;
            return System.Convert.ToInt32(temp) + "°F";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}