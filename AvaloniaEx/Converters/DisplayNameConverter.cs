namespace Macabresoft.AvaloniaEx;

using System;
using System.Globalization;
using Avalonia.Data.Converters;

/// <summary>
/// Converter for display names.
/// </summary>
public class DisplayNameConverter : IValueConverter {
    public static readonly DisplayNameConverter Instance = new();

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return value != null ? DisplayNameHelper.Instance.GetDisplayName(value.GetType(), value) : string.Empty;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}