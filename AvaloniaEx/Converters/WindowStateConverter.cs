namespace Macabresoft.AvaloniaEx;

using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

/// <summary>
/// A converter to determine if the maximize button should be shown based on the current window state.
/// </summary>
public class WindowStateConverter : IValueConverter {
    
    /// <summary>
    /// Instance of this converter with <see cref="InvertResult"/> set to false.
    /// </summary>
    public static WindowStateConverter ShowMaximize { get; } = new();
    
    /// <summary>
    /// Instance of this converter with <see cref="InvertResult"/> set to true.
    /// </summary>
    public static WindowStateConverter ShowRestore { get; } = new() { InvertResult = true };
    
    /// <summary>
    /// Gets or sets a value indicating whether or not the result should be inverted.
    /// </summary>
    public bool InvertResult { get; set; }

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is WindowState windowState) {
            var result = windowState is WindowState.Normal or WindowState.Minimized;
            return this.InvertResult ? !result : result;
        }

        return AvaloniaProperty.UnsetValue;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}