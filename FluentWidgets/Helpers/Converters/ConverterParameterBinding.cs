using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace FluentWidgets.Helpers.Converters
{
    /// <summary>
    /// ConverterParameter, B., 2021. Binding ConverterParameter. [online] Stack Overflow.
    /// Available at: <https://stackoverflow.com/a/54222061> [Accessed 28 May 2021].
    /// </summary>
    [ContentProperty(nameof(Binding))]
    public class ConverterParameterBinding : MarkupExtension
    {
        #region Public Properties

        public Binding Binding { get; set; }
        public BindingMode Mode { get; set; }
        public IValueConverter Converter { get; set; }
        public Binding ConverterParameter { get; set; }
        #endregion

        public ConverterParameterBinding(string path)
        {
            Binding = new Binding(path);
        }

        public ConverterParameterBinding()
        {
        }

        #region Overridden Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var multiBinding = new MultiBinding();
            Binding.Mode = Mode;
            multiBinding.Bindings.Add(Binding);
            ConverterParameter.Mode = BindingMode.OneWay;
            multiBinding.Bindings.Add(ConverterParameter);
            var adapter = new MultiValueConverterAdapter
            {
                Converter = Converter
            };
            multiBinding.Converter = adapter;
            return multiBinding.ProvideValue(serviceProvider);
        }

        #endregion

        [ContentProperty(nameof(Converter))]
        private class MultiValueConverterAdapter : IMultiValueConverter
        {
            public IValueConverter Converter { get; set; }

            private object lastParameter;

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (Converter == null) return values[0]; // Required for VS design-time
                if (values.Length > 1) lastParameter = values[1];
                return Converter.Convert(values[0], targetType, lastParameter, culture);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                return new object[] { Converter.ConvertBack(value, targetTypes[0], lastParameter, culture) };
            }
        }
    }
}
