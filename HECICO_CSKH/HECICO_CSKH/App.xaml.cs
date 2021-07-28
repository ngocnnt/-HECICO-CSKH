using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HECICO_CSKH.Services;
using HECICO_CSKH.Views;
using Plugin.Connectivity;
using HECICO_CSKH.Views.ErrorAndEmpty;
using HECICO_CSKH.Interface;
using HECICO_CSKH.Global;
using Plugin.FirebasePushNotification;

namespace HECICO_CSKH
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzA4MTIyQDMxMzgyZTMyMmUzME4rVWJvRGdVY0ZibWlYbUFBN1dyNVFjemJ5djZ5dWQzZzdMaDNEQ1hBN3M9");
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IProcessLoader>();
            Config.client = new System.Net.Http.HttpClient();
            MainPage = new StartupPage();
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");
                
                

            };
            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                }

            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
