namespace Macabresoft.AvaloniaEx;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;

/// <summary>
/// A <see cref="ListBox"/> with alternating row colors.
/// </summary>
public sealed class AlternatingListBox : ListBox {
    /// <summary>
    /// The alternate background property.
    /// </summary>
    public static readonly StyledProperty<IBrush> AlternateBackgroundProperty =
        AvaloniaProperty.Register<AlternatingListBox, IBrush>(nameof(AlternateBackground));
    
    /// <summary>
    /// Gets or sets the alternate background color to be used on every other row.
    /// </summary>
    public IBrush AlternateBackground {
        get => this.GetValue(AlternateBackgroundProperty);
        set => this.SetValue(AlternateBackgroundProperty, value);
    }
    
    /// <inheritdoc />
    protected override IItemContainerGenerator CreateItemContainerGenerator() {
        return new AlternatingListBoxItemGenerator(this);    
    }
    
    private sealed class AlternatingListBoxItemGenerator : ItemContainerGenerator<ListBoxItem> {
        private readonly AlternatingListBox _owner;
        private bool _useAlternateColor;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AlternatingListBoxItemGenerator" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public AlternatingListBoxItemGenerator(AlternatingListBox owner) : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty) {
            this._owner = owner;
        }

        /// <inheritdoc />
        protected override IControl CreateContainer(object item) {
            var result = base.CreateContainer(item);
            if (this._useAlternateColor && result is ListBoxItem listBoxItem) {
                result.SetValue(TemplatedControl.BackgroundProperty, this._owner.AlternateBackground);
            }

            this._useAlternateColor = !this._useAlternateColor;
            return result;
        }
    }
}

