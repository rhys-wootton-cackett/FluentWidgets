using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;
using FluentWidgets.Helpers.Objects;
using ScottPlot.Drawing.Colormaps;

namespace FluentWidgets.Helpers.Converters
{
    public class SpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || parameter == DependencyProperty.UnsetValue) return "";

            var speed = (double)value;
            var unit = (SpeedUnit)parameter;

            return unit switch
            {
                SpeedUnit.MetresPerSecond => Math.Round(speed, 1) + "m/s",
                SpeedUnit.MilesPerHour => Math.Round(speed * 2.237, 1) + "mph",
                SpeedUnit.KilometersPerHour => Math.Round(speed * 3.6, 1) + "kph",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
