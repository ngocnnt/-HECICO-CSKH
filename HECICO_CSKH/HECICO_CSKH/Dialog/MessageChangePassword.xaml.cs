using HECICO_CSKH.Global;
using HECICO_CSKH.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HECICO_CSKH.Dialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageChangePassword : PopupPage
    {
        TaskCompletionSource<DialogReturn> _tsk = null;
        public MessageChangePassword()
        {
            InitializeComponent();
        }
        private async void btnOK_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(oldPass.Text))
            {
                await new MessageBox("Nhập mật khẩu cũ").Show();
                return;
            }
            if (string.IsNullOrEmpty(newPass.Text))
            {
                await new MessageBox("Nhập mật khẩu mới").Show();
                return;
            }
            if (string.IsNullOrEmpty(newPass1.Text))
            {
                await new MessageBox("Nhập lại mật khẩu mới").Show();
                return;
            }
            if (newPass.Text != newPass1.Text)
            {
                await new MessageBox("Mật khẩu mới nhập lại không trùng nhau").Show();
                return;
            }

            ChangePasswordModel change = new ChangePasswordModel { Username  = Preferences.Get(Config.PhoneNumber, ""), OldPassword = oldPass.Text, NewPassword = newPass.Text };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Config.Url);
                var ok = client.PostAsJsonAsync("api/changepass", change);
                if (ok.Result.Content.ReadAsStringAsync().Result.ToLower().Contains("true"))
                {
                    await new MessageBox("Đổi mật khẩu thành công").Show();
                    await Navigation.PopPopupAsync(true);
                    _tsk.SetResult(DialogReturn.OK);
                }
                else
                {
                    await new MessageBox(ok.Result.Content.ReadAsStringAsync().Result.Replace("\"", "")).Show();
                }
            }

        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
            _tsk.SetResult(DialogReturn.Cancel);
        }
        public async Task<DialogReturn> Show()
        {
            _tsk = new TaskCompletionSource<DialogReturn>();
            await Navigation.PushPopupAsync(this);
            return await _tsk.Task;
        }
    }
}