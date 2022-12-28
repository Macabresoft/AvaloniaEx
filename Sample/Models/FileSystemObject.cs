namespace Macabresoft.AvaloniaEx.Sample.Models; 

public abstract class FileSystemObject {
    public string Name { get; set; } = string.Empty;
    
    public int Depth { get; set; }
}