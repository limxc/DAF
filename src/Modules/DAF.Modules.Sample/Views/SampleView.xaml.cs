using System.Windows.Controls;
using Prism.Navigation;

namespace DAF.Modules.Sample.Views
{
    /// <summary>
    ///     SampleView.xaml 的交互逻辑
    /// </summary>
    public partial class SampleView : UserControl, IDestructible
    {
        public SampleView()
        {
            InitializeComponent();
            //使用binding+viewmodel注入 或 如下
            //host.ViewModel = ContainerLocator.Container.Resolve<Rui2ViewModel>();
        }

        public void Destroy()
        {
        }
    }
}