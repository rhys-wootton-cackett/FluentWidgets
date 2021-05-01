using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FluentWidgets.Helpers
{
    [ValueConversion(typeof(string), typeof(string))]
    public class StringShortener : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (((string) value).Length > 35) return ((string) value).Substring(0, 32) + "...";
            return ((string) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new CodePrimitiveExpression(value);
        }
    }
}
