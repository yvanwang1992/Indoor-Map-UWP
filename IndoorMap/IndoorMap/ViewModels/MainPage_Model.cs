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
using Windows.UI.Popups;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class MainPage_Model : ViewModelBase<MainPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令
         
        public MainPage_Model()
        {
            if (IsInDesignMode )
            {
                Title = "Title is a little different in Design mode";
            }
        }

        #region --------------------   Properties   -------------------

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
                var resource = "GoToSettingPage";           // Command resource  
                var commandId = "GoToSettingPage";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                             //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            if(vm.SelectedCityIndex >= 0)
                            {
                                var city = vm.SupportCities[vm.SelectedCityIndex];
                                AppSettings.Intance.SelectedCityId = city.id;
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

        #endregion
         
        //Request City List
        public CommandModel<ReactiveCommand, String> CommandGetSupportCities
        {
            get { return _CommandGetSupportCitiesLocator(this).Value; }
            set { _CommandGetSupportCitiesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGetSupportCities Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGetSupportCities = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGetSupportCitiesLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGetSupportCitiesLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGetSupportCities", model => model.Initialize("CommandGetSupportCities", ref model._CommandGetSupportCities, ref _CommandGetSupportCitiesLocator, _CommandGetSupportCitiesDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGetSupportCitiesDefaultValueFactory =
            model =>
            {
                var resource = "GetSupportCities";           // Command resource  
                var commandId = "GetSupportCities";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //var location = await LocationManager.GetPosition();
                             vm.GetSupportCitesAction();
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
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

        //Request Mall List
        public CommandModel<ReactiveCommand, String> CommandGetMallList
        {
            get { return _CommandGetMallListLocator(this).Value; }
            set { _CommandGetMallListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGetMallList Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGetMallList = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGetMallListLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGetMallListLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGetMallList", model => model.Initialize("CommandGetMallList", ref model._CommandGetMallList, ref _CommandGetMallListLocator, _CommandGetMallListDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGetMallListDefaultValueFactory =
            model =>
            {
                var resource = "GetSupportCities";           // Command resource  
                var commandId = "GetSupportCities";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //var location = await LocationManager.GetPosition();
                            vm.GetSupportMallListAction();
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
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
         
        //CommandLvMallItemClick
        public CommandModel<ReactiveCommand, String> CommandLvMallItemClick
        {
            get { return _CommandLvMallItemClickLocator(this).Value; }
            set { _CommandLvMallItemClickLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLvMallItemClick Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLvMallItemClick = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLvMallItemClickLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLvMallItemClickLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLvMallItemClick", model => model.Initialize("CommandLvMallItemClick", ref model._CommandLvMallItemClick, ref _CommandLvMallItemClickLocator, _CommandLvMallItemClickDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLvMallItemClickDefaultValueFactory =
            model =>
            {
                var resource = "LvMallItemClick";           // Command resource  
                var commandId = "LvMallItemClick";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        { 
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();

                            var mall = vm.MallList[vm.SelectedMallIndex];

                            await new MessageDialog(mall.lat + " " + mall.lon).ShowAsync();
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

        #region -------------------   Methods   -------------------

        private void GetSupportCitesAction()
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
            string url = string.Format(@"http://op.juhe.cn/atlasyun/mall/list?key={0}&cityid={1}", Configmanager.INDOORMAP_APPKEY, SupportCities[SelectedCityIndex].id);
            FormAction action = new FormAction(url);
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

            string city = AppSettings.Intance.LocationCity;
            var saveCity = SupportCities.FirstOrDefault(n => n.name == city);
            if (saveCity == null)
            {
                await new MessageDialog("暂不支持" + city + "，为您切换到默认城市").ShowAsync();
                SelectedCityIndex = 0;
                return;
            }
            SelectedCityIndex = SupportCities.IndexOf(saveCity);


            SelectedCityIndex = 0;
        }

        public void HttpClientReturnMallList(List<MallModel> jsonMall)
        {
            this.MallList = jsonMall;
            
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

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewLoad(view);
        //}

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

