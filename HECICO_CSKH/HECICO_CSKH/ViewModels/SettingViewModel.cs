using HECICO_CSKH.Dialog;
using HECICO_CSKH.Global;
using HECICO_CSKH.Models;
using HECICO_CSKH.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HECICO_CSKH.ViewModels
{
   public  class SettingViewModel :BaseViewModel
    {
        #region "Field"
        CustomerModel _customer;
        INavigation Navigation;
        ObservableCollection<CustomerModel> _listcustomer;
        #endregion

        #region "Contructor"
        public ObservableCollection<ImageModel> ListImage { get; set; }
        public CustomerModel Customer { get => _customer; set => SetProperty(ref _customer, value); }
        public ObservableCollection<CustomerModel> ListCustomer { get => _listcustomer; set => SetProperty(ref _listcustomer, value); }
        public SettingViewModel()
        {


            LogoutCommand = new Command(OnLogoutClicked);
            ChangePasswordCommand = new Command(OnChangePassClicked);
            InformationCommand = new Command(OnShowInformationClicked);          
        }

       



        #endregion

        #region "Method"
        private void OnLogoutClicked(object obj)
        {
            App.Current.MainPage = new LoginPage();
        }
        private async void OnChangePassClicked(object obj)
        {
            try
            {
                var ok = await new MessageChangePassword().Show();
                if (ok == DialogReturn.OK)
                {
                    App.Current.MainPage = new LoginPage();
                }
            }
            catch (Exception)
            {


            }
        }

        private  async void OnShowInformationClicked(object obj)
        {
            await new AppInformation ().Show();
        }
        
        #endregion

        #region "Command"
        public Command InformationCommand { get; } 
        public Command LogoutCommand { get; }
        public Command ChangePasswordCommand { get; }        
        #endregion
    }
}
