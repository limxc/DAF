using System.Windows.Controls;
using DAF.Modules.Sample.Models;
using DAF.Modules.Sample.ViewModels;
using Prism.Common;
using Prism.Regions;

namespace DAF.Modules.Sample.Views
{
    public partial class RegionContentDetailView : UserControl
    {
        public RegionContentDetailView()
        {
            InitializeComponent();
            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
            {
                var context = (ObservableObject<object>) s;
                (DataContext as RegionContentDetailViewModel).Person = (Person) context.Value;
            };
        }
    }
}