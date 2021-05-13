using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DAF.Core;
using DAF.Core.ApplicationService;
using Prism.Commands;
using Prism.Regions;

namespace DAF.Shell.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;
            ApplicationCommands = applicationCommands;

            Nav2Sample =
                new DelegateCommand(() => _regionManager.RequestNavigate(RegionNames.ShellContentRegion, "SampleView"));

            #region 应用关闭确认

            void Close(CancelEventArgs e)
            {
                applicationCommands.Save.Execute(null);

                var requestClose = applicationCommands.RequestClose;
                //无限制
                if (!requestClose.RegisteredCommands.Any())
                    return;

                //可以关闭, 执行并关闭
                if (requestClose.CanExecute(null))
                {
                    requestClose.Execute(null);
                    return;
                }

                //不可关闭, 询问
                if (MessageBox.Show("something is running, close anyway?", "alert",
                    MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    e.Cancel = true;
            }

            applicationCommands.Save.RegisterCommand(new DelegateCommand(() => { Debug.WriteLine("save anyway"); },
                () => false));

            //方式1 可弹窗确认  Command="{Binding CloseApp}"
            CloseApp = new DelegateCommand<CancelEventArgs>(Close);
            applicationCommands.RequestClose.RegisterCommand(new DelegateCommand(() => { }, () => false));

            //方式2 不可弹窗  Command="{Binding ApplicationCommands.RequestClose}"
            //applicationCommands.RequestClose.RegisterCommand(new DelegateCommand<CancelEventArgs>(e =>
            //{
            //    e.Cancel = true;
            //}));

            #endregion

            Task.Run(async () =>
            {
                await Task.Delay(3000);
                Title += DateTime.Now;
            });
        }

        public string Title { get; set; } = "DAF";

        public DelegateCommand<CancelEventArgs> CloseApp { get; set; }

        public IApplicationCommands ApplicationCommands { get; set; }
        public DelegateCommand Nav2Sample { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}