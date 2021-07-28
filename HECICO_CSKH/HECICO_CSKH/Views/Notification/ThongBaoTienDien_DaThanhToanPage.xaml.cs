using HECICO_CSKH.Global;
using HECICO_CSKH.Models;
using HECICO_CSKH.Models.Notification;
using HECICO_CSKH.ViewModels;
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
using Xamarin.Forms.Xaml;

namespace HECICO_CSKH.Views.Notification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThongBaoTienDien_DaThanhToanPage : ContentPage
    {
        THONG_BAO_TIEN_DIEN_MODEL TienDien;
        public ThongBaoTienDien_DaThanhToanPage(THONG_BAO_TIEN_DIEN_MODEL tiendien)
        {
            InitializeComponent();
            try
            {

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        KhachHangModel khang = new KhachHangModel();                        
                        khang.Token = Preferences.Get(Config.Token, "");                       
                        khang.HoaDonId = tiendien.HOADON_ID;
                        client.BaseAddress = new Uri(Config.Url);
                        var ok = client.PostAsJsonAsync("api/getchitietdathanhtoan", khang);
                        string _json = ok.Result.Content.ReadAsStringAsync().Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                            Int32 from = _json.IndexOf("[");
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                            ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL> ListTraCuu = JsonConvert.DeserializeObject<ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL>>(result);
                            TienDien = ListTraCuu[0];
                            lblKyThanhToan.TextType = TextType.Html;
                            lblKyThanhToan.Text = "Quý khách hàng đã thanh toán thành công tiền điện tháng " + ListTraCuu[0].THANG + "/" +
                                ListTraCuu[0].NAM + " của mã khách hàng " + ListTraCuu[0].MA_KHANG + " - " + "<strong>" + ListTraCuu[0].TEN_KHANG +" </strong>";
                              
                        }
                        else
                        {

                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception)
            {


            }
            
            BindingContext = TienDien;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://sinvoice.viettel.vn/tracuuhoadon");
            }
            catch (Exception)
            {

               
            }
        }
    }
}