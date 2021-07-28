using HECICO_CSKH.Global;
using HECICO_CSKH.Views.ErrorAndEmpty;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HECICO_CSKH.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartupPage : ContentPage
    {
        public StartupPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(2000);
            if (!CrossConnectivity.Current.IsConnected)
            {
                App.Current.MainPage = new NoInternetConnectionPage();
            }
            else
            {
                if (Preferences.Get(Config.Token ,"") == "")
                App.Current.MainPage = new LoginPage ();
                else
                    App.Current.MainPage = new AppShell();
            }    
           
        }
    }
}