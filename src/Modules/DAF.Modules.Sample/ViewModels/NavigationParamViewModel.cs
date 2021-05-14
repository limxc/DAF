using System.Collections.ObjectModel;
using DAF.Core.Mvvm;
using DAF.Modules.Sample.Models;
using Prism.Commands;
using Prism.Regions;

namespace DAF.Modules.Sample.ViewModels
{
    public class NavigationParamViewModel : ViewModelBase
    {
        public NavigationParamViewModel(IRegionManager regionManager)
        {
            Persons = new ObservableCollection<Person>
            {
                new("a", "1", 21),
                new("b", "2", 22),
                new("c", "3", 25),
                new("d", "4", 29)
            };

            PersonSelectedCommand = new DelegateCommand<Person>(person =>
            {
                var parameters = new NavigationParameters();
                parameters.Add("person", person);

                if (person != null)
                    regionManager.RequestNavigate("NavigationParamDetailView", "NavigationParamDetailView", parameters);
            });
        }

        public string Title { get; set; } = "Navigation Parameter";
        public ObservableCollection<Person> Persons { get; set; }
        public DelegateCommand<Person> PersonSelectedCommand { get; set; }
    }
}