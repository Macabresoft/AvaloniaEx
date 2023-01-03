namespace Macabresoft.AvaloniaEx.Sample.Models;

using Macabresoft.Core;

public abstract class FileSystemObject : PropertyChangedNotifier {
    private string _name = string.Empty;

    public int Depth { get; set; }

    public string Name {
        get => this._name;
        set => this.Set(ref this._name, value);
    }
}