using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DAF.Core.Mvvm;
using DAF.Modules.Sample.Models;
using Prism.Regions;

namespace DAF.Modules.Sample.ViewModels
{
    public class RegionContentViewModel : ViewModelBase, INavigationAware
    {
        private int _count;

        public RegionContentViewModel()
        {
            Persons = new ObservableCollection<Person>
            {
                new("a", "1", 21),
                new("b", "2", 22),
                new("c", "3", 25),
                new("d", "4", 29)
            };

            Task.Run(async () =>
            {
                while (true)
                {
                    _count++;
                    await Task.Delay(1000);
                    Msg = $"{_count} : {DateTime.Now}";
                }
            });
        }

        public string Msg { get; set; }
        public string Title { get; set; } = "RegionContent";
        public ObservableCollection<Person> Persons { get; set; }

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
    }
}