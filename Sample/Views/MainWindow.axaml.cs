namespace Macabresoft.AvaloniaEx.Sample.Views;

using Avalonia.Markup.Xaml;
using Macabresoft.AvaloniaEx.Sample.ViewModels;
using Unity;

public class MainWindow : BaseDialog {
    public MainWindow() {
    }
    
    [InjectionConstructor]
    public MainWindow(MainWindowViewModel viewModel) {
        this.DataContext = viewModel;
        this.InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}