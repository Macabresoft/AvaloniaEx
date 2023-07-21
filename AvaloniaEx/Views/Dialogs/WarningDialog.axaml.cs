namespace Macabresoft.AvaloniaEx;

using Avalonia;
using Avalonia.Markup.Xaml;
using Unity;

public partial class WarningDialog : BaseDialog {
    public static readonly StyledProperty<string> WarningMessageProperty =
        AvaloniaProperty.Register<WarningDialog, string>(nameof(WarningMessage));


    [InjectionConstructor]
    public WarningDialog() : base() {
        this.InitializeComponent();
    }

    public string WarningMessage {
        get => this.GetValue(WarningMessageProperty);
        set => this.SetValue(WarningMessageProperty, value);
    }
}