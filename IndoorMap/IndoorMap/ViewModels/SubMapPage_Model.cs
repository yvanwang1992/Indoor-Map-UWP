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
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using IndoorMap.Helpers;
using Windows.Storage.Streams;
using IndoorMap.Models;
using IndoorMap.UserControls;
using Windows.UI.Xaml;
using Windows.Foundation;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class SubMapPage_Model : ViewModelBase<SubMapPage_Model>
    {
        static RandomAccessStreamReference TappedIcon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/StoreLogo.png"));
        static RandomAccessStreamReference UnTappedIcon = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/delete_auto.png"));

        static Geopoint defaultGeopoint = new Geopoint(new BasicGeoposition() { Latitude = 28.23, Longitude = 117.02 });
        static Double defaultZoomLevel = 4;

         
        bool isLoadSuscribe = false;
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        public SubMapPage_Model()
        {
            if (!isLoadSuscribe)
            {
                SuscribeCommand();
                isLoadSuscribe = true;
                MapCenter = defaultGeopoint;
                MapZoomLevel = defaultZoomLevel;
            }
           
        }

        public String Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String Title Setup
        protected Property<String> _Title = new Property<String> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<String>> _TitleLocator = RegisterContainerLocator<String>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<BindableBase, String> _TitleDefaultValueFactory = m => m.GetType().Name;
        #endregion


        //SelectedMallItem
        public MallModel SelectedMallItem
        {
            get { return _SelectedMallItemLocator(this).Value; }
            set { _SelectedMallItemLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel SelectedMallItem Setup        
        protected Property<MallModel> _SelectedMallItem = new Property<MallModel> { LocatorFunc = _SelectedMallItemLocator };
        static Func<BindableBase, ValueContainer<MallModel>> _SelectedMallItemLocator = RegisterContainerLocator<MallModel>("SelectedMallItem", model => model.Initialize("SelectedMallItem", ref model._SelectedMallItem, ref _SelectedMallItemLocator, _SelectedMallItemDefaultValueFactory));
        static Func<MallModel> _SelectedMallItemDefaultValueFactory = () => { return new MallModel(); };
        #endregion

        //SelectedItemVisibility
        public Visibility SelectedItemVisibility
        {
            get { return _SelectedItemVisibilityLocator(this).Value; }
            set { _SelectedItemVisibilityLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel SelectedItemVisibility Setup        
        protected Property<Visibility> _SelectedItemVisibility = new Property<Visibility> { LocatorFunc = _SelectedItemVisibilityLocator };
        static Func<BindableBase, ValueContainer<Visibility>> _SelectedItemVisibilityLocator = RegisterContainerLocator<Visibility>("SelectedItemVisibility", model => model.Initialize("SelectedItemVisibility", ref model._SelectedItemVisibility, ref _SelectedItemVisibilityLocator, _SelectedItemVisibilityDefaultValueFactory));
        static Func<Visibility> _SelectedItemVisibilityDefaultValueFactory = () => { return new Visibility(); };
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

        //SelectedMapElement
        public ObservableCollection<MapElement> SelectedMapElement
        {
            get { return _SelectedMapElementLocator(this).Value; }
            set { _SelectedMapElementLocator(this).SetValueAndTryNotify(value); }
        }

        #region Property Channel SelectedMapElement Setup        
        protected Property<ObservableCollection<MapElement>> _SelectedMapElement = new Property<ObservableCollection<MapElement>> { LocatorFunc = _SelectedMapElementLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<MapElement>>> _SelectedMapElementLocator = RegisterContainerLocator<ObservableCollection<MapElement>>("SelectedMapElement", model => model.Initialize("SelectedMapElement", ref model._SelectedMapElement, ref _SelectedMapElementLocator, _SelectedMapElementDefaultValueFactory));
        static Func<ObservableCollection<MapElement>> _SelectedMapElementDefaultValueFactory = () => { return new ObservableCollection<MapElement>(); };
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
                            if(vm.MapElements.Count > 1)
                            {
                                foreach(var element in vm.MapElements)
                                {
                                    (element as MapIcon).Image = UnTappedIcon;
                                }
                            }

                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            var param = e.EventArgs.Parameter as MapElementClickItemEventArgs;
                            //foreach (var item in param.args.MapElements)
                            //{
                            var item = param.args.MapElements[0];
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
                                    icon.Image = TappedIcon;
                                
                                    vm.SelectedMallItem = vm.MallList.Where(n => n.name == icon.Title).First();
                                    vm.SelectedItemVisibility = Visibility.Visible;
                                    //Is MapIcon
                                    //
                                    //MallModel mall = vm.MallList.FirstOrDefault(n => n.name == icon.Title);
                                    //await vm.StageManager.DefaultStage.Show(new AtlasPage_Model(mall.buildings[0]));
                                    //if(param.isMaxZoom)
                                    //MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(null, vm.SelectedMallItem, typeof(MallModel), "NavigateToDetailByEventRouter", true);

                                    vm.MapCenter = icon.Location;
                                }
                            //}
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
                var resource = "GeoLocation";           // Command resource  
                var commandId = "GeoLocation";
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
                                vm.MapCenter = LocationManager.TransformFromWorldlToMars(position.Coordinate.Point);
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


        //CommandGoToAtalsPage
        public CommandModel<ReactiveCommand, String> CommandGoToAtalsPage
        {
            get { return _CommandGoToAtalsPageLocator(this).Value; }
            set { _CommandGoToAtalsPageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGoToAtalsPage Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGoToAtalsPage = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGoToAtalsPageLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGoToAtalsPageLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandGoToAtalsPage", model => model.Initialize("CommandGoToAtalsPage", ref model._CommandGoToAtalsPage, ref _CommandGoToAtalsPageLocator, _CommandGoToAtalsPageDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandGoToAtalsPageDefaultValueFactory =
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
                            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(null, vm.SelectedMallItem, typeof(MallModel), "NavigateToDetailByEventRouter", true);

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


        public void AddSelectedMallInMap(MallModel item)
        {
            var elements = new ObservableCollection<MapElement>();

            MapIcon mapIcon = new MapIcon();
            var bdPosition = new Geopoint(new BasicGeoposition()
            {
                Latitude = Double.Parse(item.lat),
                Longitude = Double.Parse(item.lon)
            });
            mapIcon.Location = bdPosition;//LocationManager.TransformFromWorldlToMars(bdPosition);
            mapIcon.Title = item.name;
            mapIcon.Image = TappedIcon;
            mapIcon.NormalizedAnchorPoint = new Point(0.5, 0.5);
            elements.Add(mapIcon);

            MapElements = elements;
            MapCenter = new Geopoint(new BasicGeoposition() { Latitude = Double.Parse(item.lat), Longitude = Double.Parse(item.lon) });
            MapZoomLevel = 14;

            SelectedMallItem = item;
            SelectedItemVisibility = Visibility.Visible;
        }

        public void AddAllMallsInMap()
        {
            SelectedMallItem = null;
            SelectedItemVisibility = Visibility.Collapsed;

           var elements = new ObservableCollection<MapElement>();

            foreach (var item in MallList)
            {
                MapIcon mapIcon = new MapIcon();
                var bdPosition = new Geopoint(new BasicGeoposition()
                {
                    Latitude = Double.Parse(item.lat),
                    Longitude = Double.Parse(item.lon)
                });
                if (MallList.IndexOf(item) == 0)
                {
                    MapCenter = bdPosition;
                }
                mapIcon.Location = bdPosition;//LocationManager.TransformFromWorldlToMars(bdPosition);
                mapIcon.Title = item.name;
                mapIcon.Image = UnTappedIcon;
                elements.Add(mapIcon);
            }
            MapElements = elements;
            MapZoomLevel = 12;
        }

        private void SuscribeCommand()
        {
            //MallList Button Tapped
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
                    .Where(x => x.EventName == "ListButtonClickByEventRouter")
                    .Subscribe(
                    e =>
                    {
                        var item = e.EventData as MallModel;
                        AddSelectedMallInMap(item);
                    }
                    ).DisposeWith(this);


            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
                .Where(x => x.EventName == "CitySelectedChangedEvent")
                .Subscribe(
                e =>
                { 
                    var mallList = e.EventData as List<MallModel>;
                    this.MallList = mallList;
                    //Display all the icons in map
                    AddAllMallsInMap();
                }
                ).DisposeWith(this);
        }

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
        //protected async override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        //{ 
        //    await base.OnBindedViewLoad(view);
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

