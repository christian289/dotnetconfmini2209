using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaUIDashboard.Converters
{
    public abstract class ConverterMarkupExtension<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        private static Lazy<T> _converter = new(() => new T());

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter.Value;
        }

        public abstract object Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

        public abstract object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture);
    }

    public abstract class MultiConverterMarkupExtension<T> : MarkupExtension, IMultiValueConverter
        where T : class, new()
    {
        private static Lazy<T> _converter = new(() => new T());

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter.Value;
        }

        public abstract object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture);
    }
}
