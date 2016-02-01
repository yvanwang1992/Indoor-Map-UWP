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
using IndoorMap.Helpers;
using Windows.UI.Popups;
using Windows.UI.Core;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class SettingPage_Model : ViewModelBase<SettingPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        public Building building;
        private bool isLoadSuscribe;

        public SettingPage_Model()
        {
            isLoadSuscribe = false;
            if (IsInDesignMode)
            {
                SettingList = new ObservableCollection<SettingModel>()
                {
                    new SettingModel(true, AppSettings.LocationSettingKey) { DisplayName = "允许获取位置信息"},
                    new SettingModel(false, AppSettings.NetworkSettingKey) { DisplayName = "仅在WIFI下使用网络"},
                    new SettingModel(false, AppSettings.LandmarksVisibleSettingKey) { DisplayName = "显示地标性建筑"}
                };
            }
        }
         
        public SettingPage_Model(Building buiding)
        {
            //[{"Floor1": "541797c6ac4711c3332d6cd1",
            //"Floor2": "541797c6ac4711c3332d6cs1"}]

            building = buiding;
            
            //读取html 
            //StorageHelper.GetFileUsingUrl();
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

        //设置列表  在其他地方更改值
        public ObservableCollection<SettingModel> SettingList
        {
            get { return _SettingListLocator(this).Value; }
            set { _SettingListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<SettingModel> SettingList Setup
        protected Property<ObservableCollection<SettingModel>> _SettingList = new Property<ObservableCollection<SettingModel>> { LocatorFunc = _SettingListLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<SettingModel>>> _SettingListLocator = RegisterContainerLocator<ObservableCollection<SettingModel>>("SettingList", model => model.Initialize("SettingList", ref model._SettingList, ref _SettingListLocator, _SettingListDefaultValueFactory));
        static Func<BindableBase, ObservableCollection<SettingModel>> _SettingListDefaultValueFactory = m =>
        {
            return new ObservableCollection<SettingModel>()
            {
                new SettingModel(true, AppSettings.LocationSettingKey) { DisplayName = "允许获取位置信息"},
                    new SettingModel(false, AppSettings.NetworkSettingKey) { DisplayName = "允许使用2G/3G/4G网络"},
                    new SettingModel(false, AppSettings.LandmarksVisibleSettingKey) { DisplayName = "显示地标性建筑"}
            };
        };
        #endregion


        private void SuscribeCommand()
        { 

            //MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
            //    .Where(x => x.EventName == "ListButtonClickByEventRouter")
            //    .Subscribe(
            //     e =>
            //    {
            //        var item = e.EventData as MallModel;
            //        if (item != null)
            //        {
            //            //await new MessageDialog("Setting点击了按钮").ShowAsync();
            //            //await StageManager.DefaultStage.Show(new AtlasPage_Model(item.buildings.FirstOrDefault()));
            //        }
            //    }
            //    ).DisposeWith(this);
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

        /// <summary>
        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        /// </summary>
        /// <param name="view">View that firing Load event</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            if (!isLoadSuscribe)
            {
                SuscribeCommand();
                isLoadSuscribe = true;
            }
            return base.OnBindedViewLoad(view);
        }

        /// <summary>
        /// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        /// </summary>
        /// <param name="view">View that firing Unload event</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        {
            var visible = AppSettings.Intance.GetAppSetting(AppSettings.LandmarksVisibleSettingKey);
            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(this, visible, typeof(bool), "ChangeMapLandMarks", true);
            return base.OnBindedViewUnload(view);
        }

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

