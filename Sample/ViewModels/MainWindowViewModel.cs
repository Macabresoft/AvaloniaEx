namespace Macabresoft.AvaloniaEx.Sample.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Macabresoft.AvaloniaEx.Sample.Models;
using Macabresoft.Core;
using ReactiveUI;

public class MainWindowViewModel : ReactiveObject {
    private readonly IUndoService _undoService;
    private bool _canUndo = true;

    private byte _colorValueA;

    private byte _colorValueB;
    private FakeFlagsEnum _selectedFakeFlagsEnumValue;
    private FileSystemObject? _selectedTreeItem;

    public MainWindowViewModel() : this(new UndoService()) {
    }

    public MainWindowViewModel(IUndoService undoService) {
        this._undoService = undoService;
        this.RenameCommand = ReactiveCommand.Create<string>(this.RenameChild);
        this.ToggleUndoCommand = ReactiveCommand.Create(() => this.CanUndo = !this.CanUndo);
        this.ViewSourceCommand = ReactiveCommand.Create(this.ViewSource);
        this.List = this.CreateList();
        this.Root = new[] { this.CreateFakeDirectory(2, 0, "Root") };
    }

    public bool CanUndo {
        get => this._canUndo;
        set {
            if (value != this._canUndo) {
                var originalValue = this._canUndo;
                this._undoService.Do(() =>
                {
                    this._canUndo = value;
                    this.RaisePropertyChanged();
                }, () =>
                {
                    this._canUndo = originalValue;
                    this.RaisePropertyChanged();
                });
            }
        }
    }

    public byte ColorValueA {
        get => this._colorValueA;
        set => this.RaiseAndSetIfChanged(ref this._colorValueA, value);
    }

    public byte ColorValueB {
        get => this._colorValueB;
        set => this.RaiseAndSetIfChanged(ref this._colorValueB, value);
    }

    public IReadOnlyCollection<FakeFlagsEnum> LimitedFlagsEnum { get; } = new[] { FakeFlagsEnum.First, FakeFlagsEnum.Second, FakeFlagsEnum.Fourth, FakeFlagsEnum.Eighth };

    public IReadOnlyCollection<string> List { get; }

    public string LoremIpsum => Resources.Lorem_Ipsum;

    public string LoremIpsumLarge => Resources.Lorem_Ipsom_Large;

    public ICommand RenameCommand { get; }

    public IReadOnlyCollection<FileSystemObject> Root { get; }

    public FakeFlagsEnum SelectedFakeFlagsEnumValue {
        get => this._selectedFakeFlagsEnumValue;
        set => this.RaiseAndSetIfChanged(ref this._selectedFakeFlagsEnumValue, value);
    }

    public FileSystemObject? SelectedTreeItem {
        get => this._selectedTreeItem;
        set => this.RaiseAndSetIfChanged(ref this._selectedTreeItem, value);
    }

    public ICommand ToggleUndoCommand { get; }

    public ICommand ViewSourceCommand { get; }

    private FakeDirectory CreateFakeDirectory(int maximumDepth, int currentDepth, string name) {
        var random = new Random();
        var directory = new FakeDirectory { Name = name, Depth = currentDepth };
        if (currentDepth < maximumDepth) {
            var numberOfDirectories = random.Next(maximumDepth - currentDepth, maximumDepth - currentDepth + 2);
            for (var i = 0; i < numberOfDirectories; i++) {
                directory.Children.Add(this.CreateFakeDirectory(maximumDepth, currentDepth + 1, $"Directory {i}"));
            }
        }

        var numberOfFiles = random.Next(0, 5);
        for (var i = 0; i < numberOfFiles; i++) {
            directory.Children.Add(new FakeFile { Name = $"File {i}", Depth = currentDepth });
        }

        return directory;
    }

    private IReadOnlyCollection<string> CreateList() {
        return new string[25].Select(x => Guid.NewGuid().ToString()).ToList();
    }

    private void RenameChild(string updatedName) {
        if (this.SelectedTreeItem != null) {
            this.SelectedTreeItem.Name = updatedName;
        }
    }

    private void ViewSource() {
        WebHelper.OpenInBrowser("https://github.com/Macabresoft/Macabresoft.AvaloniaEx");
    }
}