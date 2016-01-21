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
using IndoorMap.Controller;
using Newtonsoft.Json;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class SubMallListPage_Model : ViewModelBase<SubMallListPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        bool isLoaded = false;

        public SubMallListPage_Model(List<MallModel> mallList)
        {
            this.MallList = mallList;
        }

        public SubMallListPage_Model()
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

        private void GetSupportMallListAction()
        {
            string url = string.Format(@"http://op.juhe.cn/atlasyun/mall/list?key={0}&cityid={1}", Configmanager.INDOORMAP_APPKEY, AppSettings.Intance.SelectedCityId);
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
        public void HttpClientReturnMallList(List<MallModel> jsonMall)
        {
            this.MallList = jsonMall;
        }

        private void SuscribeCommand()
        {
            //MallList Item Tapped
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
                .Where(x => x.EventName == "CitySelectedChangedEvent")
                .Subscribe(
                e =>
                {
                    var city = e.EventData as CityModel;
                    //获取保存的所有城市
                    AppSettings.Intance.SelectedCityId = city.id;

                    GetSupportMallListAction();

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
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            if (!isLoaded)
            {
                SuscribeCommand();
                isLoaded = true;
            }
            return base.OnBindedViewLoad(view);
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

