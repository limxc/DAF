using System.IO;
using System.Windows;
using System.Windows.Controls;
using DAF.Core.Adapters;
using DAF.Core.ApplicationService;
using DAF.Core.Extensions;
using DAF.Services;
using DAF.Services.Interfaces;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace DAF.Shell
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
                viewType.PrismVmLocator() ?? viewType.SameNsVmLocator() ?? viewType.SameNsVmLocator());
        }

        /// <summary>
        ///     prism初始化顺序,执行完毕后所有类型可用,下一步显示窗体
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        ///     需要动态加载的modules
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            if (!Directory.Exists("Modules"))
                Directory.CreateDirectory("Modules");
            return new DirectoryModuleCatalog
            {
                ModulePath = @".\\Modules"
            };
        }

        /// <summary>
        ///     查找到的Modules进行类型注册
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
        }

        /// <summary>
        ///     注册
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Shell>();
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        /// <summary>
        ///     手动加载Module
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<ModuleNameModule>();
            //moduleCatalog.AddModule<RxPModule>();
            //moduleCatalog.AddModule<SampleModule>();
        }

        /// <summary>
        ///     默认控件的v-vm映射, 某些类型的控件需要手动实现
        /// </summary>
        /// <param name="regionAdapterMappings"></param>
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping<StackPanel>(
                Container.Resolve<StackPanelRegionAdapter>());
        }

        /// <summary>
        ///     region的所有behaviors
        /// </summary>
        /// <param name="regionBehaviors"></param>
        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
        }
    }
}