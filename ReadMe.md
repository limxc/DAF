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
    Message定义 :  PubSubEvent<T>: Prism.Events.EventBase
    ea.GetEvent<MessageSentEvent>().Subscribe()多个重载, 实现filter, thread, alive.
    ea.GetEvent<MessageSentEvent>().Publish()
###### 5. Navigation 
    导航日志IRegionNavigationJournal: 
        可在OnNavigatedTo()方法中获取navigationContext.NavigationService.Journal
        也可在导航过程中触发回调(regionManager.RequestNavigation()方法中的回调函数)
        使用NavigationContext.NavigationService.Journal.GoBack()/GoForward()
    INavigationAware 跳转前/后, 是否新建
    IConfirmNavigationRequest(继承自INavigationAware) 跳转确认,传参方式(url/参数)
    NavigationParameters
###### 6. DialogService
    创建: ViewModel实现IDialogAware
    注册: containerRegistry.RegisterDialog<view,vm>();
    使用: IDialogService
    参数: DialogParameters(keyvaluepair)
###### 7. IActiveAware
    类似于ReactiveUI中的IActivatableViewModel, 当前vm是否处于激活状态
    IActiveAware.IsActiveChanged事件用于触发CompositeCommand的CanExecuteChanged(DelegateCommand实现了IActiveAware),
    多数时刻,仅在IsActive的Setter中处理状态切换
###### 8. Region
    RegionManager.RegionContext 共享上下文(列表/详情)
        需要在详情页view的cs文件中处理事件
            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
            {
                var context = (ObservableObject<object>) s;
                (DataContext as RegionContextDetailViewModel).People = (People) context.Value;
            };
    IRegionMemberLifetime 控制生命周期(Gets a value indicating whether this instance should be kept-alive upon deactivation.), 搭配INavigationAware.IsNavigationTarget(false)
###### 9. Aop(AspectInjector)

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
    