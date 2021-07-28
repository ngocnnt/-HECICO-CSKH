using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HECICO_CSKH.Droid.Renderer;
using HECICO_CSKH.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProcessLoading))]
namespace HECICO_CSKH.Droid.Renderer
{
    public class ProcessLoading : IProcessLoader
    {
        public void Hide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndroidHUD.AndHUD.Shared.Dismiss();
            });
          
        }
        public void Show(string title = "Loading")
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndroidHUD.AndHUD.Shared.Show(Forms.Context, status: title, maskType: AndroidHUD.MaskType.Black);
            });

        }
    }
}