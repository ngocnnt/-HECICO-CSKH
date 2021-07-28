using HECICO_CSKH.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using HECICO_CSKH.Models.Search;
using Newtonsoft.Json;
using HECICO_CSKH.Models;
using System.Net.Http;
using HECICO_CSKH.Dialog;

namespace HECICO_CSKH.ViewModels.Search
{
   public class TraCuuThongTinThanhToanViewModel : BaseViewModel
    {
        #region "Field"
        DateTime _fromdate;
        DateTime _todate;
        ObservableCollection<TraCuuThongTinThanhToanModel> _listTraCuu;
        ObservableCollection<CustomerByTel> _listCustomer;
        CustomerByTel _selectItem;
        #endregion

        #region "Contructor"
        string _ten_khang;
        public string TEN_KHANG { get => _ten_khang; set => SetProperty(ref _ten_khang, value); }
        public DateTime FromDate { get => _fromdate; set => SetProperty(ref _fromdate, value); }
        public DateTime ToDate { get => _todate; set => SetProperty(ref _todate, value); }
        public ObservableCollection<TraCuuThongTinThanhToanModel> ListTraCuu { get => _listTraCuu; set => SetProperty(ref _listTraCuu, value); }
        public ObservableCollection<CustomerByTel> ListCustomer { get => _listCustomer; set => SetProperty(ref _listCustomer, value); }
        public CustomerByTel SelectItem
        {
            get => _selectItem;
            set
            {
                SetProperty(ref _selectItem, value);
                Task.Run(() => TimKiem());
            }
        }
        public TraCuuThongTinThanhToanViewModel() 
        {
            DateTime date = DateTime.Now.Date.AddMonths(-3);
            _fromdate = new DateTime(date.Year, date.Month, 1).Date;
            _todate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
            ListTraCuu = new ObservableCollection<TraCuuThongTinThanhToanModel>();
            ListCustomer = new ObservableCollection<CustomerByTel>();
            SearchCommand = new Command(OnSearchClicked);
            LoadCommand = new Command(OnLoadExcute);
            SelectTuThangCommand = new Command(OnTuThangClicked);
            SelectDenThangCommand  = new Command(OnDenThangClicked);
            MessagingCenter.Subscribe<DanhSachKhachHang, CustomerByTel>(this, "chonkhachhang", (sender, item) => {
                TEN_KHANG = item.TEN_KHANG;
                SelectItem = item;
            });
        }

        

        #endregion

        #region "Method"
        private async void OnSearchClicked(object obj)
        {
            await TimKiem();
        }
        private async void OnLoadExcute(object obj)
        {
            try
            {
                if (!CheckInternet())
                {
                    return;
                }

                ShowLoading("Đang load dữ liệu");
                await Task.Delay(1000);
                ListCustomer = LoadCustomerByTel();
                if (ListCustomer != null)
                {
                    SelectItem = ListCustomer[0];
                    TEN_KHANG = SelectItem.TEN_KHANG;
                }
                HideLoading();


            }
            catch (Exception ex)
            {
                HideLoading();
            }
        }
        private async void OnDenThangClicked(object obj)
        {
            ToDate  = await new ChonThangNam(ToDate).Show();
        }

        private async void OnTuThangClicked(object obj)
        {
            FromDate = await new ChonThangNam(FromDate).Show();
        }
        async Task TimKiem()
        {
            try
            {
                if (!CheckInternet())
                {
                    return;
                }
                try
                {
                    if (IsBusy == true) return;
                    IsBusy = true;
                    using (HttpClient client = new HttpClient())
                    {
                        ListTraCuu.Clear();
                        KhachHangModel khang = new KhachHangModel();
                        khang.MaKhang = _selectItem.MA_KHANG;
                        khang.Token = Preferences.Get(Config.Token, "");
                        khang.ThangBatDau = string.Format("{0:yyyyMM}", _fromdate);
                        khang.ThangKetThuc = string.Format("{0:yyyyMM}", _todate);
                        client.BaseAddress = new Uri(Config.Url);
                        var ok = client.PostAsJsonAsync("api/tracuuthongtinhoadon", khang);
                        string _json = ok.Result.Content.ReadAsStringAsync().Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                            Int32 from = _json.IndexOf("[");
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                            ListTraCuu = JsonConvert.DeserializeObject<ObservableCollection<TraCuuThongTinThanhToanModel>>(result);
                            HideLoading();
                        }
                        else
                        {
                            HideLoading();
                        }
                    }


                }
                catch (Exception ex)
                {
                    HideLoading();
                }
                finally
                {
                    IsBusy = false;
                }
            }
            catch (Exception)
            {

                HideLoading();
            }
        }
        #endregion

        #region "Command"
        public Command SearchCommand { get; }
        public Command LoadCommand { get; }
        public Command SelectTuThangCommand { get; }
        public Command SelectDenThangCommand { get; }
        #endregion
    }
}
