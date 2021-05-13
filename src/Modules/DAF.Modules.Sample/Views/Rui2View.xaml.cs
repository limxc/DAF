using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DAF.Modules.Sample.ViewModels;
using ReactiveUI;

namespace DAF.Modules.Sample.Views
{
    /// <summary>
    ///     RuiView.xaml 的交互逻辑
    /// </summary>
    public partial class Rui2View : IViewFor<Rui2ViewModel>
    {
        public Rui2View()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.Message, v => v.iMsg.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Message, v => v.dMsg.Text).DisposeWith(disposables);
                this.BindCommand(ViewModel, vm => vm.Refresh, v => v.btnRefresh).DisposeWith(disposables);

                Observable.Interval(TimeSpan.FromSeconds(1))
                    .Subscribe(_ => Debug.WriteLine($"Rui2 View {DateTime.Now}"))
                    .DisposeWith(disposables);
            });
        }

        public Rui2ViewModel ViewModel
        {
            get => (Rui2ViewModel) DataContext;
            set => DataContext = value;
        }

        object IViewFor.ViewModel
        {
            get => DataContext;
            set => DataContext = value;
        }
    }
}