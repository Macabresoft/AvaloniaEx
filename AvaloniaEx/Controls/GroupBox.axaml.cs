namespace Macabresoft.AvaloniaEx;

using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using ReactiveUI;

public partial class GroupBox : HeaderedContentControl {
    public static readonly DirectProperty<GroupBox, bool> HideContentProperty =
        AvaloniaProperty.RegisterDirect<GroupBox, bool>(nameof(HideContent), x => x.HideContent);

    public static readonly StyledProperty<bool> ShowContentProperty = AvaloniaProperty.Register<GroupBox, bool>(
        nameof(ShowContent),
        true,
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly DirectProperty<GroupBox, ICommand> ToggleContentCommandProperty =
        AvaloniaProperty.RegisterDirect<GroupBox, ICommand>(nameof(ToggleContentCommand), x => x.ToggleContentCommand);

    public GroupBox() {
        this.ToggleContentCommand = ReactiveCommand.Create(this.CollapseContent);
        this.InitializeComponent();
    }

    public bool HideContent => !this.ShowContent;

    public bool ShowContent {
        get => this.GetValue(ShowContentProperty);
        set => this.SetValue(ShowContentProperty, value);
    }

    public ICommand ToggleContentCommand { get; }

    private void CollapseContent() {
        this.ShowContent = !this.ShowContent;
        this.RaisePropertyChanged(HideContentProperty, this.ShowContent, !this.ShowContent);
    }
}