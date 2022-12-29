namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;

/// <summary>
/// Converts from a <see cref="Type" /> of an enum to a collection of all the distinct enum values. If a <see cref="IEnumerable" /> is provided, that overrides the enum.
/// </summary>
public class EnumTypeToValuesWithOverrideConverter : IMultiValueConverter {
    /// <summary>
    /// Gets a singleton instance.
    /// </summary>
    public static EnumTypeToValuesWithOverrideConverter Instance { get; } = new(); 
    
    /// <inheritdoc />
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture) {
        var items = values.FirstOrDefault(x => x is IEnumerable);
        if (items is IEnumerable enumerable && enumerable.Cast<object>().Any()) {
            return items;
        }

        var enumType = values.FirstOrDefault(x => x is Type);
        return enumType != null ? EnumTypeToValuesConverter.Instance.Convert(enumType, targetType, parameter, culture) : AvaloniaProperty.UnsetValue;
    }
}