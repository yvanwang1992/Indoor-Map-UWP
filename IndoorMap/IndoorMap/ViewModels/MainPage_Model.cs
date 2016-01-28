using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using IndoorMap.Controller;
using IndoorMap.Models;
using System.Diagnostics;
using IndoorMap.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class MainPage_Model : ViewModelBase<MainPage_Model> 
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令
        bool isLoadSuscribe = false;

        private SubMallListPage_Model subMallListPageModel;
        public SubMallListPage_Model SubMallListPageModel
        {
            get
            {
                if(subMallListPageModel == null)
                {
                    subMallListPageModel = new SubMallListPage_Model();
                }
                return subMallListPageModel;
            }
        }

        private SubMapPage_Model subMapPageModel;
        public SubMapPage_Model SubMapPageModel
        {
            get
            {
                if (subMapPageModel == null)
                {
                    subMapPageModel = new SubMapPage_Model();
                }
                return subMapPageModel;
            }
        }

        const string NoResultString =  "未能查询到结果";

        public MainPage_Model()
        {
            if (IsInDesignMode)
            {
                InitPaneListData();
            }
            subMapPageModel = new SubMapPage_Model();
            subMallListPageModel = new SubMallListPage_Model();

            InitPaneListData();

        }

        #region --------------------   Properties   -------------------


        public String AutoSuggestText
        {
            get { return _AutoSuggestTextLocator(this).Value; }
            set { _AutoSuggestTextLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String AutoSuggestText Setup
        protected Property<String> _AutoSuggestText = new Property<String> { LocatorFunc = _AutoSuggestTextLocator };
        static Func<BindableBase, ValueContainer<String>> _AutoSuggestTextLocator = RegisterContainerLocator<String>("AutoSuggestText", model => model.Initialize("AutoSuggestText", ref model._AutoSuggestText, ref _AutoSuggestTextLocator, _AutoSuggestTextDefaultValueFactory));
        static Func<String> _AutoSuggestTextDefaultValueFactory = () => string.Empty;
        #endregion

        public List<MallModel> AutoSuggests
        {
            get { return _AutoSuggestsLocator(this).Value; }
            set { _AutoSuggestsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property List<MallModel> AutoSuggests Setup
        protected Property<List<MallModel>> _AutoSuggests = new Property<List<MallModel>> { LocatorFunc = _AutoSuggestsLocator };
        static Func<BindableBase, ValueContainer<List<MallModel>>> _AutoSuggestsLocator = RegisterContainerLocator<List<MallModel>>("AutoSuggests", model => model.Initialize("AutoSuggests", ref model._AutoSuggests, ref _AutoSuggestsLocator, _AutoSuggestsDefaultValueFactory));
        static Func<List<MallModel>> _AutoSuggestsDefaultValueFactory = () => new List<MallModel>() {};
        #endregion
         
        public Visibility MainVisibility
        {
            get { return _MainVisibilityLocator(this).Value; }
            set { _MainVisibilityLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Visibility MainVisibility Setup
        protected Property<Visibility> _MainVisibility = new Property<Visibility> { LocatorFunc = _MainVisibilityLocator };
        static Func<BindableBase, ValueContainer<Visibility>> _MainVisibilityLocator = RegisterContainerLocator<Visibility>("MainVisibility", model => model.Initialize("MainVisibility", ref model._MainVisibility, ref _MainVisibilityLocator, _MainVisibilityDefaultValueFactory));
        static Func<Visibility> _MainVisibilityDefaultValueFactory = () => Visibility.Visible;
        #endregion

        public Visibility FrameVisibility
        {
            get { return _FrameVisibilityLocator(this).Value; }
            set { _FrameVisibilityLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Visibility FrameVisibility Setup
        protected Property<Visibility> _FrameVisibility = new Property<Visibility> { LocatorFunc = _FrameVisibilityLocator };
        static Func<BindableBase, ValueContainer<Visibility>> _FrameVisibilityLocator = RegisterContainerLocator<Visibility>("FrameVisibility", model => model.Initialize("FrameVisibility", ref model._FrameVisibility, ref _FrameVisibilityLocator, _FrameVisibilityDefaultValueFactory));
        static Func<Visibility> _FrameVisibilityDefaultValueFactory = () => Visibility.Visible;
        #endregion

        public bool IsHumburgShow
        {
            get { return _IsHumburgShowLocator(this).Value; }
            set { _IsHumburgShowLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsHumburgShow Setup
        protected Property<bool> _IsHumburgShow = new Property<bool> { LocatorFunc = _IsHumburgShowLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsHumburgShowLocator = RegisterContainerLocator<bool>("IsHumburgShow", model => model.Initialize("IsHumburgShow", ref model._IsHumburgShow, ref _IsHumburgShowLocator, _IsHumburgShowDefaultValueFactory));
        static Func<bool> _IsHumburgShowDefaultValueFactory = () => false;
        #endregion

        //propvm tab tab string tab Title
        public String Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String Title Setup
        protected Property<String> _Title = new Property<String> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<String>> _TitleLocator = RegisterContainerLocator<String>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<String> _TitleDefaultValueFactory = ()=>"Title is Here";
        #endregion

        private Frame frameSplitContent;
        public Frame FrameSplitContent
        {
            get { return frameSplitContent;  }
        }

        //SelectedPaneDownIndex
        public int SelectedPaneDownIndex
        {
            get { return _SelectedPaneDownIndexLocator(this).Value; }
            set
            {
                _SelectedPaneDownIndexLocator(this).SetValueAndTryNotify(value);
                //SubNavigateTo(SelectedPaneDownIndex.type);
                //Navigate To SubPage :  List.Map..ect 
            }
        }
        #region Property int SelectedPaneDownIndex Setup
        protected Property<int> _SelectedPaneDownIndex = new Property<int> { LocatorFunc = _SelectedPaneDownIndexLocator };
        static Func<BindableBase, ValueContainer<int>> _SelectedPaneDownIndexLocator = RegisterContainerLocator<int>("SelectedPaneDownIndex", model => model.Initialize("SelectedPaneDownIndex", ref model._SelectedPaneDownIndex, ref _SelectedPaneDownIndexLocator, _SelectedPaneDownIndexDefaultValueFactory));
        static Func<int> _SelectedPaneDownIndexDefaultValueFactory = () => { return 0; };
        #endregion


        //SelectedPaneDownItem
        public PaneModel SelectedPaneDownItem
        {
            get { return _SelectedPaneDownItemLocator(this).Value; }
            set
            {
                _SelectedPaneDownItemLocator(this).SetValueAndTryNotify(value);
                //SubNavigateTo(SelectedPaneDownItem.type);
                //Navigate To SubPage :  List.Map..ect 
            }
        }
        #region Property PaneModel SelectedPaneDownItem Setup
        protected Property<PaneModel> _SelectedPaneDownItem = new Property<PaneModel> { LocatorFunc = _SelectedPaneDownItemLocator };
        static Func<BindableBase, ValueContainer<PaneModel>> _SelectedPaneDownItemLocator = RegisterContainerLocator<PaneModel>("SelectedPaneDownItem", model => model.Initialize("SelectedPaneDownItem", ref model._SelectedPaneDownItem, ref _SelectedPaneDownItemLocator, _SelectedPaneDownItemDefaultValueFactory));
        static Func<PaneModel> _SelectedPaneDownItemDefaultValueFactory = () => { return new PaneModel(); };
        #endregion


        public List<PaneModel> PaneDownList
        {
            get { return _PaneDownListLocator(this).Value; }
            set { _PaneDownListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property List<PaneModel> PaneDownList Setup
        protected Property<List<PaneModel>> _PaneDownList = new Property<List<PaneModel>> { LocatorFunc = _PaneDownListLocator };
        static Func<BindableBase, ValueContainer<List<PaneModel>>> _PaneDownListLocator = RegisterContainerLocator<List<PaneModel>>("PaneDownList", model => model.Initialize("PaneDownList", ref model._PaneDownList, ref _PaneDownListLocator, _PaneDownListDefaultValueFactory));
        static Func<List<PaneModel>> _PaneDownListDefaultValueFactory = () => { return new List<PaneModel>(); };
        #endregion

        //SupportCities
        public List<CityModel> SupportCities
        {
            get { return _SupportCitiesLocator(this).Value; }
            set { _SupportCitiesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel SupportCities Setup        
        protected Property<List<CityModel>> _SupportCities = new Property<List<CityModel>> { LocatorFunc = _SupportCitiesLocator };
        static Func<BindableBase, ValueContainer<List<CityModel>>> _SupportCitiesLocator = RegisterContainerLocator<List<CityModel>>("SupportCities", model => model.Initialize("SupportCities", ref model._SupportCities, ref _SupportCitiesLocator, _SupportCitiesDefaultValueFactory));
        static Func<List<CityModel>> _SupportCitiesDefaultValueFactory = () => { return new List<CityModel>(); };
        #endregion

        //MallList
        public List<MallModel> MallList
        {
            get { return _MallListLocator(this).Value; }
            set { _MallListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel MallList Setup        
        protected Property<List<MallModel>> _MallList = new Property<List<MallModel>> { LocatorFunc = _MallListLocator };
        static Func<BindableBase, ValueContainer<List<MallModel>>> _MallListLocator = RegisterContainerLocator<List<MallModel>>("MallList", model => model.Initialize("MallList", ref model._MallList, ref _MallListLocator, _MallListDefaultValueFactory));
        static Func<List<MallModel>> _MallListDefaultValueFactory = () => { return new List<MallModel>(); };
        #endregion

        //SelectedCityIndex
        public int SelectedCityIndex
        {
            get { return _SelectedCityIndexLocator(this).Value; }
            set { _SelectedCityIndexLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel SelectedCityIndex Setup        
        protected Property<int> _SelectedCityIndex = new Property<int> { LocatorFunc = _SelectedCityIndexLocator };
        static Func<BindableBase, ValueContainer<int>> _SelectedCityIndexLocator = RegisterContainerLocator<int>("SelectedCityIndex", model => model.Initialize("SelectedCityIndex", ref model._SelectedCityIndex, ref _SelectedCityIndexLocator, _SelectedCityIndexDefaultValueFactory));
        static Func<int> _SelectedCityIndexDefaultValueFactory = () => { return -1; };
        #endregion

        //SelectedCity
        public CityModel SelectedCity
        {
            get { return _SelectedCityLocator(this).Value; }
            set { _SelectedCityLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel SelectedCity Setup        
        protected Property<CityModel> _SelectedCity = new Property<CityModel> { LocatorFunc = _SelectedCityLocator };
        static Func<BindableBase, ValueContainer<CityModel>> _SelectedCityLocator = RegisterContainerLocator<CityModel>("SelectedCity", model => model.Initialize("SelectedCity", ref model._SelectedCity, ref _SelectedCityLocator, _SelectedCityDefaultValueFactory));
        static Func<CityModel> _SelectedCityDefaultValueFactory = () => { return new CityModel(); };
        #endregion

        //SelectedMallIndex
        public int SelectedMallIndex
        {
            get { return _SelectedMallIndexLocator(this).Value; }
            set { _SelectedMallIndexLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel SelectedMallIndex Setup        
        protected Property<int> _SelectedMallIndex = new Property<int> { LocatorFunc = _SelectedMallIndexLocator };
        static Func<BindableBase, ValueContainer<int>> _SelectedMallIndexLocator = RegisterContainerLocator<int>("SelectedMallIndex", model => model.Initialize("SelectedMallIndex", ref model._SelectedMallIndex, ref _SelectedMallIndexLocator, _SelectedMallIndexDefaultValueFactory));
        static Func<int> _SelectedMallIndexDefaultValueFactory = () => { return -1; };
        #endregion

        #endregion

        #region -------------------   Commands   -------------------

        #region CommandCityChanged
        public CommandModel<ReactiveCommand, String> CommandCityChanged
        {
            get { return _CommandCityChangedLocator(this).Value; }
            set { _CommandCityChangedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandCityChanged Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandCityChanged = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandCityChangedLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandCityChangedLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandCityChanged", model => model.Initialize("CommandCityChanged", ref model._CommandCityChanged, ref _CommandCityChangedLocator, _CommandCityChangedDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandCityChangedDefaultValueFactory =
            model =>
            {
                var resource = "CityChanged";           // Command resource  
                var commandId = "CityChanged";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();

                            var city = vm.SelectedCity;

                            AppSettings.Intance.SelectedCityId = city.id;

                            vm.GetSupportMallList();
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        #endregion
        
        #region CommandHamburgClick

        //CommandHamburgClick
        public CommandModel<ReactiveCommand, String> CommandHamburgClick
        {
            get { return _CommandHamburgClickLocator(this).Value; }
            set { _CommandHamburgClickLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandHamburgClick Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandHamburgClick = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandHamburgClickLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandHamburgClickLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandHamburgClick", model => model.Initialize("CommandHamburgClick", ref model._CommandHamburgClick, ref _CommandHamburgClickLocator, _CommandHamburgClickDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandHamburgClickDefaultValueFactory =
            model =>
            {
                var resource = "HamburgClick";           // Command resource  
                var commandId = "HamburgClick";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();

                            //Todo: Add NavigateToAbout logic here, or
                            vm.IsHumburgShow = !vm.IsHumburgShow;
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        #endregion

        #region -------------   AutoSuggestionCommand   -------------
         
        //CommandAutoSuggestionTextChange   
        public CommandModel<ReactiveCommand, String> CommandAutoSuggestionTextChange
        {
            get { return _CommandAutoSuggestionTextChangeLocator(this).Value; }
            set { _CommandAutoSuggestionTextChangeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandAutoSuggestionTextChange Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandAutoSuggestionTextChange = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandAutoSuggestionTextChangeLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandAutoSuggestionTextChangeLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandAutoSuggestionTextChange", model => model.Initialize("CommandAutoSuggestionTextChange", ref model._CommandAutoSuggestionTextChange, ref _CommandAutoSuggestionTextChangeLocator, _CommandAutoSuggestionTextChangeDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandAutoSuggestionTextChangeDefaultValueFactory =
            model =>
            {
                var resource = "AutoSuggestionTextChange";           // Command resource  
                var commandId = "AutoSuggestionTextChange";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {   
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            var args = e.EventArgs.Parameter as AutoSuggestBoxTextChangedEventArgs;
                            if(args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                            {
                                //搜索
                                vm.SearchMallFromInputKey();
                            } 
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion
        
        //CommandAutoSuggestionSubmitted
        public CommandModel<ReactiveCommand, String> CommandAutoSuggestionSubmitted
        {
            get { return _CommandAutoSuggestionSubmittedLocator(this).Value; }
            set { _CommandAutoSuggestionSubmittedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandAutoSuggestionSubmitted Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandAutoSuggestionSubmitted = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandAutoSuggestionSubmittedLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandAutoSuggestionSubmittedLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandAutoSuggestionSubmitted", model => model.Initialize("CommandAutoSuggestionSubmitted", ref model._CommandAutoSuggestionSubmitted, ref _CommandAutoSuggestionSubmittedLocator, _CommandAutoSuggestionSubmittedDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandAutoSuggestionSubmittedDefaultValueFactory =
            model =>
            {
                var resource = "AutoSuggestionSubmitted";           // Command resource  
                var commandId = "AutoSuggestionSubmitted";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            var args = e.EventArgs.Parameter as AutoSuggestBoxQuerySubmittedEventArgs;
                            if (args.ChosenSuggestion != null)
                            {
                                var mall = (args.ChosenSuggestion as MallModel);
                                if (mall.name == NoResultString) return;

                                vm.AutoSuggestText = mall.name;
                                if (vm.SelectedPaneDownItem.type == PanelItemType.PanelItemMallList)
                                {
                                    MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(vm, mall, typeof(MallModel), "NavigateToDetailByEventRouter", true);
                                    MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(vm, mall, typeof(MallModel), "MarkSearchedMall", true);
                                }
                                else if (vm.SelectedPaneDownItem.type == PanelItemType.PanelItemMap)
                                {

                                }
                            }
                            //else
                            //{
                            //    vm.SearchMallFromInputKey();
                                  //自己处理  
                            //}
                        }) 
                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        #endregion



        #region CommandGridLoaded

        public CommandModel<ReactiveCommand, String> CommandGridLoaded
        {
            get { return _CommandGridLoadedLocator(this).Value; }
            set { _CommandGridLoadedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGridLoaded Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGridLoaded = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGridLoadedLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGridLoadedLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGridLoaded", model => model.Initialize("CommandGridLoaded", ref model._CommandGridLoaded, ref _CommandGridLoadedLocator, _CommandGridLoadedDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGridLoadedDefaultValueFactory =
            model =>
            {
                var resource = "GridLoaded";           // Command resource  
                var commandId = "GridLoaded";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        #endregion


        #region CommandGoToSettingPage

        public CommandModel<ReactiveCommand, String> CommandGoToSettingPage
        {
            get { return _CommandGoToSettingPageLocator(this).Value; }
            set { _CommandGoToSettingPageLocator(this).SetValueAndTryNotify(value); }
        } 
        #region Property CommandModel<ReactiveCommand, String> CommandGoToSettingPage Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGoToSettingPage = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGoToSettingPageLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGoToSettingPageLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGoToSettingPage", model => model.Initialize("CommandGoToSettingPage", ref model._CommandGoToSettingPage, ref _CommandGoToSettingPageLocator, _CommandGoToSettingPageDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGoToSettingPageDefaultValueFactory =
            model =>
            {
                var resource = "GoToSettingPage";           // Command resource  
                var commandId = "GoToSettingPage";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            if (CommonHelper.HostPage.ActualWidth <= 500)
                            {
                                vm.MainVisibility = Visibility.Collapsed;
                            }
                            else
                            {
                                vm.MainVisibility = Visibility.Visible;
                            }
                            await vm.StageManager["frameSub"].Show(new SettingPage_Model());
                           
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        #endregion

        //CommandGoToRefreshPage
        #region CommandGoToRefreshPage

        public CommandModel<ReactiveCommand, String> CommandGoToRefreshPage
        {
            get { return _CommandGoToRefreshPageLocator(this).Value; }
            set { _CommandGoToRefreshPageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGoToRefreshPage Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGoToRefreshPage = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGoToRefreshPageLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGoToRefreshPageLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGoToRefreshPage", model => model.Initialize("CommandGoToRefreshPage", ref model._CommandGoToRefreshPage, ref _CommandGoToRefreshPageLocator, _CommandGoToRefreshPageDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGoToRefreshPageDefaultValueFactory =
            model =>
            {
                var resource = "GoToRefreshPage";           // Command resource  
                var commandId = "GoToRefreshPage";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();

                            vm.GetSupportMallListAction();
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        #endregion

        


        #region CommandPaneItemChanged

        //CommandPaneItemChanged
        public CommandModel<ReactiveCommand, String> CommandPaneItemChanged
        {
            get { return _CommandPaneItemChangedLocator(this).Value; }
            set { _CommandPaneItemChangedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandPaneItemChanged Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandPaneItemChanged = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandPaneItemChangedLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandPaneItemChangedLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandPaneItemChanged", model => model.Initialize("CommandPaneItemChanged", ref model._CommandPaneItemChanged, ref _CommandPaneItemChangedLocator, _CommandPaneItemChangedDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandPaneItemChangedDefaultValueFactory =
            model =>
            {
                var resource = "PaneItemChanged";           // Command resource  
                var commandId = "PaneItemChanged";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            var navItem = e.EventArgs.Parameter as PaneModel;
                            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(vm, navItem, typeof(PaneModel), "SplitViewPaneItemClick", true);
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        #endregion

        //CommandShowAllMallInMap
        //public CommandModel<ReactiveCommand, String> CommandShowAllMallInMap
        //{
        //    get { return _CommandShowAllMallInMapLocator(this).Value; }
        //    set { _CommandShowAllMallInMapLocator(this).SetValueAndTryNotify(value); }
        //}
        //#region Property CommandModel<ReactiveCommand, String> CommandShowAllMallInMap Setup        
        //protected Property<CommandModel<ReactiveCommand, String>> _CommandShowAllMallInMap = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowAllMallInMapLocator };
        //static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowAllMallInMapLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowAllMallInMap", model => model.Initialize("CommandShowAllMallInMap", ref model._CommandShowAllMallInMap, ref _CommandShowAllMallInMapLocator, _CommandShowAllMallInMapDefaultValueFactory));
        //static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowAllMallInMapDefaultValueFactory =
        //    model =>
        //    {
        //        var resource = "ShowAllMallInMap";           // Command resource  
        //        var commandId = "ShowAllMallInMap";
        //        var vm = CastToCurrentType(model);
        //        var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
        //        cmd.DoExecuteUIBusyTask(
        //                vm,
        //                async e =>
        //                {
        //                    //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
        //                    //Todo: Add NavigateToAbout logic here, or
        //                    await MVVMSidekick.Utilities.TaskExHelper.Yield();
        //                    //var mall = vm.MallList[0];
        //                    //maps.Center = new Geopoint(new BasicGeoposition()
        //                    //{
        //                    //    Latitude = Double.Parse(mall.lat),
        //                    //    Longitude = Double.Parse(mall.lon)
        //                    //});
        //                    //maps.ZoomLevel = 50;

        //                    //maps.MapElementClick
        //                    vm.AddAllMallsInMap();                            
        //                }
        //            )

        //            .DoNotifyDefaultEventRouter(vm, commandId)
        //            .Subscribe()
        //            .DisposeWith(vm);

        //        var cmdmdl = cmd.CreateCommandModel(resource);
        //        cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
        //        return cmdmdl;
        //    };
        //#endregion



        internal void SetFrameSplitContent(Frame frame)
        {
            frameSplitContent = frame;
        }

        private void SubNavigateTo(Type type)
        {
            FrameSplitContent.Navigate(type);
        }

        #endregion

        #region -------------------   Methods   -------------------

        private async void GetSupportCities()
        {
            string url = @"http://op.juhe.cn/atlasyun/city/list?key=" + Configmanager.INDOORMAP_APPKEY;
            FormAction action = new FormAction(url);
            action.viewModel = this;
            action.Run();
            action.FormActionCompleted += (result, ee) =>
            {
                JsonCityModel jsonCity = JsonConvert.DeserializeObject<JsonCityModel>(result);
                if (jsonCity.reason == "成功" || jsonCity.reason == "successed")
                { 
                    HttpClientReturnCities(jsonCity.result);
                }
            };
            await StageManager["frameMain"].Show(SubMallListPageModel); 
        }

        private async void GetSupportMallList()
        {
            var mallListJson = await StorageHelper.GetLocalTextFile(DataManager.CityMallListFile);
            if (mallListJson != null)
            {
                DataManager.SavedMallList = JsonConvert.DeserializeObject<List<SavedMallListModel>>(mallListJson);
                var selectedCitMallList = DataManager.SavedMallList.FirstOrDefault(n => n.City.id == SelectedCity.id);
                if (selectedCitMallList != null)
                {
                    this.MallList = selectedCitMallList.MallList;
                    MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(this, this.MallList, typeof(List<MallModel>), "CitySelectedChangedEvent", true);
                }
                else
                    GetSupportMallListAction();
            }
            else
                GetSupportMallListAction(); 
        }

        public void GetSupportMallListAction()
        {
            string url = string.Format(@"http://op.juhe.cn/atlasyun/mall/list?key={0}&cityid={1}", Configmanager.INDOORMAP_APPKEY, SelectedCity.id);
            FormAction action = new FormAction(url);
            action.isShowWaitingPanel = true;
            action.viewModel = this;
            action.Run();
            action.FormActionCompleted += async (result, ee) =>
            {
                JsonMallModel jsonMall = JsonConvert.DeserializeObject<JsonMallModel>(result);
                                    
                if (jsonMall.reason == "成功" || jsonMall.reason == "successed")
                { 
                    //此处 解析时间非常慢慢慢慢
                    foreach (var mall in jsonMall.result)
                    {
                        mall.district = await LocationManager.GetDistrictUsingLocation(new Geopoint(new BasicGeoposition()
                        {
                            Latitude = Double.Parse(mall.lat),
                            Longitude = Double.Parse(mall.lon)
                        }));
                    }                    

                    var item = new SavedMallListModel() { City = SelectedCity, MallList = jsonMall.result };
                    if (!DataManager.SavedMallList.Contains(item))
                    {
                        DataManager.SavedMallList.Add(item);
                        string save = JsonConvert.SerializeObject(DataManager.SavedMallList);
                        await StorageHelper.SetLocalTextFile(DataManager.CityMallListFile, save);
                    }
                    HttpClientReturnMallList(jsonMall.result);
                }
            };
        }

        public async void HttpClientReturnCities(List<CityModel> jsonCity)
        {

            this.SupportCities = jsonCity;
            if (AppSettings.Intance.LocationSetting)
            {
                string city = AppSettings.Intance.LocationCity;
                var saveCity = SupportCities.FirstOrDefault(n => n.name == city);
                if (saveCity == null)
                {
                    await new MessageDialog("暂不支持" + city + "，为您切换到默认城市").ShowAsync();
                    SelectedCityIndex = 0;
                }
                else
                    SelectedCityIndex = SupportCities.IndexOf(saveCity);
            }
            else
                SelectedCityIndex = 0;
        }

        public void HttpClientReturnMallList(List<MallModel> jsonMall)
        { 
            this.MallList = jsonMall;
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(this, this.MallList, typeof(List<MallModel>), "CitySelectedChangedEvent", true);
        }

        public void InitPaneListData()
        {
            PaneDownList.Add(new PaneModel() { Label = "列表", Icon = "\xEA37", type = PanelItemType.PanelItemMallList }); 
            PaneDownList.Add(new PaneModel() { Label = "地图", Icon = "\xE774", type = PanelItemType.PanelItemMap }); 

            SelectedPaneDownItem = PaneDownList.FirstOrDefault(); 
        }

        //Subscribe
        private void SuscribeCommand()
        {
            //MallList Item Tapped
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
                .Where(x => x.EventName == "NavigateToDetailByEventRouter")
                .Subscribe(
                async e =>
                {
                    var item = e.EventData as MallModel;
                    if (item != null)
                    {
                        if (CommonHelper.HostPage.ActualWidth <= 500)
                        {
                            MainVisibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MainVisibility = Visibility.Visible;
                        }
                        //await StageManager.DefaultStage.Show(new AtlasPage_Model(item.buildings.FirstOrDefault()));
                        await StageManager["frameSub"].Show(new AtlasPage_Model(item.buildings.FirstOrDefault()));
                    }
                }
                ).DisposeWith(this);
            //MallList Button Tapped
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
                .Where(x => x.EventName == "ListButtonClickByEventRouter")
                .Subscribe(
                e =>
                {
                    var item = e.EventData as MallModel;
                    if (item != null)
                    {
                        Debug.WriteLine(SelectedPaneDownIndex + ":" + SelectedPaneDownItem.Label);
                        
                        SelectedPaneDownIndex = 1;
                        SelectedPaneDownItem = PaneDownList[1];
                        
                    }
                }
                ).DisposeWith(this);

            //SplitViewPaneItemClick 
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>().
            Where(x => x.EventName == "SplitViewPaneItemClick")
                .Subscribe(
                async e =>
                {
                    var navItem = e.EventData as PaneModel;
                    if (navItem != null)
                    {
                        switch (navItem.type)
                        {
                            case PanelItemType.PanelItemMallList:
                                await StageManager["frameMain"].Show(SubMallListPageModel);
                                break;
                            case PanelItemType.PanelItemMap:
                                await StageManager["frameMain"].Show(SubMapPageModel);
                                break;
                        }
                    }
                }
                ).DisposeWith(this);
        }

        private async void SetApplicationCityLocation()
        {
            //ShowLocationMessageDialog
            if (AppSettings.Intance.IsFirstRun)    //true
            {
                //Popup just for one time
                bool result = await CommonHelper.ShowMessageDialog("是否打开定位");
                if (result)
                {
                    AppSettings.Intance.LocationSetting = true;
                    string location = await LocationManager.GetCityUsingMapLocation();
                    if (!string.IsNullOrEmpty(location))
                        AppSettings.Intance.LocationCity = location;
                }
                else
                    AppSettings.Intance.LocationSetting = false;

                AppSettings.Intance.IsFirstRun = false;
            }

            //if the locate setting is on but the city is empty or null
            if (AppSettings.Intance.LocationSetting && !string.IsNullOrEmpty(AppSettings.Intance.LocationCity))
            {
                string location = await LocationManager.GetCityUsingMapLocation();
                if (!string.IsNullOrEmpty(location))
                    AppSettings.Intance.LocationCity = location;
            } 

            GetSupportCities();
        }

        private void SearchMallFromInputKey()
        {
            if (string.IsNullOrWhiteSpace(AutoSuggestText))
                return;
            AutoSuggests.Clear();

            var result = MallList.Where(n =>
                                n.name.ToLower().Contains(AutoSuggestText.ToLower().Trim())
                                ).ToList();
            if (!result.Any())
            {
                result = new List<MallModel>() { new MallModel() { name = NoResultString } };
            }
            AutoSuggests = result;
        }

        #endregion

        #region Life Time Event Handling

        ///// <summary>
        ///// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        ///// </summary>
        ///// <param name="view">Set target</param>
        ///// <param name="oldValue">Value before set.</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        //{ 
        //    return base.OnBindedToView(view, oldValue);
        //}

        ///// <summary>
        ///// This will be invoked by view when this instance of viewmodel in ViewModel property is overwritten.
        ///// </summary>
        ///// <param name="view">Overwrite target view.</param>
        ///// <param name="newValue">The value replacing </param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnUnbindedFromView(MVVMSidekick.Views.IView view, IViewModel newValue)
        //{
        //    return base.OnUnbindedFromView(view, newValue);
        //}

        /// <summary>
        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        /// </summary>
        /// <param name="view">View that firing Load event</param>
        /// <returns>Task awaiter</returns>
        protected async override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            await base.OnBindedViewLoad(view);

            if (!isLoadSuscribe)
            {
                SuscribeCommand();
                isLoadSuscribe = true;
                //设置定位相关信息   但是未包括定位更改事件
                 
                SetApplicationCityLocation();
                await StageManager["frameSub"].Show(new AtlasBlankPage_Model());

            }
        }

        ///// <summary>
        ///// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Unload event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewUnload(view);
        //}

        ///// <summary>
        ///// <para>If dispose actions got exceptions, will handled here. </para>
        ///// </summary>
        ///// <param name="exceptions">
        ///// <para>The exception and dispose infomation</para>
        ///// </param>
        //protected override async void OnDisposeExceptions(IList<DisposeInfo> exceptions)
        //{
        //    base.OnDisposeExceptions(exceptions);
        //    await TaskExHelper.Yield();
        //}

        #endregion

    }

}

