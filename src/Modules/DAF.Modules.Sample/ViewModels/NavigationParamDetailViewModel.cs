using DAF.Core.Mvvm;
using DAF.Modules.Sample.Models;
using Prism.Regions;

namespace DAF.Modules.Sample.ViewModels
{
    public class NavigationParamDetailViewModel : ViewModelBase, INavigationAware
    {
        public Person SelectedPerson { get; set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["person"] is Person person)
                SelectedPerson = person;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["person"] is Person person)
                return SelectedPerson != null && SelectedPerson.Name == person.Name;
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}