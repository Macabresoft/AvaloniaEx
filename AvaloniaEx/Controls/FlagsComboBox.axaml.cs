namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using ReactiveUI;

public partial class FlagsComboBox : UserControl {
    public static readonly StyledProperty<Type> EnumTypeProperty =
        AvaloniaProperty.Register<FlagsComboBox, Type>(nameof(EnumType));

    public static readonly DirectProperty<FlagsComboBox, IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.RegisterDirect<FlagsComboBox, IEnumerable>(
            nameof(ItemsSource),
            o => o.ItemsSource,
            (o, v) => o.ItemsSource = v);

    public static readonly StyledProperty<object> SelectedValueProperty =
        AvaloniaProperty.Register<FlagsComboBox, object>(nameof(SelectedValue), defaultBindingMode: BindingMode.TwoWay);

    public static readonly DirectProperty<FlagsComboBox, ICommand> ToggleValueCommandProperty =
        AvaloniaProperty.RegisterDirect<FlagsComboBox, ICommand>(
            nameof(ToggleValueCommand),
            editor => editor.ToggleValueCommand);

    private IEnumerable _itemsSource;

    public FlagsComboBox() {
        this.ToggleValueCommand = ReactiveCommand.Create<object>(this.ToggleValue);
        this.InitializeComponent();
    }

    public Type EnumType {
        get => this.GetValue(EnumTypeProperty);
        set => this.SetValue(EnumTypeProperty, value);
    }

    public IEnumerable ItemsSource {
        get => this._itemsSource;
        set => this.SetAndRaise(ItemsSourceProperty, ref this._itemsSource, value);
    }

    public object SelectedValue {
        get => this.GetValue(SelectedValueProperty);
        set => this.SetValue(SelectedValueProperty, value);
    }

    public ICommand ToggleValueCommand { get; }

    private void SelectingItemsControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        if (sender is ComboBox comboBox) {
            comboBox.SelectedItem = null;
        }
    }

    private void ToggleValue(object value) {
        var original = Convert.ToInt32(this.SelectedValue);
        var toggled = Convert.ToInt32(value);
        var wasTurnedOff = (original & toggled) == toggled;
        var newValue = (wasTurnedOff ? original & ~toggled : original | toggled).ToString();
        this.SelectedValue = Enum.Parse(this.EnumType, newValue);
    }
}