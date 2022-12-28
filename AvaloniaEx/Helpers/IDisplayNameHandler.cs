namespace Macabresoft.AvaloniaEx;

/// <summary>
/// Interface for classes which handle converting from a value to a display name.
/// </summary>
public interface IDisplayNameHandler {
    string GetDisplayName(object value);
}