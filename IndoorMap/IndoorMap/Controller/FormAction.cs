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
                if (isWaitingPanelShow)
                {
                    WaitingPanelHelper.ShowWaitingPanel();
                }
                httpClient = new HttpClient() { };
                HttpRequestMessage requestMessage = new HttpRequestMessage() { };
                requestMessage.Method = HttpMethod.Get;
                requestMessage.RequestUri = uri;

                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
                string a = await responseMessage.Content.ReadAsStringAsync();
                
                if (viewModel == null)
                {
                    
                }
                else
                {
                    if(viewModel is MainPage_Model)
                    {

                    }
                    else if(viewModel is MainPage_Model)
                    {

                    }
                    var aa = JsonConvert.DeserializeObject<JsonMallModel>(a);
                }

                if (FormActionCompleted != null)
                    FormActionCompleted(a, "");

                if (WaitingPanelHelper.IsWaitingPanelExisted())
                    WaitingPanelHelper.HiddenWaitingPanel();
            }
            catch (Exception ee)
            {
                if (FormActionError != null)
                    FormActionError("", "");
            }
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
