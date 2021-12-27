namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;
using JetBrains.Annotations;

public class StringConcatenationConverter : IMultiValueConverter {
    [CanBeNull]
    public string ConcatenationCharacter { get; set; }

    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture) {
        var stringValues = values.OfType<string>().ToList();
        if (stringValues.Any()) {
            return this.ConcatenationCharacter != null ? string.Join(this.ConcatenationCharacter, stringValues) : string.Concat(stringValues);
        }

        return AvaloniaProperty.UnsetValue;
    }
}