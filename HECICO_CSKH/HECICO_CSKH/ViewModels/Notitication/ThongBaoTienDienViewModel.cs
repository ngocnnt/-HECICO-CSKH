using HECICO_CSKH.Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using HECICO_CSKH.Models.Search;
using System.Net.Http;
using HECICO_CSKH.Models;
using HECICO_CSKH.Models.Notification;
using HECICO_CSKH.Views.Notification;

namespace HECICO_CSKH.ViewModels.Notitication
{
    public class ThongBaoTienDienViewModel : BaseViewModel
    {
        #region "Field"      
       public  INavigation Navigation;
        ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL> _listTraCuu;
        THONG_BAO_TIEN_DIEN_MODEL _selectItem;
        #endregion

        #region "Contructor"
       
        public ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL> ListTraCuu { get => _listTraCuu; set => SetProperty(ref _listTraCuu, value); }
        public THONG_BAO_TIEN_DIEN_MODEL SelectItem { get => _selectItem; set {
                _selectItem = value;
                try
                {
                    try
                    {
                        if (!CheckInternet())
                        {
                            return;
                        }
                        try
                        {
                             Task.Delay(1000);
                            using (HttpClient client = new HttpClient())
                            {
                                KhachHangModel khang = new KhachHangModel();                               
                                khang.Token = Preferences.Get(Config.Token, "");
                                khang.HoaDonId = _selectItem.HOADON_ID;
                                khang.NoticeType = _selectItem.LOAI_THONG_BAO;
                                client.BaseAddress = new Uri(Config.Url);
                                var ok = client.PostAsJsonAsync("api/postnoticesetview", khang);
                                string _json = ok.Result.Content.ReadAsStringAsync().Result;
                                _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                                SelectItem.TRANG_THAI_XEM =0 ;
                                OnPropertyChanged(nameof(SelectItem));



                            }


                        }
                        catch (Exception ex)
                        {
                          
                        }
                    }
                    catch (Exception)
                    {

                      
                    }
                    if (_selectItem.LOAI_THONG_BAO == "1") // Thông báo tiền điện
                    {
                        Navigation.PushAsync(new ThongBaoTienDien_ChiTietPage(_selectItem ));
                    }    
                    else // = 1 là thông báo thanh toán
                    {
                        Navigation.PushAsync(new ThongBaoTienDien_DaThanhToanPage(_selectItem));
                    }                        
                }
                catch (Exception ex)
                {

                   
                }
            } }
        public ThongBaoTienDienViewModel()
        {
            ListTraCuu = new ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL>();           
            LoadCommand = new Command(LoadExcute);
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
                    using (HttpClient client = new HttpClient())
                    {
                        KhachHangModel khang = new KhachHangModel();                       
                        khang.Token = Preferences.Get(Config.Token, "");
                        khang.Sdt = Preferences.Get(Config.PhoneNumber, "");
                        client.BaseAddress = new Uri(Config.Url);
                        var ok = client.PostAsJsonAsync("api/getthongbaotiendien", khang);
                        string _json = ok.Result.Content.ReadAsStringAsync().Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                            Int32 from = _json.IndexOf("[");
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                            ListTraCuu = JsonConvert.DeserializeObject<ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL>>(result);
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
