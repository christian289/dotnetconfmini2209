using System.Globalization;

namespace AvaloniaUIDashboard.Converters
{
    public class ClockConverter : MultiConverterMarkupExtension<ClockConverter>
    {
        public override object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values[0] is UnsetValueType ||
                values[1] is UnsetValueType ||
                values[2] is UnsetValueType)
            {
                return "00:00:00";
            }

            var hour = (short)values[0]!;
            var minute = (short)values[1]!;
            var second = (short)values[2]!;

            return $"{hour:D2}:{minute:D2}:{second:D2}";
        }
    }
}
