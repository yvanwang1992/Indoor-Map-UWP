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
using IndoorMap.Controller;
using Windows.UI.Core;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class ShopDetailsPage_Model : ViewModelBase<ShopDetailsPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性

        public string ActionUrl;

        public ShopDetailsPage_Model()
        {
            

        }

        public ShopDetailsPage_Model(string url)
        {
            ActionUrl = url;
            DoSomething();
        }

        public void DoSomething()
        {
            FormAction action = new FormAction(ActionUrl);
            action.isShowWaitingPanel = true;
            action.Run();
            action.FormActionCompleted += (result, ee) =>
            {
                JsonShopDetailsModel jsonShopDetails = JsonConvert.DeserializeObject<JsonShopDetailsModel>(result);
                if (jsonShopDetails.reason == "成功" || jsonShopDetails.reason == "successed")
                {
                    //HttpClientReturnCities(jsonShopDetails.result);
                    CommentList = jsonShopDetails.result.comments;
                    ShopName = jsonShopDetails.result.ch_name;
                }
            };
        }

        public String ShopName
        {
            get { return _ShopNameLocator(this).Value; }
            set { _ShopNameLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String ShopName Setup
        protected Property<String> _ShopName = new Property<String> { LocatorFunc = _ShopNameLocator };
        static Func<BindableBase, ValueContainer<String>> _ShopNameLocator = RegisterContainerLocator<String>("ShopName", model => model.Initialize("ShopName", ref model._ShopName, ref _ShopNameLocator, _ShopNameDefaultValueFactory));
        static Func<BindableBase, String> _ShopNameDefaultValueFactory = m => { return "姓名呀"; };
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

        //CommentList
        public List<Comment> CommentList
        {
            get { return _CommentListLocator(this).Value; }
            set { _CommentListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel CommentList Setup        
        protected Property<List<Comment>> _CommentList = new Property<List<Comment>> { LocatorFunc = _CommentListLocator };
        static Func<BindableBase, ValueContainer<List<Comment>>> _CommentListLocator = RegisterContainerLocator<List<Comment>>("CommentList", model => model.Initialize("CommentList", ref model._CommentList, ref _CommentListLocator, _CommentListDefaultValueFactory));
        static Func<BindableBase,List<Comment>> _CommentListDefaultValueFactory = m => { return new List<Comment>() { }; };
        #endregion



        #region Life Time Event Handling

        /// <summary>
        /// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        /// </summary>
        /// <param name="view">Set target</param>
        /// <param name="oldValue">Value before set.</param>
        /// <returns>Task awaiter</returns>
        protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        {
            return base.OnBindedToView(view, oldValue);
        }

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

