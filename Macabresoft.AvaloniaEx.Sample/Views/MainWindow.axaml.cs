namespace Macabresoft.AvaloniaEx.Sample.Views;

using Avalonia.Markup.Xaml;

public class MainWindow : BaseDialog {
    public MainWindow() {
        this.InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}