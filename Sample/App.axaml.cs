using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Macabresoft.AvaloniaEx.Sample.ViewModels;
using Macabresoft.AvaloniaEx.Sample.Views;

namespace Macabresoft.AvaloniaEx.Sample {
    using Macabresoft.AvaloniaEx.Sample.Models;
    using Unity;
    using Unity.Lifetime;

    public class App : Application {
        private readonly IUnityContainer _unityContainer = new UnityContainer().RegisterType<IUndoService, UndoService>(new SingletonLifetimeManager());
        
        public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted() {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                var fileSystemObjectDisplayNameHandler = new FileSystemObjectDisplayNameHandler();
                DisplayNameHelper.Instance.RegisterHandler(typeof(FileSystemObject), fileSystemObjectDisplayNameHandler);
                DisplayNameHelper.Instance.RegisterHandler(typeof(FakeFile), fileSystemObjectDisplayNameHandler);
                DisplayNameHelper.Instance.RegisterHandler(typeof(FakeDirectory), fileSystemObjectDisplayNameHandler);
                DisplayNameHelper.Instance.RegisterHandler(typeof(FakeFlagsEnum), new EnumDisplayNameHandler());
                desktop.MainWindow = this._unityContainer.Resolve<MainWindow>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}