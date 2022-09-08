using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaCommunityToolkitTest
{
    public class MarginAnimationConverter : MultiConverterMarkupExtension<MarginAnimationConverter>
    {
        public override object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            // 필요한거
            // X. Margin (Thickness Type: http://reference.avaloniaui.net/api/Avalonia.Layout/Layoutable/13C9B4A5) 필요없나..?
            // 1. 이동되는 컨트롤의 Width (View의 전체 사이즈와 비교해야함) http://reference.avaloniaui.net/api/Avalonia/Size/3052D92B
            // 2. 이동되는 바탕 View 크기

            //Thickness value1 = (Thickness)values[0]!;
            //double value2 = (double)values[1]!;
            //double value3 = (double)values[2]!;

            if (values[1] is UnsetValueType) // 이렇게하면 디버깅 시 unset 으로 된 타입은 걸러짐.
            {
                return 0;
            }

            double value1 = (double)values[0]!;
            double value2 = (double)values[1]!;

            return new Thickness(value2 - value1, 0, 0, 0);
        }
    }
}
