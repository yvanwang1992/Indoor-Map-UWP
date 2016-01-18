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

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class MainPage_Model : ViewModelBase<MainPage_Model> 
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

    public MainPage_Model()
        {
            if (IsInDesignMode)
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

        //All the Map Element
        public ObservableCollection<MapElement> MapElements
        {
            get { return _MapElementsLocator(this).Value; }
            set { _MapElementsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel MapElements Setup        
        protected Property<ObservableCollection<MapElement>> _MapElements = new Property<ObservableCollection<MapElement>> { LocatorFunc = _MapElementsLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<MapElement>>> _MapElementsLocator = RegisterContainerLocator<ObservableCollection<MapElement>>("MapElements", model => model.Initialize("MapElements", ref model._MapElements, ref _MapElementsLocator, _MapElementsDefaultValueFactory));
        static Func<ObservableCollection<MapElement>> _MapElementsDefaultValueFactory = () => { return new ObservableCollection<MapElement>(); };
        #endregion

        //MapZoomLevel
        public Double MapZoomLevel
        {
            get { return _MapZoomLevelLocator(this).Value; }
            set { _MapZoomLevelLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel MapZoomLevel Setup        
        protected Property<Double> _MapZoomLevel = new Property<Double> { LocatorFunc = _MapZoomLevelLocator };
        static Func<BindableBase, ValueContainer<Double>> _MapZoomLevelLocator = RegisterContainerLocator<Double>("MapZoomLevel", model => model.Initialize("MapZoomLevel", ref model._MapZoomLevel, ref _MapZoomLevelLocator, _MapZoomLevelDefaultValueFactory));
        static Func<Double> _MapZoomLevelDefaultValueFactory = () => { return 0; };
        #endregion

        //MapCenter GeopoGeopoint
        public Geopoint MapCenter
        {
            get { return _MapCenterLocator(this).Value; }
            set { _MapCenterLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel MapCenter Setup        
        protected Property<Geopoint> _MapCenter = new Property<Geopoint> { LocatorFunc = _MapCenterLocator };
        static Func<BindableBase, ValueContainer<Geopoint>> _MapCenterLocator = RegisterContainerLocator<Geopoint>("MapCenter", model => model.Initialize("MapCenter", ref model._MapCenter, ref _MapCenterLocator, _MapCenterDefaultValueFactory));
        static Func<Geopoint> _MapCenterDefaultValueFactory = () => { return new Geopoint(new BasicGeoposition() { }); };
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
                            if(vm.SelectedCityIndex >= 0)
                            {
                                var city = vm.SupportCities[vm.SelectedCityIndex];
                               
                                AppSettings.Intance.SelectedCityId = city.id;

                                vm.GetSupportMallListAction();

                                //之后在地图上画出各个商铺
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
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();

                            //var location = await LocationManager.GetPosition();
                             vm.GetSupportCitesAction();
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

                            await vm.StageManager.DefaultStage.Show(new AtlasPage_Model(mall.buildings.FirstOrDefault()));

                            //await new MessageDialog(mall.lat + " " + mall.lon).ShowAsync();
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

        //CommandShowAllMallInMap
        public CommandModel<ReactiveCommand, String> CommandShowAllMallInMap
        {
            get { return _CommandShowAllMallInMapLocator(this).Value; }
            set { _CommandShowAllMallInMapLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowAllMallInMap Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowAllMallInMap = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowAllMallInMapLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowAllMallInMapLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowAllMallInMap", model => model.Initialize("CommandShowAllMallInMap", ref model._CommandShowAllMallInMap, ref _CommandShowAllMallInMapLocator, _CommandShowAllMallInMapDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowAllMallInMapDefaultValueFactory =
            model =>
            {
                var resource = "ShowAllMallInMap";           // Command resource  
                var commandId = "ShowAllMallInMap";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            //var mall = vm.MallList[0];
                            //maps.Center = new Geopoint(new BasicGeoposition()
                            //{
                            //    Latitude = Double.Parse(mall.lat),
                            //    Longitude = Double.Parse(mall.lon)
                            //});
                            //maps.ZoomLevel = 50;

                            //maps.MapElementClick
                            vm.AddAllMallsInMap();                            
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
         
        //CommandShowOneMallInMap
        public CommandModel<ReactiveCommand, String> CommandShowOneMallInMap
        {
            get { return _CommandShowOneMallInMapLocator(this).Value; }
            set { _CommandShowOneMallInMapLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowOneMallInMap Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowOneMallInMap = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowOneMallInMapLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowOneMallInMapLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowOneMallInMap", model => model.Initialize("CommandShowOneMallInMap", ref model._CommandShowOneMallInMap, ref _CommandShowOneMallInMapLocator, _CommandShowOneMallInMapDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowOneMallInMapDefaultValueFactory =
            model =>
            {
                var resource = "ShowAllMallInMap";           // Command resource  
                var commandId = "ShowAllMallInMap";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
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

        //CommandMapItemClick
        public CommandModel<ReactiveCommand, String> CommandMapItemClick
        {
            get { return _CommandMapItemClickLocator(this).Value; }
            set { _CommandMapItemClickLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandMapItemClick Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandMapItemClick = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandMapItemClickLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandMapItemClickLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandMapItemClick", model => model.Initialize("CommandMapItemClick", ref model._CommandMapItemClick, ref _CommandMapItemClickLocator, _CommandMapItemClickDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandMapItemClickDefaultValueFactory =
            model =>
            {
                var resource = "ShowAllMallInMap";           // Command resource  
                var commandId = "ShowAllMallInMap";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            var param = e.EventArgs.Parameter as MapElementClickEventArgs;
                            foreach (var item in param.MapElements)
                            {
                                if (item is MapPolygon)
                                {
                                    var poly = item as MapPolygon;
                                    //Is MapPolygon
                                }
                                else if (item is MapPolyline)
                                {
                                    var poly = item as MapPolyline;
                                    //Is MapPolyline
                                }
                                else if (item is MapIcon)
                                {
                                    var icon = item as MapIcon;
                                    //Is MapIcon
                                    //
                                    //MallModel mall = vm.MallList.FirstOrDefault(n => n.name == icon.Title);
                                    //await vm.StageManager.DefaultStage.Show(new AtlasPage_Model(mall.buildings[0]));

                                    vm.MapCenter = icon.Location;

                                }
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

        //CommandGeoLocation
        public CommandModel<ReactiveCommand, String> CommandGeoLocation
        {
            get { return _CommandGeoLocationLocator(this).Value; }
            set { _CommandGeoLocationLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGeoLocation Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGeoLocation = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGeoLocationLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGeoLocationLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGeoLocation", model => model.Initialize("CommandGeoLocation", ref model._CommandGeoLocation, ref _CommandGeoLocationLocator, _CommandGeoLocationDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGeoLocationDefaultValueFactory =
            model =>
            {
                var resource = "ShowAllMallInMap";           // Command resource  
                var commandId = "ShowAllMallInMap";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {

                            //    await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            var position = await LocationManager.GetPosition();
                            if (position != null)
                            {
                                AppSettings.Intance.LocationSetting = true;
                                //GetCity(position.Coordinate);
                                vm.MapCenter = position.Coordinate.Point;
                                vm.MapZoomLevel = 15;
                            }
                            else
                            {
                                AppSettings.Intance.LocationSetting = false;
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

        public void AddAllMallsInMap()
        {
            var elements = new ObservableCollection<MapElement>();
            
            foreach (var item in MallList)
            {
                MapIcon mapIcon = new MapIcon();
                var bdPosition = new Geopoint(new BasicGeoposition()
                {
                    Latitude = Double.Parse(item.lat),
                    Longitude = Double.Parse(item.lon)
                });
                if(MallList.IndexOf(item) == 0)
                {
                    MapCenter = bdPosition;
                }
                mapIcon.Location = bdPosition;//LocationManager.TransformFromWorldlToMars(bdPosition);
                mapIcon.Title = item.name;
                mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/delete_auto.png"));
                elements.Add(mapIcon);
            }
            MapElements = elements;
            MapZoomLevel = 10;
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

