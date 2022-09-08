using System.Globalization;

namespace AvaloniaUIDashboard.Converters
{
    public class MarginAnimationConverter : MultiConverterMarkupExtension<MarginAnimationConverter>
    {
        public override object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            // Margin : Thickness Type: http://reference.avaloniaui.net/api/Avalonia.Layout/Layoutable/13C9B4A5
            // Width : Double Type : http://reference.avaloniaui.net/api/Avalonia/Size/3052D92B

            if (values[0] is UnsetValueType ||
                values[1] is UnsetValueType) // 디버깅 시 unset 으로 된 타입은 걸러짐.
            {
                return new Thickness();
            }

            double value1 = (double)values[0]!;
            double value2 = (double)values[1]!;

            return new Thickness(value2 - value1, 0, 0, 0);
        }
    }
}
