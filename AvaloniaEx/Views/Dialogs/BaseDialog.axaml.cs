namespace Macabresoft.AvaloniaEx;

using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;

public partial class BaseDialog : Window, IWindow {
    public static readonly StyledProperty<ICommand> CloseCommandProperty =
        AvaloniaProperty.Register<BaseDialog, ICommand>(nameof(CloseCommand), defaultBindingMode: BindingMode.OneWay, defaultValue: WindowHelper.CloseDialogCommand);

    public static readonly StyledProperty<object> ContentLeftOfMenuProperty =
        AvaloniaProperty.Register<BaseDialog, object>(nameof(ContentLeftOfMenu), defaultBindingMode: BindingMode.OneWay);

    public static readonly StyledProperty<Menu> MenuProperty =
        AvaloniaProperty.Register<BaseDialog, Menu>(nameof(Menu), defaultBindingMode: BindingMode.OneWay);

    public static readonly StyledProperty<bool> ShowIconProperty =
        AvaloniaProperty.Register<BaseDialog, bool>(nameof(ShowIcon), defaultBindingMode: BindingMode.OneWay, defaultValue: true);

    public static readonly StyledProperty<bool> ShowMinimizeProperty =
        AvaloniaProperty.Register<BaseDialog, bool>(nameof(ShowMinimize), defaultBindingMode: BindingMode.OneWay, defaultValue: true);

    public static readonly StyledProperty<StreamGeometry> VectorIconProperty =
        AvaloniaProperty.Register<BaseDialog, StreamGeometry>(nameof(VectorIcon), defaultBindingMode: BindingMode.OneWay);

    public BaseDialog() {
        this.InitializeComponent();
    }

    public ICommand CloseCommand {
        get => this.GetValue(CloseCommandProperty);
        set => this.SetValue(CloseCommandProperty, value);
    }

    public object ContentLeftOfMenu {
        get => this.GetValue(ContentLeftOfMenuProperty);
        set => this.SetValue(ContentLeftOfMenuProperty, value);
    }

    public Menu Menu {
        get => this.GetValue(MenuProperty);
        set => this.SetValue(MenuProperty, value);
    }

    public bool ShowIcon {
        get => this.GetValue(ShowIconProperty);
        set => this.SetValue(ShowIconProperty, value);
    }

    public bool ShowMinimize {
        get => this.GetValue(ShowMinimizeProperty);
        set => this.SetValue(ShowMinimizeProperty, value);
    }

    public StreamGeometry VectorIcon {
        get => this.GetValue(VectorIconProperty);
        set => this.SetValue(VectorIconProperty, value);
    }

    private void TitleBar_OnDoubleTapped(object _, TappedEventArgs e) {
        if (this.CanResize) {
            WindowHelper.ToggleWindowState(this);
        }
    }

    private void TitleBar_OnPointerPressed(object _, PointerPressedEventArgs e) {
        this.BeginMoveDrag(e);
    }
}