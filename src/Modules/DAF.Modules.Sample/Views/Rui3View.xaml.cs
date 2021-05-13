using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using DAF.Modules.Sample.ViewModels;
using ReactiveUI;

namespace DAF.Modules.Sample.Views
{
    /// <summary>
    ///     RuiView.xaml 的交互逻辑
    /// </summary>
    public partial class Rui3View : UserControl, IViewFor<Rui3ViewModel>
    {
        public Rui3View()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.Message, v => v.iMsg.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Message, v => v.dMsg.Text).DisposeWith(disposables);
                //this.BindCommand(ViewModel, vm => vm.Refresh, v => v.btnRefresh).DisposeWith(disposables);

                Observable.FromEventPattern(btnRefresh, "Click")
                    //.Throttle(TimeSpan.FromSeconds(1))
                    .Select(_ => Unit.Default)
                    .ObserveOnDispatcher()
                    //.InvokeCommand(ViewModel.Refresh)
                    .InvokeCommand(this, x => x.ViewModel.Refresh)
                    .DisposeWith(disposables);

                Observable.Interval(TimeSpan.FromSeconds(1))
                    .Subscribe(_ => Debug.WriteLine($"Rui3 View {DateTime.Now}"))
                    .DisposeWith(disposables);
            });
        }

        public Rui3ViewModel ViewModel
        {
            get => (Rui3ViewModel) DataContext;
            set => DataContext = value;
        }

        object IViewFor.ViewModel
        {
            get => DataContext;
            set => DataContext = value;
        }
    }
}