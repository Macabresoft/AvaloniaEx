namespace Macabresoft.AvaloniaEx;

using System;
using System.Linq;
using System.Reflection;
using Macabresoft.Core;

/// <summary>
/// A display name handler for <see cref="Enum"/>.
/// </summary>
public class EnumDisplayNameHandler : IDisplayNameHandler {
    /// <inheritdoc />
    public string GetDisplayName(object value) {
        var displayName = string.Empty;

        if (value is Enum enumValue) {
            var enumType = enumValue.GetType();
            if (enumType.GetCustomAttribute<FlagsAttribute>() != null) {
                var values = Enum.GetValues(enumType).Cast<Enum>().Where(enumValue.HasFlag).ToList();
                var noneValue = values.FirstOrDefault(x => System.Convert.ToInt32(x) == 0);
                values.Remove(noneValue);

                foreach (var singleValue in values) {
                    var enumName = singleValue.GetEnumDisplayName();
                    displayName = string.IsNullOrEmpty(displayName) ? enumName : $"{displayName}, {enumName}";
                }

                if (displayName == string.Empty && noneValue != null) {
                    displayName = noneValue?.GetEnumDisplayName();
                }
            }
            else {
                displayName = enumValue.GetEnumDisplayName();
            }
        }
        else if (value != null) {
            displayName = value.ToString();
        }
        
        return displayName;
    }
}