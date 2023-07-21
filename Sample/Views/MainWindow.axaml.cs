namespace Macabresoft.AvaloniaEx.Sample.Views;

using Macabresoft.AvaloniaEx.Sample.ViewModels;
using Unity;

public partial class MainWindow : BaseDialog {
    public MainWindow() {
    }

    [InjectionConstructor]
    public MainWindow(MainWindowViewModel viewModel) : base() {
        this.DataContext = viewModel;
        this.InitializeComponent();
    }
}