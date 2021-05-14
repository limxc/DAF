using System.Diagnostics;
using System.Linq;
using DAF.Modules.Sample.Views;
using DAF.Services.Interfaces;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;

namespace DAF.Modules.Sample.ViewModels
{
    public class SampleViewModel : BindableBase, IDestructible, INavigationAware
    {
        private readonly IContainerProvider _container;
        private readonly IRegionManager _regionManager;

        public SampleViewModel(IRegionManager regionManager, IContainerProvider container)
        {
            _regionManager = regionManager;
            _container = container;

            LoadCommand = new DelegateCommand<string>(Load);
            UnloadCommand = new DelegateCommand<string>(UnLoad);
            //_regionManager.RegisterViewWithRegion("SampleRegion"1, typeof(PrismView));
            //_regionManager.RegisterViewWithRegion("SampleRegion"2, typeof(RuiView));
            //_regionManager.RegisterViewWithRegion("SampleRegion"3, typeof(Rui2View));
            //_regionManager.RegisterViewWithRegion("SampleRegion"4, typeof(Rui3View)); 

            var ms = _container.Resolve<IMessageService>();
            Debug.WriteLine(ms.GetMessage());
        }

        public DelegateCommand<string> LoadCommand { get; }
        public DelegateCommand<string> UnloadCommand { get; }

        public void Destroy()
        {
            //UnloadPrism.Execute();
            //UnloadRui.Execute();
            //UnloadRui2.Execute();
            //UnloadRui3.Execute();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Load("PrismView");
            Load("RuiView");
            Load("Rui2View");
            Load("Rui3View");

            Load("RegionContentView");
            _regionManager.RequestNavigate("RegionContentDetailView", nameof(RegionContentDetailView));

            Load("MultiView");

            Load("NavigationParamView");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void Load(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("TabRegion", navigatePath);
        }

        private void UnLoad(string navigatePath)
        {
            if (navigatePath != null)
            {
                var views = _regionManager.Regions["TabRegion"].Views;

                foreach (var v in views.Where(p => p.GetType().Name == navigatePath))
                    _regionManager.Regions["TabRegion"].Remove(v);
            }
        }
    }
}