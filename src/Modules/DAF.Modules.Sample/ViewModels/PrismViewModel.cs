using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Prism.Mvvm;
using Prism.Navigation;

namespace DAF.Modules.Sample.ViewModels
{
    public class PrismViewModel : BindableBase, IDestructible
    {
        private readonly CompositeDisposable disposables = new();

        /// <summary>
        ///     prism:ViewModelLocator.AutoWireViewModel="True" 自动匹配
        /// </summary>
        public PrismViewModel()
        {
            Message = "Prism View";

            //Observable.Interval(TimeSpan.FromSeconds(3))
            //    .Subscribe(i => Message = $"Prism View {DateTime.Now}");

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => Debug.WriteLine($"Prism ViewModel {DateTime.Now}"))
                .DisposeWith(disposables);

            //Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        Debug.WriteLine($"Prism ViewModel 2 {DateTime.Now}");
            //        await Task.Delay(1000);
            //    }
            //});
        }

        public string Title { get; set; } = "Prism";

        public string Message { get; set; }

        /// <summary>
        ///     只有在region中remove掉view, 才会触发
        ///     navigate不会触发
        /// </summary>
        public void Destroy()
        {
            disposables.Dispose();
        }
    }
}