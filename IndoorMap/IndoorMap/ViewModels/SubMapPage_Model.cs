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

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class SubMapPage_Model : ViewModelBase<SubMapPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        public SubMapPage_Model()
        {

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


        //public void AddAllMallsInMap()
        //{
        //    var elements = new ObservableCollection<MapElement>();

        //    foreach (var item in MallList)
        //    {
        //        MapIcon mapIcon = new MapIcon();
        //        var bdPosition = new Geopoint(new BasicGeoposition()
        //        {
        //            Latitude = Double.Parse(item.lat),
        //            Longitude = Double.Parse(item.lon)
        //        });
        //        if (MallList.IndexOf(item) == 0)
        //        {
        //            MapCenter = bdPosition;
        //        }
        //        mapIcon.Location = bdPosition;//LocationManager.TransformFromWorldlToMars(bdPosition);
        //        mapIcon.Title = item.name;
        //        mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/delete_auto.png"));
        //        elements.Add(mapIcon);
        //    }
        //    MapElements = elements;
        //    MapZoomLevel = 10;
        //}

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

