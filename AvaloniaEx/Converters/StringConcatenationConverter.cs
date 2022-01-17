namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;

/// <summary>
/// Concatenates strings using the specified character.
/// </summary>
public class StringConcatenationConverter : IMultiValueConverter {
    /// <summary>
    /// Gets or sets the character by which to concatenate strings.
    /// </summary>
    public string ConcatenationCharacter { get; set; }

    /// <inheritdoc />
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture) {
        var stringValues = values.OfType<string>().ToList();
        if (stringValues.Any()) {
            return this.ConcatenationCharacter != null ? string.Join(this.ConcatenationCharacter, stringValues) : string.Concat(stringValues);
        }

        return AvaloniaProperty.UnsetValue;
    }
}