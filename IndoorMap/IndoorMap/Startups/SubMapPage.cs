using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using IndoorMap;
using IndoorMap.ViewModels;
using System;
using System.Net;
using System.Windows;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action SubMapPageConfig =
           CreateAndAddToAllConfig(ConfigSubMapPage);

        public static void ConfigSubMapPage()
        {
            ViewModelLocator<SubMapPage_Model>
                .Instance
                .Register(context => 
                    new SubMapPage_Model())
                .GetViewMapper()
                .MapToDefault<SubMapPage>();

        }
    }
}
