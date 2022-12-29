namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

public class FlagsComboBox : UserControl {
    public static readonly StyledProperty<Type> EnumTypeProperty =
        AvaloniaProperty.Register<FlagsComboBox, Type>(nameof(EnumType));

    public static readonly DirectProperty<FlagsComboBox, IEnumerable> ItemsProperty =
        ItemsControl.ItemsProperty.AddOwner<FlagsComboBox>(
            o => o.Items,
            (o, v) => o.Items = v);

    public static readonly StyledProperty<object> SelectedValueProperty =
        AvaloniaProperty.Register<FlagsComboBox, object>(nameof(SelectedValue));

    public static readonly DirectProperty<FlagsComboBox, ICommand> ToggleValueCommandProperty =
        AvaloniaProperty.RegisterDirect<FlagsComboBox, ICommand>(
            nameof(ToggleValueCommand),
            editor => editor.ToggleValueCommand);

    private IEnumerable _items;

    public FlagsComboBox() {
        this.ToggleValueCommand = ReactiveCommand.Create<object>(this.ToggleValue);
        this.InitializeComponent();
    }

    public Type EnumType {
        get => this.GetValue(EnumTypeProperty);
        set => this.SetValue(EnumTypeProperty, value);
    }

    public IEnumerable Items {
        get => this._items;
        set => this.SetAndRaise(ItemsProperty, ref this._items, value);
    }

    public object SelectedValue {
        get => this.GetValue(SelectedValueProperty);
        set => this.SetValue(SelectedValueProperty, value);
    }

    public ICommand ToggleValueCommand { get; }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

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