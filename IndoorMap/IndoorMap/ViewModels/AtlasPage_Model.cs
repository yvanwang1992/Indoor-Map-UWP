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
using IndoorMap.Models;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using IndoorMap.Controller;
using Windows.Phone.UI.Input;
using IndoorMap.Helpers;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class AtlasPage_Model : ViewModelBase<AtlasPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        public Building Building;

        public AtlasPage_Model()
        { 
        } 

        public AtlasPage_Model(Building buiding)
        {
            //[{"Floor1": "541797c6ac4711c3332d6cd1",
            //"Floor2": "541797c6ac4711c3332d6cs1"}]
            Building = buiding;
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


        #region ---------------------   Command     -------------------------
        //Loading Map when the webview is loaded.
        public CommandModel<ReactiveCommand, String> CommandAtlasLoading
        {
            get { return _CommandAtlasLoadingLocator(this).Value; }
            set { _CommandAtlasLoadingLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandAtlasLoading Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandAtlasLoading = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandAtlasLoadingLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandAtlasLoadingLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandAtlasLoading", model => model.Initialize("CommandAtlasLoading", ref model._CommandAtlasLoading, ref _CommandAtlasLoadingLocator, _CommandAtlasLoadingDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandAtlasLoadingDefaultValueFactory =
            model =>
            {
                var resource = "AtlasLoading";           // Command resource  
                var commandId = "AtlasLoading";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or                  
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();

                            WebView webView = e.EventArgs.Parameter as WebView;
                            string param = vm.GetInvokeParamsInJson();
                            await webView.InvokeScriptAsync("StartInit", new string[] { param });

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

        //when the webview's event: scriptNotify is invoke
        //CommandLvMallItemClick
        public CommandModel<ReactiveCommand, String> CommandScriptNotify
        {
            get { return _CommandScriptNotifyLocator(this).Value; }
            set { _CommandScriptNotifyLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandScriptNotify Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandScriptNotify = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandScriptNotifyLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandScriptNotifyLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandScriptNotify", model => model.Initialize("CommandScriptNotify", ref model._CommandScriptNotify, ref _CommandScriptNotifyLocator, _CommandScriptNotifyDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandScriptNotifyDefaultValueFactory =
            model =>
            {
                var resource = "ScriptNotify";           // Command resource  
                var commandId = "ScriptNotify";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                           
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or                  
                            NotifyEventArgs notify = e.EventArgs.Parameter as NotifyEventArgs;
                            var split = notify.Value.Split('&');
                            //split[0]: POI ID  ;   split[1]: POI Name
                            if (split[0].Length == 1)
                            {
                                await new MessageDialog("这是:" + split[1]).ShowAsync();
                            }
                            else
                            {
                                //GoToDetailsPage
                                string url = string.Format("http://op.juhe.cn/atlasyun/shop/detail?key={0}&cityid={1}&shopid={2}",
                                    Configmanager.INDOORMAP_APPKEY, AppSettings.Intance.SelectedCityId, split[0]);
                                 
                                    await vm.StageManager.DefaultStage.Show(new ShopDetailsPage_Model(url));
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


        #region -----------------------    Method    -------------------------
        public string GetInvokeParamsInJson()
        {
            var floors = Building.floors;
            List<Object> obj = new List<Object>() { };
            foreach (var floor in floors)
            {
                if (!string.IsNullOrEmpty(floor.floorB3))
                {
                    obj.Add(new FloorB3Class() { FloorB3 = floor.floorB3 });
                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floorB2))
                {
                    obj.Add(new FloorB2Class() { FloorB2 = floor.floorB2 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floorB1))
                {
                    obj.Add(new FloorB1Class() { FloorB1 = floor.floorB1 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor1))
                {
                    obj.Add(new Floor1Class() { Floor1 = floor.floor1 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor2))
                {
                    obj.Add(new Floor2Class() { Floor2 = floor.floor2 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor3))
                {
                    obj.Add(new Floor3Class() { Floor3 = floor.floor3 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor4))
                {
                    obj.Add(new Floor4Class() { Floor4 = floor.floor4 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor5))
                {
                    obj.Add(new Floor5Class() { Floor5 = floor.floor5 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor6))
                {
                    obj.Add(new Floor6Class() { Floor6 = floor.floor6 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor7))
                {
                    obj.Add(new Floor7Class() { Floor7 = floor.floor7 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor8))
                {
                    obj.Add(new Floor8Class() { Floor8 = floor.floor8 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor9))
                {
                    obj.Add(new Floor9Class() { Floor9 = floor.floor9 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor10))
                {
                    obj.Add(new Floor10Class() { Floor10 = floor.floor10 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor11))
                {
                    obj.Add(new Floor11Class() { Floor11 = floor.floor11 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor12))
                {
                    obj.Add(new Floor12Class() { Floor12 = floor.floor12 });

                    continue;
                }
            }

            //object[] obj = new object[] {
            //    new FloorB2Class{ FloorB2 = "539098c7b8604a3d36934658"},
            //    new FloorB1Class { FloorB1 = "539098c7b8604a3d3693463d"},
            //    new Floor1Class { Floor1 = "539098c7b8604a3d369345ed"} };

            return JsonConvert.SerializeObject(obj);
        }
        #endregion

    }

}

