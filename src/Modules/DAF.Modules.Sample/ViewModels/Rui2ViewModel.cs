using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Prism.Events;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DAF.Modules.Sample.ViewModels
{
    public class Rui2ViewModel : ReactiveObject, IDestructible, IActivatableViewModel //: RuiViewModelBase
    {
        private readonly IEventAggregator eventAggregator;

        public Rui2ViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Message = "Rui2 View";

            this.WhenActivated(d =>
            {
                Refresh = ReactiveCommand.Create(() => { Message = $"Rui2 View {DateTime.Now}"; }).DisposeWith(d);

                Observable.Interval(TimeSpan.FromSeconds(1))
                    .Subscribe(_ => Debug.WriteLine($"Rui2 ViewModel {DateTime.Now}"))
                    .DisposeWith(d);
            });
        }

        [Reactive] public string Message { get; set; }

        public ReactiveCommand<Unit, Unit> Refresh { get; private set; }

        public ViewModelActivator Activator { get; } = new();

        public void Destroy()
        {
        }
    }
}