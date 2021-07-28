using HECICO_CSKH.Dialog;
using HECICO_CSKH.Global;
using HECICO_CSKH.Models;
using HECICO_CSKH.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using HECICO_CSKH.Interface;
using Plugin.DeviceInfo;

namespace HECICO_CSKH.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region "Field" 
        bool _canLogin;
        LoginModel loginModel;
        String _Username;
        string _Password;
        #endregion

        #region "Contructor"       
        public string UserName { get => _Username; set => SetProperty(ref _Username, value); }
        public bool  CanLogin { get => _canLogin ; set => SetProperty(ref _canLogin, value); }
        public string Password { get => _Password ; set => SetProperty(ref _Password , value); }
        public LoginModel LoginModels { get => loginModel; set => SetProperty(ref loginModel, value); }
        public LoginViewModel()
        {
            LoginModels = new LoginModel();
            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            this.PropertyChanged +=
                                  (_, __) => LoginCommand.ChangeCanExecute();
        }
        #endregion

        #region "Command" 
        public Command LoginCommand { get; }
        #endregion

        #region "Function" 
        private bool ValidateLogin()
        {
           
           CanLogin = !String.IsNullOrWhiteSpace(_Username )
                 && !String.IsNullOrWhiteSpace(_Password );
            return CanLogin;
        }
        private async void OnLoginClicked()
        {
            try
            {
                if (!CheckInternet())
                {
                    return;
                }
                try
                {
                    //App.Current.MainPage = new AppShell();
                   // return;
                    ShowLoading("Đang kiểm tra");
                    await Task.Delay(1000);
                    LoginModel login = new LoginModel();
                    login.Username = _Username;
                    login.Password = _Password;
                    login.LoaiThietBi = Device.RuntimePlatform;                       
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Config.Url);
                       // client.Timeout = new TimeSpan(5000);
                        var ok = client.PostAsJsonAsync("api/login" , login );
                        HideLoading();
                        string _json = ok.Result.Content.ReadAsStringAsync().Result;
                        _json = _json.Replace("\\r\\n", "").Replace("\\", "");
                        if (_json.Contains("error") == false && _json.Contains("[]") == false)
                        {
                           
                            Int32 from = _json.IndexOf("["); 
                            Int32 to = _json.IndexOf("]");
                            string result = _json.Substring(from, to - from + 1);
                           ObservableCollection<LoginModel > Tokens = JsonConvert.DeserializeObject<ObservableCollection<LoginModel>>(result);
                            if (Tokens.Count >0)
                            {
                                Preferences.Set(Config.MaKhachHang, Tokens [0].MA_KHANG );
                                Preferences.Set(Config.PhoneNumber, _Username);
                                Preferences.Set(Config.Token, Tokens[0].TOKEN );
                                //dang ki token trong firebase de nhan pushnotification
                                //tuấn tạm đóng lai
                                CreateTokenFirebaseModel tokenFB = new CreateTokenFirebaseModel();
                                tokenFB.Sdt = _Username;
                                tokenFB.Token = DependencyService.Get<IFirebase>().getFirebaseToken();
                                await client.PostAsJsonAsync("api/createtokenfirebase", tokenFB);
                                App.Current.MainPage = new AppShell();
                            }    
                            
                        }    
                        else
                        {
                            await new MessageBox("Thông tin đăng nhập không chính xác").Show ();
                            return;
                        }    
                    }


                }
                catch (Exception ex)
                {
                    HideLoading();
                    await new MessageBox(ex.Message ).Show();
                    return;
                }
            }
            catch (Exception)
            {

                HideLoading();
            }
          

        }
       
        #endregion
    }
}
  