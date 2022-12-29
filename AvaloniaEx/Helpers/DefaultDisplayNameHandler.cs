namespace Macabresoft.AvaloniaEx;

using System;
using Macabresoft.Core;

/// <summary>
/// A default display name handler.
/// </summary>
public class DefaultDisplayNameHandler : IDisplayNameHandler {
    private readonly EnumDisplayNameHandler _enumDisplayNameHandler = new();

    /// <inheritdoc />
    public string GetDisplayName(object value) {
        return value switch {
            Enum enumValue => this._enumDisplayNameHandler.GetDisplayName(enumValue),
            Type type => type.GetTypeDisplayName(),
            _ => value?.ToString() ?? string.Empty
        };
    }
}