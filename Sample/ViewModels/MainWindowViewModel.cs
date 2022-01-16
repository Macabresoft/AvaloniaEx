namespace Macabresoft.AvaloniaEx.Sample.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Macabresoft.AvaloniaEx.Sample.Models;
using Macabresoft.Core;
using ReactiveUI;

public class MainWindowViewModel : ReactiveObject {
    public MainWindowViewModel() {
        this.ViewSourceCommand = ReactiveCommand.Create(this.ViewSource);
        this.List = this.CreateList();
        this.Root = new[] { this.CreateFakeDirectory(2, 0, "Root") };
    }
    
    public IReadOnlyCollection<string> List { get; }

    public IReadOnlyCollection<FileSystemObject> Root { get; }

    public string LoremIpsum => Resources.Lorem_Ipsum;

    public string LoremIpsumLarge => Resources.Lorem_Ipsom_Large;

    public ICommand ViewSourceCommand { get; }

    private void ViewSource() {
        WebHelper.OpenInBrowser("https://github.com/Macabresoft/Macabresoft.AvaloniaEx");
    }

    private IReadOnlyCollection<string> CreateList() {
        return new string[25].Select(x => Guid.NewGuid().ToString()).ToList();
    }

    private FakeDirectory CreateFakeDirectory(int maximumDepth, int currentDepth, string name) {
        var random = new Random();
        var directory = new FakeDirectory { Name = name };
        if (currentDepth < maximumDepth) {
            var numberOfDirectories = random.Next(maximumDepth - currentDepth, maximumDepth - currentDepth + 2);
            for (var i = 0; i < numberOfDirectories; i++) {
                directory.Children.Add(this.CreateFakeDirectory(maximumDepth, currentDepth + 1, $"Directory {i} (depth {currentDepth})"));
            }
        }
        
        var numberOfFiles = random.Next(0, 5);
        for (var i = 0; i < numberOfFiles; i++) {
            directory.Children.Add(new FakeFile { Name = $"File {i} (depth {currentDepth})" });
        }

        return directory;
    }
}