namespace Macabresoft.AvaloniaEx;

using Unity;
using Unity.Extension;
using Unity.Lifetime;

/// <summary>
/// A <see cref="UnityContainerExtension"/> that should be registered with an application's <see cref="UnityContainer"/>.
/// </summary>
public class AvaloniaUnityContainerExtension : UnityContainerExtension {
    protected override void Initialize() {
        Container.RegisterType<IFileSystemService, FileSystemService>(new SingletonLifetimeManager())
                    .RegisterType<ILoggingService, LoggingService>(new SingletonLifetimeManager())
                    .RegisterType<IProcessService, ProcessService>(new SingletonLifetimeManager())
                    .RegisterType<IUndoService, UndoService>(new SingletonLifetimeManager())
                    .RegisterInstance(DisplayNameHelper.Instance);
    }
}