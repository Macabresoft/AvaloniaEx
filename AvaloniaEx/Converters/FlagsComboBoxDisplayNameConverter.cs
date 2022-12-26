namespace Macabresoft.AvaloniaEx;

using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Macabresoft.Core;

public class FlagsComboBoxDisplayNameConverter : IValueConverter {
    public static readonly FlagsComboBoxDisplayNameConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var displayName = string.Empty;
        if (value is Enum enumValue) {
            displayName = GetEnumName(enumValue);
        }

        return displayName;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }

    private static string GetEnumName(Enum enumValue) {
        var enumType = enumValue.GetType();
        var displayName = string.Empty;
        var values = Enum.GetValues(enumType).OfType<Enum>().Where(enumValue.HasFlag).ToList();
        var noneValue = values.FirstOrDefault(x => System.Convert.ToInt32(x) == 0);
        values.Remove(noneValue);

        foreach (var value in values) {
            var enumName = value.GetEnumDisplayName();
            displayName = string.IsNullOrEmpty(displayName) ? enumName : $"{displayName}, {enumName}";
        }

        if (displayName == string.Empty && noneValue != null) {
            displayName = noneValue?.GetEnumDisplayName();
        }

        return displayName;
    }
}