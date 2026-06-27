namespace Macabresoft.AvaloniaEx.Sample.Views;

using Macabresoft.AvaloniaEx.Sample.ViewModels;
using Unity;

public partial class MainWindow : BaseDialog {
    public MainWindow() {
    }
    
    public MainWindowViewModel? ViewModel => this.DataContext as MainWindowViewModel;

    [InjectionConstructor]
    public MainWindow(MainWindowViewModel viewModel) : base() {
        this.DataContext = viewModel;
        this.InitializeComponent();
    }
}