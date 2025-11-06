namespace Macabresoft.AvaloniaEx;

using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;

/// <summary>
/// A <see cref="IDataObject" /> that wraps an <see cref="object" />.
/// </summary>
public class GenericDataObject : IDataTransfer {
    private readonly string _name;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericDataObject" /> class.
    /// </summary>
    public GenericDataObject(object genericObject, string name) {
        this.GenericObject = genericObject ?? throw new ArgumentNullException(nameof(genericObject));
        this._name = name;
    }

    /// <summary>
    /// Gets the generic object.
    /// </summary>
    public object GenericObject { get; }

    /// <inheritdoc />
    public override string ToString() => this._name;

    /// <inheritdoc />
    public void Dispose() {
    }

    /// <inheritdoc />
    public IReadOnlyList<DataFormat> Formats { get; } = [];
    
    /// <inheritdoc />
    public IReadOnlyList<IDataTransferItem> Items { get; } = [];
}