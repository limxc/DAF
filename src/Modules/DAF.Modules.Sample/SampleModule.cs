using DAF.Modules.Sample.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DAF.Modules.Sample
{
    public class SampleModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SampleModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //Locator.CurrentMutable.InitializeSplat();
            //Locator.CurrentMutable.InitializeReactiveUI();
            //Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetAssembly(this.GetType()));
            //Locator.CurrentMutable.Register(() => new Rui2View(), typeof(IViewFor<Rui2ViewModel>));

            //_regionManager.RequestNavigate(RegionNames.ShellContentRegion, nameof(SampleView));

            ///将view注册到region, region中remove掉则无法navigate
            //_regionManager.RegisterViewWithRegion(RegionNames.SampleRegion1, typeof(PrismView));
            //_regionManager.RegisterViewWithRegion(RegionNames.SampleRegion2, typeof(RuiView));
            //_regionManager.RegisterViewWithRegion(RegionNames.SampleRegion3, typeof(Rui2View));
            //_regionManager.RegisterViewWithRegion(RegionNames.SampleRegion4, typeof(Rui3View));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SampleView>();
            containerRegistry.RegisterForNavigation<EmptyView>();

            ///将view注册到全局, region中remove掉依然可以navigate
            containerRegistry.RegisterForNavigation<PrismView>();
            containerRegistry.RegisterForNavigation<RuiView>();
            containerRegistry.RegisterForNavigation<Rui2View>();
            containerRegistry.RegisterForNavigation<Rui3View>();
        }
    }
}