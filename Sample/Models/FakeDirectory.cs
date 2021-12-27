namespace Macabresoft.AvaloniaEx.Sample.Models;

using System.Collections.ObjectModel;

public sealed class FakeDirectory : FileSystemObject {
    public ObservableCollection<FileSystemObject> Children { get; } = new();
}