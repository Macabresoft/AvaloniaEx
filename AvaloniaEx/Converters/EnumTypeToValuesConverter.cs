namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia.Data.Converters;

/// <summary>
/// Converts from a <see cref="Type" /> of an enum to a collection of all the distinct enum values.
/// </summary>
public sealed class EnumTypeToValuesConverter : IValueConverter {
    /// <summary>
    /// Gets a singleton instance.
    /// </summary>
    public static EnumTypeToValuesConverter Instance { get; } = new(); 
    
    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var result = new List<object>();

        if (value is Type { IsEnum: true } enumType) {
            var values = Enum.GetValues(enumType);
            var intValues = values.Cast<Enum>().Select(System.Convert.ToInt32).ToList();
            var showZero = !enumType.GetCustomAttributes<FlagsAttribute>().Any();

            for (var i = 0; i < values.Length; i++) {
                if (showZero || intValues[i] != 0) {
                    result.Add(values.GetValue(i));
                }
            }
        }

        return result;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}