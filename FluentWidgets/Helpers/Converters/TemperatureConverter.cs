﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FluentWidgets.Helpers.Objects;

namespace FluentWidgets.Helpers.Converters
{
    [ValueConversion(typeof(double), typeof(int))]
    public class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue || parameter == DependencyProperty.UnsetValue) return "";

            var temp = (double) value;
            var unit = (TemperatureUnit) parameter;

            return unit switch
            {
                TemperatureUnit.Celsius => System.Convert.ToInt32(temp - 273.15) + "°",
                TemperatureUnit.Fahrenheit => System.Convert.ToInt32((temp - 273.15) * 1.8 + 32) + "°",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}