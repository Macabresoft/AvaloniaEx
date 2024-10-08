namespace Macabresoft.AvaloniaEx;

using System;
using System.IO;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using ReactiveUI;

public partial class EditableSelectableItem : UserControl, IObserver<AvaloniaPropertyChangedEventArgs<string>> {
    public static readonly DirectProperty<EditableSelectableItem, ICommand> EditCommandProperty =
        AvaloniaProperty.RegisterDirect<EditableSelectableItem, ICommand>(
            nameof(EditCommand),
            editor => editor.EditCommand);

    public static readonly StyledProperty<SolidColorBrush> IconForegroundProperty =
        AvaloniaProperty.Register<EditableSelectableItem, SolidColorBrush>(nameof(IconForeground));

    public static readonly StyledProperty<StreamGeometry> IconProperty =
        AvaloniaProperty.Register<EditableSelectableItem, StreamGeometry>(nameof(Icon));

    public static readonly StyledProperty<bool> IsEditableProperty =
        AvaloniaProperty.Register<EditableSelectableItem, bool>(nameof(IsEditable), true);

    public static readonly DirectProperty<EditableSelectableItem, bool> IsEditingProperty =
        AvaloniaProperty.RegisterDirect<EditableSelectableItem, bool>(
            nameof(IsEditing),
            editor => editor.IsEditing,
            (editor, value) => editor.IsEditing = value);

    public static readonly StyledProperty<bool> IsFileNameProperty =
        AvaloniaProperty.Register<EditableSelectableItem, bool>(nameof(IsFileName));

    public static readonly StyledProperty<ICommand> TextCommittedCommandProperty =
        AvaloniaProperty.Register<EditableSelectableItem, ICommand>(nameof(TextCommittedCommand));

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<EditableSelectableItem, string>(nameof(Text), string.Empty);

    private bool _isEditing;

    public EditableSelectableItem() {
        TextProperty.Changed.Subscribe(this);

        this.EditCommand = ReactiveCommand.Create(
            () => Dispatcher.UIThread.Post(this.StartEdit),
            this.WhenAnyValue(x => x.IsEditable));

        this.InitializeComponent();
    }

    public ICommand EditCommand { get; }

    public StreamGeometry Icon {
        get => this.GetValue(IconProperty);
        set => this.SetValue(IconProperty, value);
    }

    public Brush IconForeground {
        get => this.GetValue(IconForegroundProperty);
        set => this.SetValue(IconForegroundProperty, value);
    }

    public bool IsEditable {
        get => this.GetValue(IsEditableProperty);
        set => this.SetValue(IsEditableProperty, value);
    }

    public bool IsEditing {
        get => this._isEditing;
        set => this.SetAndRaise(IsEditingProperty, ref this._isEditing, value);
    }

    public bool IsFileName {
        get => this.GetValue(IsFileNameProperty);
        set => this.SetValue(IsFileNameProperty, value);
    }

    public string Text {
        get => this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }

    public ICommand TextCommittedCommand {
        get => this.GetValue(TextCommittedCommandProperty);
        set => this.SetValue(TextCommittedCommandProperty, value);
    }

    private static bool CanEditItem(IDataContextProvider control) {
        return control?.DataContext != null && control is ISelectable { IsSelected: true };
    }

    private void CommitNewText(string newText) {
        if (this.TextCommittedCommand != null && this.TextCommittedCommand.CanExecute(newText)) {
            this.Text = newText;
            this.IsEditing = false;
            this.TextCommittedCommand.Execute(newText);
        }
    }

    private void EditableTextBox_OnPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e) {
        if (e.Property.Name == nameof(this.IsVisible)) {
            if (sender is TextBox { IsVisible: true } textBox) {
                textBox.Focus();
                textBox.SelectAll();
            }
        }
    }

    private string GetEditableText(string originalText) {
        var result = originalText;

        if (this.IsFileName) {
            result = Path.GetFileNameWithoutExtension(originalText);
        }

        return result;
    }

    private void Item_OnDoubleTapped(object sender, TappedEventArgs e) {
        var item = this.FindAncestor<ISelectable>() as Control;
        this.StartEdit();
        e.Handled |= this.IsEditing;
    }

    private void StartEdit() {
        if (this.FindAncestor<ISelectable>() is Control item &&
            this.IsEditable &&
            CanEditItem(item) &&
            this.TryGetEditableTextBox(out var textBox)) {
            textBox.Text = this.GetEditableText(this.Text);
            this.IsEditing = true;
        }
    }

    private void TextBox_OnKeyDown(object sender, KeyEventArgs e) {
        if (e.Key is Key.Enter or Key.Tab) {
            if (this.TryGetEditableTextBox(out var textBox)) {
                var newText = this.IsFileName ? $"{textBox.Text}{Path.GetExtension(this.Text)}" : textBox.Text;
                this.CommitNewText(newText);
                e.Handled = true;
            }
        }
        else if (e.Key == Key.Escape) {
            if (this.TryGetEditableTextBox(out var textBox)) {
                textBox.Text = this.GetEditableText(this.Text);
                this.IsEditing = false;
                e.Handled = true;
            }
        }
    }

    private void TextBox_OnLostFocus(object sender, RoutedEventArgs e) {
        this.IsEditing = false;
        if (this.TryGetEditableTextBox(out var textBox)) {
            textBox.Text = this.GetEditableText(this.Text);
        }
    }

    private bool TryGetEditableTextBox(out TextBox textBox) {
        textBox = this.FindControl<TextBox>("_editableTextBox");
        return textBox != null;
    }

    public void OnCompleted() {
    }

    public void OnError(Exception error) {
    }

    public void OnNext(AvaloniaPropertyChangedEventArgs<string> value) {
        if (this.TryGetEditableTextBox(out var textBox)) {
            textBox.Text = this.GetEditableText(this.Text);
        }
    }
}