namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;

public class DisplayNameHelper {
    private readonly Dictionary<Type, IDisplayNameHandler> _typeToDisplayNameHandler = new();

    private DisplayNameHelper() {
    }

    /// <summary>
    /// Gets a static instance of <see cref="DisplayNameHelper" />.
    /// </summary>
    public static DisplayNameHelper Instance { get; } = new();

    /// <summary>
    /// Gets the display name.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The display name.</returns>
    public string GetDisplayName<T>(T value) {
        return this.GetDisplayName(typeof(T), value);
    }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <returns>The display name.</returns>
    public string GetDisplayName(Type type, object value) {
        var displayName = string.Empty;
        if (type != null) {
            if (this._typeToDisplayNameHandler.TryGetValue(type, out var handler)) {
                displayName = handler.GetDisplayName(value);
            }
            else if (value != null) {
                displayName = value.ToString();
            }
        }
        else if (value != null) {
            displayName = value.ToString();
        }

        return displayName;
    }

    /// <summary>
    /// Registers a <see cref="IDisplayNameHandler" /> with this helper.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="handler">The handler.</param>
    public void RegisterHandler(Type type, IDisplayNameHandler handler) {
        this._typeToDisplayNameHandler[type] = handler;
    }
}