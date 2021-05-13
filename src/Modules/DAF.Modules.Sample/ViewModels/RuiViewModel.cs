using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DAF.Modules.Sample.ViewModels
{
    public class RuiViewModel : ReactiveObject, IDestructible
    {
        private readonly IDisposable dis;

        /// <summary>
        ///     仅在viewmodel中使用rxui, prism匹配v-vm
        /// </summary>
        public RuiViewModel()
        {
            Message = "Rui View";

            dis = Observable.Interval(TimeSpan.FromSeconds(3))
                .Subscribe(_ =>
                {
                    Message = $"Rui View {DateTime.Now}";
                    Debug.WriteLine(Message);
                });
        }

        [Reactive] public string Message { get; set; }

        public void Destroy()
        {
            dis.Dispose();
        }
    }
}