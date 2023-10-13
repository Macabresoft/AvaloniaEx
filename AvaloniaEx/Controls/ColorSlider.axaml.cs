namespace Macabresoft.AvaloniaEx;

using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

public partial class ColorSlider : UserControl, IObserver<AvaloniaPropertyChangedEventArgs<byte>> {
    public static readonly DirectProperty<ColorSlider, byte> ValueDisplayProperty =
        AvaloniaProperty.RegisterDirect<ColorSlider, byte>(
            nameof(ValueDisplay),
            editor => editor.ValueDisplay,
            (editor, value) => editor.ValueDisplay = value);

    public static readonly StyledProperty<byte> ValueProperty =
        AvaloniaProperty.Register<ColorSlider, byte>(nameof(Value), defaultBindingMode: BindingMode.TwoWay);

    private IDisposable _pointerReleaseDispose;
    private byte _valueDisplay;

    public ColorSlider() {
        ValueProperty.Changed.Subscribe(this);
        this.InitializeComponent();
    }

    public byte Value {
        get => this.GetValue(ValueProperty);
        set => this.SetValue(ValueProperty, value);
    }

    public byte ValueDisplay {
        get => this._valueDisplay;
        set => this.SetAndRaise(ValueDisplayProperty, ref this._valueDisplay, value);
    }

    public void OnCompleted() {
    }

    public void OnError(Exception error) {
    }

    public void OnNext(AvaloniaPropertyChangedEventArgs<byte> value) {
        if (value.Sender == this && value.NewValue.HasValue && value.NewValue.Value != this.ValueDisplay) {
            this.ValueDisplay = value.NewValue.Value;
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
        base.OnApplyTemplate(e);

        this._pointerReleaseDispose?.Dispose();

        if (this.Content is Slider slider) {
            this._pointerReleaseDispose = slider.AddDisposableHandler(PointerReleasedEvent, this.OnPointerReleased, RoutingStrategies.Tunnel);
        }
    }

    private void OnPointerReleased(object sender, PointerReleasedEventArgs e) {
        if (this.Value != this.ValueDisplay) {
            this.Value = this.ValueDisplay;
        }
    }

    private void Slider_OnKeyUp(object sender, KeyEventArgs e) {
        if (this.Value != this.ValueDisplay && sender is Slider slider && this.Content is Slider content && content == slider) {
            this.Value = this.ValueDisplay;
        }
    }
}