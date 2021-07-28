using HECICO_CSKH.Global;
using HECICO_CSKH.Models;
using HECICO_CSKH.Models.Notification;
using HECICO_CSKH.Views.Notification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HECICO_CSKH.ViewModels.Notitication
{
 public   class NotificationViewModel : BaseViewModel
    {
        #region "Field"
        NotificationModel notificationModel;
        //ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL> _listThongBaoTienDien;
        //ObservableCollection<THONG_BAO_KHAC_MODEL> _listThongBaoKhac;
        //ObservableCollection<THONG_BAO_KHAC_MODEL> _listThongBaoCapDien;
        public  INavigation Navigation;
        #endregion

        #region "Contructor"

        public NotificationModel NotificationModels { get => notificationModel; set => SetProperty(ref notificationModel, value); }
        //public ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL> ListThongBaoTienDiens { get => _listThongBaoTienDien ; set => SetProperty(ref _listThongBaoTienDien, value); }
        //public ObservableCollection<THONG_BAO_KHAC_MODEL> ListThongBaoKhac { get => _listThongBaoKhac; set => SetProperty(ref _listThongBaoKhac, value); }
        //public ObservableCollection<THONG_BAO_KHAC_MODEL> ListThongBaoCapDien { get => _listThongBaoCapDien; set => SetProperty(ref _listThongBaoCapDien, value); }

        public NotificationViewModel() 
        {           
          
            NotificationModels = new NotificationModel();
            //ListThongBaoTienDiens = new ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL>();
            //ListThongBaoCapDien = new ObservableCollection<THONG_BAO_KHAC_MODEL>();
            //ListThongBaoKhac = new ObservableCollection<THONG_BAO_KHAC_MODEL>();
            LoadCommand = new Command(LoadExcute);
            ThongBaoTienDienCommand = new Command(OnThongBaoTienDienCLicked);
            ThongBaoCatDienCommand = new Command(OnThongBaoCatDienClicked);
            ThongBaoKhacCommand = new Command(OnThongBaoKhacClicked);
           
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

                    //ShowLoading("Đang load dữ liệu");
                    await Task.Delay(1000);
                    using (HttpClient client = new HttpClient())
                    {
                        KhachHangModel khang = new KhachHangModel();
                        khang.MaKhang = Preferences.Get(Config.MaKhachHang, "");
                        khang.Token = Preferences.Get(Config.Token, "");
                        khang.Sdt = Preferences.Get(Config.PhoneNumber , "");
                        client.BaseAddress = new Uri(Config.Url);
                        var ok = client.PostAsJsonAsync("api/getthongbaotiendien", khang);
                        string _json = ok.Result.Content.ReadAsStringAsync().Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                            Int32 from = _json.IndexOf("[");
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                            ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL> ListThongBaoTienDiens = JsonConvert.DeserializeObject<ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL>>(result);
                            NotificationModels.ThongBaoTienDienCount = ListThongBaoTienDiens.Where(p => p.TRANG_THAI_XEM == 0).ToList().Count.ToString();
                            OnPropertyChanged("NotificationModels");                           

                        }
                        //lấy thông báo khác
                        var noticeother = client.PostAsJsonAsync("api/getthongbaokhac", khang);
                         _json = noticeother.Result.Content.ReadAsStringAsync().Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                            Int32 from = _json.IndexOf("[");
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                            ObservableCollection <THONG_BAO_KHAC_MODEL> ThongBao= JsonConvert.DeserializeObject<ObservableCollection<THONG_BAO_KHAC_MODEL>>(result);
                            NotificationModels.ThongBaoKhacCount = ThongBao.Where(p => p.TRANG_THAI_XEM == 0 && p.LOAI_THONG_BAO == "4").ToList().Count.ToString();
                            NotificationModels.ThongBaoCapDienCount = ThongBao.Where(p => p.TRANG_THAI_XEM == 0 && p.LOAI_THONG_BAO == "3").ToList().Count.ToString();
                            OnPropertyChanged("NotificationModels");
                        }
                    }
                  //  HideLoading();

                }
                catch (Exception ex)
                {
                   /// HideLoading();
                }
            }
            catch (Exception)
            {

                //HideLoading();
            }
        }
        private async void OnThongBaoTienDienCLicked(object obj)
        {
            await Navigation.PushAsync(new ThongBaoTienDienPage());
        }
        private async void OnThongBaoKhacClicked(object obj)
        {
            await Navigation.PushAsync(new ThongBaoKhacPage());
        }

        private async void OnThongBaoCatDienClicked(object obj)
        {
            await Navigation.PushAsync(new ThongBaoNgungCapDienPage());
        }
        #endregion

        #region "Command"
        public Command LoadCommand { get; }       
        public Command ThongBaoTienDienCommand { get; }
        public Command ThongBaoCatDienCommand { get; }
        public Command ThongBaoKhacCommand { get; }
        #endregion
    }
}
