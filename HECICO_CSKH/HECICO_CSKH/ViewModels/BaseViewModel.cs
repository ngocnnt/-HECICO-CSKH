using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using HECICO_CSKH.Models;
using HECICO_CSKH.Services;
using Plugin.Connectivity;
using System.Threading.Tasks;
using HECICO_CSKH.Dialog;
using HECICO_CSKH.Interface;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Essentials;
using HECICO_CSKH.Global;
using Newtonsoft.Json;

namespace HECICO_CSKH.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel ()
        {
            SelectKhachHangCommand = new Command(OnSelectKhachHangClicked);
            CallMeCommand = new Command(CallMeExcute);
        }
        private async void OnSelectKhachHangClicked(object obj)
        {
            await Navigation.PushModalAsync(new DanhSachKhachHang(LoadCustomerByTel()));
        }
        private void CallMeExcute(object obj)
        {
            try
            {
                PhoneDialer.Open("02252211309");
            }
            catch (Exception ex)
            {
                ShortAlert(ex.Message);

            }
        }
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public INavigation Navigation;
        public Command SelectKhachHangCommand { get; }
        public Command CallMeCommand { get; }
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public bool CheckInternet()
        {           
            if (!CrossConnectivity.Current.IsConnected)
            {
                Task.Run(() => new MessageInternetProblem().Show());
            }
            return CrossConnectivity.Current.IsConnected;
        }
        public  ObservableCollection<CustomerByTel> LoadCustomerByTel()
        { 
            try
            {
                if (!CheckInternet())
                {
                    return null;
                }
                using (HttpClient client = new HttpClient())
                {
                    KhachHangModel khang = new KhachHangModel();
                    khang.MaKhang = Preferences.Get(Config.MaKhachHang, "");
                    khang.Token = Preferences.Get(Config.Token, "");
                    khang.Sdt = Preferences.Get(Config.PhoneNumber, "");
                    client.BaseAddress = new Uri(Config.Url);
                    var ok = client.PostAsJsonAsync("api/getlistcustbytel", khang);
                    string _json = ok.Result.Content.ReadAsStringAsync().Result;
                    _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                    if (_json.Contains("error") == false && _json.Contains("[]") == false)
                    {
                        Int32 from = _json.IndexOf("[");
                        Int32 to = _json.IndexOf("]");
                        string result = _json.Substring(from, to - from + 1);
                        ObservableCollection<CustomerByTel> ListCustomerByTel = JsonConvert.DeserializeObject<ObservableCollection<CustomerByTel>>(result);
                        return ListCustomerByTel;
                    }
                    else
                    {
                        return null;
                    }
                }



            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void ShowLoading(string title)
        {
            DependencyService.Get<IProcessLoader>().Show(Title);
        }
        public void HideLoading()
        {
            DependencyService.Get<IProcessLoader>().Hide();
        }
        public void ShortAlert(string title)
        {
            DependencyService.Get<IMessage>().ShortAlert(title);
        }
        public void LongAlert(string title)
        {
            DependencyService.Get<IMessage>().LongAlert(title);
        }
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
