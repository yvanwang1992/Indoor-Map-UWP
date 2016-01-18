using IndoorMap.Models;
using IndoorMap.ViewModels;
using MVVMSidekick.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace IndoorMap.Controller
{
    public class FormAction
    {
        #region Delegate and Event

        public delegate void FormActionCompletedAction(string result, string tag);

        public event FormActionCompletedAction FormActionCompleted;
        public event FormActionCompletedAction FormActionError;

        private IViewModel _viewModel;
        public IViewModel viewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }
        #endregion

        private bool _isShowWaitingPanel;
        public bool isShowWaitingPanel
        {
            get { return _isShowWaitingPanel; }
            set { _isShowWaitingPanel = value; }
        }

        Uri uri { get; set; }
        HttpClient httpClient { get; set; }
        CancellationToken cancellationToken { get; set; }

        public FormAction(string requestUrl, IViewModel viewmodel = null)
        {
            //Do something with url
            viewModel = viewmodel;
            uri = new Uri(requestUrl, UriKind.RelativeOrAbsolute);
        }

        public async void Run(bool isWaitingPanelShow = true)
        {
            try
            {
                (App.Current as App).formAction = this;
                if (isWaitingPanelShow)
                {
                    WaitingPanelHelper.ShowWaitingPanel();
                }
                httpClient = new HttpClient() { };
                HttpRequestMessage requestMessage = new HttpRequestMessage() { };

                requestMessage.Method = HttpMethod.Get;
                requestMessage.RequestUri = uri;
                requestMessage.Headers.IfModifiedSince = DateTime.Now;
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);

                string content = await responseMessage.Content.ReadAsStringAsync();

                //if (viewModel == null)
                //{
                //    //Maybe you can do somthing else there. 
                //}
                //else
                //{
                //    if(viewModel is MainPage_Model)
                //    {
                //        JsonCityModel jsonCity = JsonConvert.DeserializeObject<JsonCityModel>(content);
                //        if (jsonCity.reason == "成功")
                //        {
                //            (viewModel as MainPage_Model).HttpClientReturn(jsonCity.result);
                //        }
                //        else
                //        {

                //        }
                //    }
                //    else if(viewModel is MainPage_Model)
                //    {

                //    }
                //}

                if (FormActionCompleted != null)
                    FormActionCompleted(content, "");

                if (WaitingPanelHelper.IsWaitingPanelExisted())
                    WaitingPanelHelper.HiddenWaitingPanel();
            }
            catch (Exception exception)
            {
                WaitingPanelHelper.HiddenWaitingPanel();

                if (!(exception is TaskCanceledException))
                {
                    //if (FormActionError != null)
                    //    FormActionError("", "");
                    ShowMessage(exception);
                }
            }
        }

        private async void ShowMessage(Exception exception)
        {
            await new MessageDialog(exception.Message).ShowAsync();
        }

        public void setHeaders()
        {

        }

        public void Absort()
        {
            httpClient.CancelPendingRequests();
        }
        
    }
     
}
