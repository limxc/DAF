using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using Prism.Navigation;

namespace DAF.Modules.Sample.Views
{
    /// <summary>
    ///     PrismView.xaml 的交互逻辑
    /// </summary>
    public partial class PrismView : UserControl, IDestructible
    {
        private readonly CompositeDisposable disposables = new();

        public PrismView()
        {
            InitializeComponent();

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => Debug.WriteLine($"Prism View {DateTime.Now}"))
                .DisposeWith(disposables);
        }

        public void Destroy()
        {
            disposables.Dispose();
        }
    }
}