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
using Windows.UI.Core;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class AtlasPage_Model : ViewModelBase<AtlasPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        public WebView webView;
        public Building Building;
        public AtlasPage_Model()
        {
            if (IsInDesignMode)
            {
                SelectedMallModel = new MallModel()
                {
                    addr = "某某区  某某路  ",
                    desc = "描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述",
                    name = "某某某商场",
                    opentime = "20：00 ------ 10：00",
                    traffic = "公交路线"
                };
            }
        } 

        public AtlasPage_Model(MallModel mall)
        {
            //[{"Floor1": "541797c6ac4711c3332d6cd1",
            //"Floor2": "541797c6ac4711c3332d6cs1"}]
            SelectedMallModel = mall;
            Building = mall.buildings.FirstOrDefault();
            //Title = Building.name; 
        }

        //SelectedMallModel
        public MallModel SelectedMallModel
        {
            get { return _SelectedMallModelLocator(this).Value; }
            set { _SelectedMallModelLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property MallModel SelectedMallModel Setup
        protected Property<MallModel> _SelectedMallModel = new Property<MallModel> { LocatorFunc = _SelectedMallModelLocator };
        static Func<BindableBase, ValueContainer<MallModel>> _SelectedMallModelLocator = RegisterContainerLocator<MallModel>("SelectedMallModel", model => model.Initialize("SelectedMallModel", ref model._SelectedMallModel, ref _SelectedMallModelLocator, _SelectedMallModelDefaultValueFactory));
        static Func<BindableBase, MallModel> _SelectedMallModelDefaultValueFactory = m => new MallModel();
        #endregion



        //AutoSuggestText
        public String AutoSuggestText
        {
            get { return _AutoSuggestTextLocator(this).Value; }
            set { _AutoSuggestTextLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String AutoSuggestText Setup
        protected Property<String> _AutoSuggestText = new Property<String> { LocatorFunc = _AutoSuggestTextLocator };
        static Func<BindableBase, ValueContainer<String>> _AutoSuggestTextLocator = RegisterContainerLocator<String>("AutoSuggestText", model => model.Initialize("AutoSuggestText", ref model._AutoSuggestText, ref _AutoSuggestTextLocator, _AutoSuggestTextDefaultValueFactory));
        static Func<BindableBase, String> _AutoSuggestTextDefaultValueFactory = m => string.Empty;
        #endregion


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

        //ShopSearchList
        public List<ShopSearchResultModel> ShopSearchList
        {
            get { return _ShopSearchListLocator(this).Value; }
            set { _ShopSearchListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property List<ShopSearchResultModel> ShopSearchList Setup
        protected Property<List<ShopSearchResultModel>> _ShopSearchList = new Property<List<ShopSearchResultModel>> { LocatorFunc = _ShopSearchListLocator };
        static Func<BindableBase, ValueContainer<List<ShopSearchResultModel>>> _ShopSearchListLocator = RegisterContainerLocator<List<ShopSearchResultModel>>("ShopSearchList", model => model.Initialize("ShopSearchList", ref model._ShopSearchList, ref _ShopSearchListLocator, _ShopSearchListDefaultValueFactory));
        static Func<BindableBase, List<ShopSearchResultModel>> _ShopSearchListDefaultValueFactory = m => new List<ShopSearchResultModel>();
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

                            vm.webView = e.EventArgs.Parameter as WebView;
                            string param = vm.GetInvokeParamsInJson();

                            double altlasWith = 0;
                            double width = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                            //手机  并且是 横屏
                            if ((DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.LandscapeFlipped ||
                            DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Landscape)
                            && Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                            {
                                altlasWith = width * 3 / 5 - 30;
                            }
                            else
                            {
                                altlasWith = (width <= 500) ? width : 700;
                            }

                            await vm.webView.InvokeScriptAsync("StartInit", new string[] { param, altlasWith.ToString() });
                            // altlasWith.ToString() });
                        
                        })
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
                                //await new MessageDialog("接口中未提供店铺有用信息，后续版本将添加").ShowAsync();
                                //GoToDetailsPage
                                //string url = string.Format("http://op.juhe.cn/atlasyun/shop/detail?key={0}&cityid={1}&shopid={2}",
                                //    Configmanager.INDOORMAP_APPKEY, AppSettings.Intance.SelectedCityId, split[0]);
                                 
                                //    await vm.StageManager.DefaultStage.Show(new ShopDetailsPage_Model(url));
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
        
        //CommandAutoSuggestionQuerySubmitted
          public CommandModel<ReactiveCommand, String> CommandAutoSuggestionQuerySubmitted
        {
            get { return _CommandAutoSuggestionQuerySubmittedLocator(this).Value; }
            set { _CommandAutoSuggestionQuerySubmittedLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandAutoSuggestionQuerySubmitted Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandAutoSuggestionQuerySubmitted = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandAutoSuggestionQuerySubmittedLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandAutoSuggestionQuerySubmittedLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandAutoSuggestionQuerySubmitted", model => model.Initialize("CommandAutoSuggestionQuerySubmitted", ref model._CommandAutoSuggestionQuerySubmitted, ref _CommandAutoSuggestionQuerySubmittedLocator, _CommandAutoSuggestionQuerySubmittedDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandAutoSuggestionQuerySubmittedDefaultValueFactory =
            model =>
            {
                var resource = "AutoSuggestionQuerySubmitted";           // Command resource  
                var commandId = "AutoSuggestionQuerySubmitted";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {

                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();

                            if (string.IsNullOrEmpty(vm.AutoSuggestText)) return;

                            var args = e.EventArgs.Parameter as AutoSuggestBoxQuerySubmittedEventArgs;
                            if (args.ChosenSuggestion != null)
                            {
                                var selectedShop = (args.ChosenSuggestion as ShopSearchResultModel);
                                if(selectedShop.ch_name == "未查询到结果")
                                {
                                    return;
                                }

                                await vm.webView.InvokeScriptAsync("SetFloor", new string[] { selectedShop.floor.name });
                                await vm.webView.InvokeScriptAsync("MoveToPoiId", new string[] { selectedShop.id, "true" });
                                await vm.webView.InvokeScriptAsync("SetWidth", new string[] { "100" });

                                
                            }
                            else
                            { 
                                string searchUrl = string.Format(@"http://ap.atlasyun.com/poi/shop/search?kw={0}&bid={1}", vm.AutoSuggestText, vm.Building.id);
                                FormAction action = new FormAction(searchUrl);
                                action.isShowWaitingPanel = true;
                                action.Run();
                                action.FormActionCompleted += (result, tag) =>
                                {
                                    vm.PraseSearchResult(result, tag);
                                };
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

        private void PraseSearchResult(string result, string tag)
        {
            var shopList = JsonConvert.DeserializeObject<List<ShopSearchResultModel>>(result);
            if (shopList.Any())
            {
                ShopSearchList = shopList;
             }
            else
            {
                //没有数据
                ShopSearchList = new List<ShopSearchResultModel>() { new ShopSearchResultModel() { ch_name = "未查询到结果" } };
            }
        }

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

