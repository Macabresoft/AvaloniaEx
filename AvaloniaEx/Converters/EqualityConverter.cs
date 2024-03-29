﻿namespace Macabresoft.AvaloniaEx;

using System;
using System.Globalization;
using Avalonia.Data.Converters;

/// <summary>
/// Takes a value and a parameter. If these two things are equal, returns true.
/// </summary>
public class EqualityConverter : IValueConverter {
    
    /// <summary>
    /// Instance of this converter with <see cref="InvertResult"/> set to false.
    /// </summary>
    public static EqualityConverter IsEqual { get; } = new();
    
    /// <summary>
    /// Instance of this converter with <see cref="InvertResult"/> set to true.
    /// </summary>
    public static EqualityConverter IsNotEqual { get; } = new() { InvertResult = true };
    
    /// <summary>
    /// Gets or sets a value indicating whether or not the result should be inverted.
    /// </summary>
    public bool InvertResult { get; set; }

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var result = !(value == null && parameter != null || value != null && !value.Equals(parameter));
        return this.InvertResult ? !result : result;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return value is true ? parameter : null;
    }
}