using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using HECICO_CSKH.Models;
using Xamarin.Forms;
using HECICO_CSKH.Global;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Net.Http;
using HECICO_CSKH.Dialog;

namespace HECICO_CSKH.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region "Field"
        string _ten_khang;
        CustomerModel _customer;
        ObservableCollection <   CustomerModel> _listcustomer;
        ObservableCollection<CustomerByTel> _listCustomerbytel;
        CustomerByTel _selectItem;       
        #endregion

        #region "Contructor"      
        public string TEN_KHANG { get => _ten_khang; set => SetProperty(ref _ten_khang, value); }
        public ObservableCollection<ImageModel> ListImage { get; set; }
        public CustomerModel Customer { get => _customer; set => SetProperty(ref _customer,value ); }
        public ObservableCollection<CustomerModel> ListCustomer { get => _listcustomer; set => SetProperty(ref _listcustomer, value); }
        public HomeViewModel()
        {
            ListImage = new ObservableCollection<ImageModel>
            { 
                new ImageModel {ImagePath= "http://smart.cpc.vn/HecicoApi/image/hinh1.jpg"},
                new ImageModel {ImagePath="http://smart.cpc.vn/HecicoApi/image/hinh2.jpg"},
                new ImageModel {ImagePath="http://smart.cpc.vn/HecicoApi/image/hinh3.jpg"}
            };
            ListCustomer = new ObservableCollection<CustomerModel>();
            ListCustomerByTel = new ObservableCollection<CustomerByTel>();
            LoadCommand = new Command(LoadExcute);
           
            OpenWebCommand =  new Command(async () => await new Barcode(_selectItem.MA_KHANG ).Show());
           // SelectKhachHangCommand = new Command(OnSelectKhachHangClicked);
            MessagingCenter.Subscribe<DanhSachKhachHang, CustomerByTel>(this, "chonkhachhang", (sender, item) => {
                TEN_KHANG = item.TEN_KHANG;
                SelectItem = item;               
            });
        }

       
        public ObservableCollection<CustomerByTel> ListCustomerByTel { get => _listCustomerbytel; set => SetProperty(ref _listCustomerbytel, value); }
        public CustomerByTel SelectItem
        {
            get => _selectItem;
            set
            {
                SetProperty(ref _selectItem, value);
                Task.Run(() => TimKiem());
            }
        }
        #endregion

        #region "Method"
        private async void LoadExcute(object obj)
        {
            try
            {
                if (!CheckInternet())
                {
                    return;
                }
                try
                {
                   
                    ShowLoading("Đang load dữ liệu");
                    await Task.Delay(1000);
                    ListCustomerByTel = LoadCustomerByTel();
                    if (ListCustomerByTel != null )
                    {
                        SelectItem = ListCustomerByTel[0];
                        TEN_KHANG = SelectItem.TEN_KHANG;
                    }
                    HideLoading();
                }
                catch (Exception ex)
                {
                    HideLoading();
                }
            }
            catch (Exception)
            {

                HideLoading();
            }
        }
     
        
        async Task TimKiem()
        {
            try
            {
                if (!CheckInternet())
                {
                    return;
                }

                using (HttpClient client = new HttpClient())
                {
                    KhachHangModel khang = new KhachHangModel();
                    khang.MaKhang = _selectItem.MA_KHANG;
                    khang.Token = Preferences.Get(Config.Token, "");
                    client.BaseAddress = new Uri(Config.Url);
                    var ok = client.PostAsJsonAsync("api/getthongtinkhachhang", khang);
                    string _json = ok.Result.Content.ReadAsStringAsync().Result;
                    _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                    if (_json.Contains("error") == false && _json.Contains("[]") == false)
                    {
                        Int32 from = _json.IndexOf("[");
                        Int32 to = _json.IndexOf("]");
                        string result = _json.Substring(from, to - from + 1);
                        ListCustomer = JsonConvert.DeserializeObject<ObservableCollection<CustomerModel>>(result);
                        if (ListCustomer.Count > 0)
                        {
                            Customer = ListCustomer[0];
                        }
                        HideLoading();

                    }
                    else
                    {
                        HideLoading();
                    }
                }
            }
            catch (Exception)
            {

                HideLoading();
            }
        }
        #endregion

        #region "Command"
        public Command LoadCommand { get; }
       
        public Command OpenWebCommand { get; }
        
        #endregion
    }
}
