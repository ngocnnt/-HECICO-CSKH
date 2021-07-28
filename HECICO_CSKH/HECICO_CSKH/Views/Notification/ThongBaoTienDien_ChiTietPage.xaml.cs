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
    public partial class ThongBaoTienDien_ChiTietPage :  ContentPage
    {
        THONG_BAO_TIEN_DIEN_MODEL TienDien;
        public ThongBaoTienDien_ChiTietPage(THONG_BAO_TIEN_DIEN_MODEL tiendien)
        {
            InitializeComponent();
            try
            {
               
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        KhachHangModel khang = new KhachHangModel();
                        khang.MaKhang = Preferences.Get(Config.MaKhachHang, "");
                        khang.Token = Preferences.Get(Config.Token, "");
                        khang.Sdt = Preferences.Get(Config.PhoneNumber, "");
                        khang.HoaDonId = tiendien.HOADON_ID;
                        client.BaseAddress = new Uri(Config.Url);
                        var ok = client.PostAsJsonAsync("api/getchitiettiendien", khang);
                        string _json = ok.Result.Content.ReadAsStringAsync().Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                            Int32 from = _json.IndexOf("[");
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                            ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL> ListTraCuu = JsonConvert.DeserializeObject<ObservableCollection<THONG_BAO_TIEN_DIEN_MODEL>>(result);
                            TienDien = ListTraCuu[0];
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
             
            lblKyThanhToan.Text = "(Từ ngày " + string.Format("{0:dd/MM/yyyy}", TienDien.NGAY_DKY) + " đến ngày " + string.Format("{0:dd/MM/yyyy}", TienDien.NGAY_CKY);
            TienDien.TIEN_BANG_CHU ="Bằng chữ : " + Config.DocTien(Convert.ToDouble(TienDien.TONG_TIEN));
            BindingContext = TienDien;
        }
    }
}