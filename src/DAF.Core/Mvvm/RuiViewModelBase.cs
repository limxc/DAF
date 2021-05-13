using System.Reactive.Disposables;
using Prism.Navigation;
using ReactiveUI;

namespace DAF.Core.Mvvm
{
    public abstract class RuiViewModelBase : ReactiveObject, IDestructible, IActivatableViewModel
    {
        protected CompositeDisposable Disposables { get; } = new();

        public ViewModelActivator Activator { get; } = new();

        public virtual void Destroy()
        {
            Disposables.Dispose();
        }
    }
}