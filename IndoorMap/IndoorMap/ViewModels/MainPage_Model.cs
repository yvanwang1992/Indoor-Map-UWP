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


        //Current Channel
        public List<CityModel> SupportCities
        {
            get { return _SupportCitiesLocator(this).Value; }
            set { _SupportCitiesLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel CurrentChannel Setup        
        protected Property<List<CityModel>> _SupportCities = new Property<List<CityModel>> { LocatorFunc = _SupportCitiesLocator };
        static Func<BindableBase, ValueContainer<List<CityModel>>> _SupportCitiesLocator = RegisterContainerLocator<List<CityModel>>("SupportCities", model => model.Initialize("SupportCities", ref model._SupportCities, ref _SupportCitiesLocator, _SupportCitiesDefaultValueFactory));
        static Func<List<CityModel>> _SupportCitiesDefaultValueFactory = () => { return new List<CityModel>(); };
        #endregion

        #region Commands
        //Request Button
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
                cmd
                    //.DoExecuteUIBusyTask(
                    //    vm,
                    //    e =>
                    //    {
                    //        //var seletedItem = e.EventArgs.Parameter as Channel;
                    //        //if (seletedItem != null)
                    //        //{
                    //        //    vm.DealWithCatalogs(seletedItem);
                    //        //    //vm.CurrentChannel = seletedItem.Url;
                    //        //    //vm.CurrentChannel = seletedItem;
                    //        //}
                    //        ////Todo: Add GetSupportCities logic here, or
                    //        ////await MVVMSidekick.Utilities.TaskExHelper.Yield();
                    //    }
                    //)
                     .DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        { 
                            vm.Test();
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

        #endregion

        private void Test()
        {
            string url = @"http://op.juhe.cn/atlasyun/city/list?key=" + Configmanager.INDOORMAP_APPKEY;
            //string url = @"http://op.juhe.cn/atlasyun/mall/list?key=" + Configmanager.INDOORMAP_APPKEY + "&cityid=53d5e4c85620fa7f111a3f67";
            FormAction action = new FormAction(url);
            action.viewModel = this;
            action.Run();
            action.FormActionCompleted += (ss, ee) =>
            {
            };
        }

        public void HttpClientReturn(List<CityModel> jsonCity)
        {
            this.SupportCities = jsonCity;
            Debug.WriteLine(SupportCities);

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

