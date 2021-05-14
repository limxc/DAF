using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DAF.Core.Mvvm;
using Prism.Events;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DAF.Modules.Sample.ViewModels
{
    public class Rui3ViewModel : RuiViewModelBase
    {
        private readonly IEventAggregator eventAggregator;

        public Rui3ViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Message = "Rui3 View";

            Refresh = ReactiveCommand.Create(() => { Message = $"Rui3 View {DateTime.Now}"; });
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => Debug.WriteLine($"Rui3 ViewModel {DateTime.Now}"))
                .DisposeWith(Disposables);
        }

        [Reactive] public string Message { get; set; }
        public string Title { get; set; } = "Rui3";
        public ReactiveCommand<Unit, Unit> Refresh { get; }
    }
}