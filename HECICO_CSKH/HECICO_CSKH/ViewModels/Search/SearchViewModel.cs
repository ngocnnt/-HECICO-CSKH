using HECICO_CSKH.Global;
using HECICO_CSKH.Models;
using HECICO_CSKH.Models.Notification;
using HECICO_CSKH.Views.Notification;
using HECICO_CSKH.Views.Search;
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

namespace HECICO_CSKH.ViewModels.Search
{
  public   class SearchViewModel : BaseViewModel 
    {
        #region "Field"       
        public INavigation Navigation;
        #endregion

        #region "Contructor"

        public SearchViewModel() 
        {
            TraCuuDiemThuTienDienCommand = new Command(OnTraCuuDiemThuTienDienClick);
            TraCuuDienNangTieuThuCommand = new Command(OnTraCuuDienNangTieuThuClicked);
            TraCuuGhiChiSoCommand = new Command(OnTraCuuGhiChiSoClicked);
            TraCuuHoaDonDienTuCommand = new Command(OnTraCuuHoaDonDienTuClick);
            TraCuuLichTamNgugnCapDienCommand = new Command(OnTraCuuLichTamNgugnCapDienClicked);
            TraCuuThongTinThanhToanCommand = new Command(OnTraCuuThongTinThanhToanClicked);
            OpenWebCommand = new Command(async () =>
            {
                using (HttpClient client = new HttpClient ())
                {
                    try
                    {
                        client.BaseAddress = new Uri(Config.Url);

                        string _json = client.GetStringAsync("api/getlinkcskh").Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {           
                            await Browser.OpenAsync(_json.Replace("\"", ""));
                        }
                    }
                    catch (Exception ex)
                    {

                       
                    }
                    
                }    
               
            });
            
        }



        #endregion

        #region "Method"

        private async void OnTraCuuThongTinThanhToanClicked(object obj)
        {
            await Navigation.PushAsync(new TraCuuThongTinThanhToanPage());
        }

        private void OnTraCuuLichTamNgugnCapDienClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private async void OnTraCuuHoaDonDienTuClick(object obj)
        {
            await Navigation.PushAsync(new TraCuuHoaDonDienTuPage());
        }

        private async void OnTraCuuGhiChiSoClicked(object obj)
        {
           await Navigation.PushAsync(new TraCuuLichGhiChiSoPage());
        }

        private async void OnTraCuuDienNangTieuThuClicked(object obj)
        {
            await Navigation.PushAsync(new TraCuuDienNangTieuThuPage());
        }

        private async  void OnTraCuuDiemThuTienDienClick(object obj)
        {
            await Navigation.PushAsync(new TraCuuDiemThuTienDienPage());
        }
        #endregion

        #region "Command"

        public Command TraCuuGhiChiSoCommand { get; }
        public Command TraCuuDiemThuTienDienCommand { get; }
        public Command TraCuuLichTamNgugnCapDienCommand { get; }
        public Command TraCuuDienNangTieuThuCommand { get; }
        public Command TraCuuThongTinThanhToanCommand { get; }
        public Command TraCuuHoaDonDienTuCommand { get; }
        public Command OpenWebCommand { get; }
        #endregion
    }
}
