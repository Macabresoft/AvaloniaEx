namespace Macabresoft.AvaloniaEx.Sample.Models;

public class FileSystemObjectDisplayNameHandler : IDisplayNameHandler {
    public string GetDisplayName(object value) {
        var displayName = string.Empty;
        if (value is FileSystemObject fileSystemObject) {
            displayName = fileSystemObject.Depth == 0 ? fileSystemObject.Name : $"{fileSystemObject.Name} (Depth {fileSystemObject.Depth})";
        }

        return displayName;
    }
}