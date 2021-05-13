#### 应用启动加载顺序(OnStartup):
    ConfigureViewModelLocator
    *Initialize : prism初始化顺序,执行完毕后所有类型可用,下一步显示窗体 
    CreateContainerExtension : ioc
    *CreateModuleCatalog : 需要动态加载的modules
    RegisterRequiredTypes : 查找到的Modules进行类型注册
    *RegisterTypes
    *ConfigureModuleCatalog : 手动加载Module
    *ConfigureRegionAdapterMappings : 默认控件的v-vm映射, 某些类型的控件需要手动实现
    *ConfigureDefaultRegionBehaviors : region的所有behaviors
    InitializeShell
    InitializeModules
    OnInitialized : Initialize完毕,显示窗体

#### 模块化
###### 1. 文件复制
    单dll文件: xcopy "$(TargetDir)$(TargetFileName)" "$(SolutionDir)\src\DAF.Shell\$(OutDir)\Modules\" /Y /S"
    全部文件:  xcopy "$(TargetDir)*" "$(SolutionDir)\src\DAF.Shell\$(OutDir)\Modules\" /Y /S
###### 2. IModule
    RegisterTypes : RegisterForNavigation
    OnInitialized : _regionManager.RequestNavigate
###### 3. ViewModelLocator(ViewModelLocatorExtension)
    默认方式->同文件夹内->外部dll(xxx.Core.ViewModels)
###### 4. IEventAggregator 
    Prism.Events.EventBase
###### 5. Navigation
    INavigationAware及子类IConfirmNavgationRequest: 传参方式(url/参数)
    导航日志IRegionNavigationJournal: 
        需要在导航过程中触发回调(x.Context.NavigationService.Journal)
        使用NavigationContext.NavigationService.Journal.GoBack()/GoForward()
###### 6. DialogService
    创建: ViewModel实现IDialogAware
    注册: containerRegistry.RegisterDialog<view,vm>();
    使用: IDialogService
    参数: DialogParameters(keyvaluepair)
###### 7. Aop(AspectInjector)

#### 解决方案
###### 1.模块控制与应用关闭
    View:
        xmlns:b="http://schemas.microsoft.com/xaml/behaviors"
        <b:Interaction.Triggers>
            <b:EventTrigger EventName="Closing">
                <b:InvokeCommandAction Command="{Binding CloseApp}" PassEventArgsToCommand="True" />
            </b:EventTrigger>
        </b:Interaction.Triggers>

    ViewModel: 需要单一弹窗时,绑定DelegateCommand, 在其中处理CompositeCommand (需要实现CanExecute)
               不需要弹窗时,直接绑定ApplicationCommands.RequestClose (不需要实现CanExecute)
    