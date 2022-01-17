namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

/// <summary>
/// Takes an flags enum value and determines if the second value provided is marked as on in the flags enum.
/// </summary>
public class FlagsEnumToBoolConverter : IMultiValueConverter {
    /// <summary>
    /// Gets a singleton instance.
    /// </summary>
    public static FlagsEnumToBoolConverter Instance { get; } = new();

    /// <inheritdoc />
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture) {
        object result = 0;

        if (values.Count == 2) {
            var value0 = values[0];
            var value1 = values[1];

            if (value0 != null && value1 != null && value0 is not UnsetValueType && value1 is not UnsetValueType) {
                var enumValue = values[0] == null ? 0 : System.Convert.ToInt32(values[0]);
                var toggledValue = values[1] == null ? 0 : System.Convert.ToInt32(values[1]);
                result = (enumValue & toggledValue) == toggledValue;
            }
        }

        return result;
    }
}