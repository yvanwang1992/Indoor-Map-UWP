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
         

        public MainPage_Model()
        {
            if (IsInDesignMode)
            {

            }
            subMapPageModel = new SubMapPage_Model();

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

                            vm.GetSupportMallListAction();

                            //MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(vm, city, typeof(CityModel), "CitySelectedChangedEvent", true);


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
                                vm.AutoSuggests.Clear();
                                vm.AutoSuggests = vm.MallList.Where(n =>
                                n.name.ToLower().Contains(vm.AutoSuggestText.ToLower().Trim())
                                ).ToList();
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


        #region CommandGoToSettingPage
        //Navigate To Setting Page
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

                            await vm.StageManager.DefaultStage.Show(new SettingPage_Model());

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

        private void GetSupportCities()
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
        }

        private void GetSupportMallListAction()
        {
            string url = string.Format(@"http://op.juhe.cn/atlasyun/mall/list?key={0}&cityid={1}", Configmanager.INDOORMAP_APPKEY, SelectedCity.id);
            FormAction action = new FormAction(url);
            action.isShowWaitingPanel = true;
            action.viewModel = this;
            action.Run();
            action.FormActionCompleted += (result, ee) =>
            {
                JsonMallModel jsonMall = JsonConvert.DeserializeObject<JsonMallModel>(result);
                if (jsonMall.reason == "成功" || jsonMall.reason == "successed")
                {
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
            PaneDownList.Add(new PaneModel() { Label = "列表", Icon = "", type = PanelItemType.PanelItemMallList }); 
            PaneDownList.Add(new PaneModel() { Label = "地图", Icon = "", type = PanelItemType.PanelItemMap }); 

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
                        await StageManager.DefaultStage.Show(new AtlasPage_Model(item.buildings.FirstOrDefault()));
                    }
                    //await StageManager.DefaultStage.Show(AtlasPageModel);

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

            if (!DataManager.isLoadSuscribe)
            {
                SuscribeCommand();
                DataManager.isLoadSuscribe = true;
                //设置定位相关信息   但是未包括定位更改事件
                
                SetApplicationCityLocation();

                subMallListPageModel = new SubMallListPage_Model();
                await StageManager["frameMain"].Show(SubMallListPageModel);
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

