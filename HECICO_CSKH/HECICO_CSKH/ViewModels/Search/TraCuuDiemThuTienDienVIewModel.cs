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
   public class TraCuuDiemThuTienDienVIewModel :BaseViewModel
    {
        #region "Field"
        DateTime _fromdate;
        DateTime _todate;
        ObservableCollection<TraCuuDiemThuTienDienModel> _listTraCuu;
        ObservableCollection<CustomerByTel> _listCustomer;
        CustomerByTel _selectItem;
        #endregion

        #region "Contructor"
        string _ten_khang;
        public string TEN_KHANG { get => _ten_khang; set => SetProperty(ref _ten_khang, value); }
        public DateTime FromDate { get => _fromdate; set => SetProperty(ref _fromdate, value); }
        public DateTime ToDate { get => _todate; set => SetProperty(ref _todate, value); }
        public ObservableCollection<TraCuuDiemThuTienDienModel> ListTraCuu { get => _listTraCuu; set => SetProperty(ref _listTraCuu, value); }
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
        public TraCuuDiemThuTienDienVIewModel()
        {
            DateTime date = DateTime.Now.Date.AddMonths(-3);
            _fromdate = new DateTime(date.Year, date.Month, 1).Date;
            _todate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
            ListTraCuu = new ObservableCollection<TraCuuDiemThuTienDienModel>();
            ListCustomer = new ObservableCollection<CustomerByTel>();          
            LoadCommand = new Command(OnLoadExcute);
            MessagingCenter.Subscribe<DanhSachKhachHang, CustomerByTel>(this, "chonkhachhang", (sender, item) => {
                TEN_KHANG = item.TEN_KHANG;
                SelectItem = item;
            });
        }
        #endregion

        #region "Method"
      
        private async void OnLoadExcute(object obj)
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
                try
                {
                    if (IsBusy == true) return;
                    IsBusy = true;
                    using (HttpClient client = new HttpClient())
                    {
                        ListTraCuu.Clear();
                        client.BaseAddress = new Uri(Config.Url);
                        var ok = client.GetStringAsync("api/getdiemthutiendien/" + _selectItem.MA_KHANG);
                        string _json = ok.Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                            Int32 from = _json.IndexOf("[");
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                            ListTraCuu = JsonConvert.DeserializeObject<ObservableCollection<TraCuuDiemThuTienDienModel>>(result);
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
        public Command LoadCommand { get; }      
        #endregion
    }
}
 