using System;
using Prism.Mvvm;
using Prism.Regions;

namespace DAF.Modules.Sample.ViewModels
{
    public class MultiViewModel : BindableBase, INavigationAware
    {
        public string Title { get; set; } = "MultiView Test";

        public int PageViews { get; set; }
        public DateTime LastTime { get; set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LastTime = DateTime.Now;
            PageViews++;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return PageViews / 3 != 1;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}