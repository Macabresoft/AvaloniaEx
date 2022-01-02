#nullable enable
namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;

/// <summary>
/// The accent color for a <see cref="MacabreTheme" />.
/// </summary>
public enum ThemeAccent {
    Purple,
    Green
}

/// <summary>
/// A Macabresoft theme.
/// </summary>
public class MacabreTheme : AvaloniaObject, IStyle, IResourceProvider {
    /// <summary>
    /// The accent property.
    /// </summary>
    public static readonly StyledProperty<ThemeAccent> AccentProperty =
        AvaloniaProperty.Register<MacabreTheme, ThemeAccent>(nameof(Accent));

    private readonly Dictionary<ThemeAccent, Styles> _accents = new();
    private Styles _controlStyles = new();
    private bool _isLoading;
    private IStyle? _loaded;
    private Styles _sharedStyles = new();

    /// <inheritdoc />
    public event EventHandler OwnerChanged {
        add {
            if (this.Loaded is IResourceProvider rp) {
                rp.OwnerChanged += value;
            }
        }
        remove {
            if (this.Loaded is IResourceProvider rp) {
                rp.OwnerChanged -= value;
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluentTheme" /> class.
    /// </summary>
    /// <param name="baseUri">The base URL for the XAML context.</param>
    public MacabreTheme(Uri baseUri) {
        this.InitializeStyles(baseUri);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FluentTheme" /> class.
    /// </summary>
    /// <param name="serviceProvider">The XAML service provider.</param>
    public MacabreTheme(IServiceProvider serviceProvider) {
        if (serviceProvider.GetService(typeof(IUriContext)) is IUriContext context) {
            this.InitializeStyles(context.BaseUri);
        }
        else {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Gets the loaded style.
    /// </summary>
    public IStyle Loaded {
        get {
            if (this._loaded == null) {
                this._isLoading = true;

                if (this._accents[this.Accent] is { } accent) {
                    this._loaded = new Styles { this._sharedStyles, accent[0], this._controlStyles[0] };
                }

                this._isLoading = false;
            }

            return this._loaded!;
        }
    }

    /// <inheritdoc />
    public IResourceHost? Owner => (this.Loaded as IResourceProvider)?.Owner;

    /// <summary>
    /// Gets or sets the theme's color accent.
    /// </summary>
    public ThemeAccent Accent {
        get => this.GetValue(AccentProperty);
        set => this.SetValue(AccentProperty, value);
    }

    /// <inheritdoc />
    IReadOnlyList<IStyle> IStyle.Children => this._loaded?.Children ?? Array.Empty<IStyle>();

    /// <inheritdoc />
    bool IResourceNode.HasResources => (this.Loaded as IResourceProvider)?.HasResources ?? false;

    /// <inheritdoc />
    public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) {
        return this.Loaded.TryAttach(target, host);
    }

    /// <inheritdoc />
    public bool TryGetResource(object key, out object? value) {
        if (!this._isLoading && this.Loaded is IResourceProvider provider) {
            return provider.TryGetResource(key, out value);
        }

        value = null;
        return false;
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
        base.OnPropertyChanged(change);
        if (change.Property == AccentProperty) {
            if (this.Loaded is Styles loaded && this._accents[this.Accent] is { } accent) {
                loaded[1] = accent[0];
            }
        }
    }

    void IResourceProvider.AddOwner(IResourceHost owner) {
        if (this.Loaded is IResourceProvider provider) {
            provider.AddOwner(owner);
        }
    }

    private void InitializeStyles(Uri baseUri) {
        this._sharedStyles = new Styles {
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Accents/AccentColors.xaml")
            },
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Accents/Base.xaml")
            },
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Controls/FluentControls.xaml")
            },
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Accents/BaseDark.xaml")
            },
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Accents/FluentControlResourcesDark.xaml")
            }
        };

        this._controlStyles = new Styles {
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Macabresoft.AvaloniaEx/Theme/Base.axaml")
            }
        };

        this._accents[ThemeAccent.Purple] = new Styles {
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Macabresoft.AvaloniaEx/Theme/Accents/Purple.axaml")
            }
        };

        this._accents[ThemeAccent.Green] = new Styles {
            new StyleInclude(baseUri) {
                Source = new Uri("avares://Macabresoft.AvaloniaEx/Theme/Accents/Green.axaml")
            }
        };
    }

    void IResourceProvider.RemoveOwner(IResourceHost owner) {
        if (this.Loaded is IResourceProvider provider) {
            provider.RemoveOwner(owner);
        }
    }
}