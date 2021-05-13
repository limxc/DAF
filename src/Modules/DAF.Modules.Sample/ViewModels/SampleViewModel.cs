using System;
using DAF.Modules.Sample.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;

namespace DAF.Modules.Sample.ViewModels
{
    public class SampleViewModel : BindableBase, IDestructible, INavigationAware
    {
        private readonly IRegionManager regionManager;

        public SampleViewModel(IRegionManager regionManager, IContainerProvider container)
        {
            LoadPrism = new DelegateCommand(() => Load("SampleRegion1", typeof(PrismView)));
            UnloadPrism = new DelegateCommand(() => Unload("SampleRegion1"));
            Nav2Empty = new DelegateCommand(() => regionManager.RequestNavigate("SampleRegion1", nameof(EmptyView)));

            LoadRui = new DelegateCommand(() => Load("SampleRegion2", typeof(RuiView)));
            UnloadRui = new DelegateCommand(() => Unload("SampleRegion2"));

            LoadRui2 = new DelegateCommand(() => Load("SampleRegion3", typeof(Rui2View)));
            UnloadRui2 = new DelegateCommand(() => Deactivate("SampleRegion3"));

            LoadRui3 = new DelegateCommand(() => Load("SampleRegion4", typeof(Rui3View)));
            UnloadRui3 = new DelegateCommand(() => Unload("SampleRegion4"));

            this.regionManager = regionManager;

            //regionManager.RegisterViewWithRegion("SampleRegion"1, typeof(PrismView));
            //regionManager.RegisterViewWithRegion("SampleRegion"2, typeof(RuiView));
            //regionManager.RegisterViewWithRegion("SampleRegion"3, typeof(Rui2View));
            //regionManager.RegisterViewWithRegion("SampleRegion"4, typeof(Rui3View));
        }

        public Rui2ViewModel Rui2 { get; set; }

        public DelegateCommand LoadPrism { get; }
        public DelegateCommand UnloadPrism { get; }
        public DelegateCommand Nav2Empty { get; }

        public DelegateCommand LoadRui { get; }
        public DelegateCommand UnloadRui { get; }
        public DelegateCommand LoadRui2 { get; }
        public DelegateCommand UnloadRui2 { get; }
        public DelegateCommand LoadRui3 { get; }
        public DelegateCommand UnloadRui3 { get; }

        public void Destroy()
        {
            //UnloadPrism.Execute();
            //UnloadRui.Execute();
            //UnloadRui2.Execute();
            //UnloadRui3.Execute();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void Load(string region, Type type)
        {
            regionManager.RequestNavigate(region, type.Name);

            //regionManager.RegisterViewWithRegion(region, type);
        }

        private void Unload(string regionName)
        {
            regionManager.Regions[regionName].RemoveAll();
        }

        private void Deactivate(string regionName)
        {
            //var region = regionManager.Regions[regionName];
            //foreach (var view in region.ActiveViews)
            //{
            //    region.Deactivate(view);
            //}

            regionManager.RequestNavigate("SampleRegion3", nameof(EmptyView));
        }
    }
}