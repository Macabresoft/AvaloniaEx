namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;

/// <summary>
/// A class which helps with display names in the UI.
/// </summary>
public class DisplayNameHelper {
    private readonly Dictionary<Type, IDisplayNameHandler> _typeToDisplayNameHandler = new();

    private DisplayNameHelper() {
    }

    /// <summary>
    /// Gets the default handler for unregistered types.
    /// </summary>
    public IDisplayNameHandler DefaultHandler { get; set; } = new DefaultDisplayNameHandler();

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
        if (value != null && type != null && this._typeToDisplayNameHandler.TryGetValue(type, out var handler)) {
            return handler.GetDisplayName(value);
        }

        return this.GetFromDefaultHandler(value);
    }

    /// <summary>
    /// Registers a <see cref="IDisplayNameHandler" /> with this helper.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="handler">The handler.</param>
    public void RegisterHandler(Type type, IDisplayNameHandler handler) {
        this._typeToDisplayNameHandler[type] = handler;
    }

    private string GetFromDefaultHandler(object value) {
        var displayName = string.Empty;

        if (value != null) {
            displayName = this.DefaultHandler != null ? this.DefaultHandler.GetDisplayName(value) : value.ToString();
        }

        return displayName;
    }
}