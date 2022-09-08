using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaCommunityToolkitTest
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

            byte hour = (byte)values[0]!;
            byte minute = (byte)values[1]!;
            byte second = (byte)values[2]!;

            return $"{hour:D2}:{minute:D2}:{second:D2}";
        }
    }
}
