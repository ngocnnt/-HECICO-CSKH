using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigTed;
using Foundation;
using HECICO_CSKH.Interface;
using HECICO_CSKH.iOS.Renderer;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(ProcessLoading))]
namespace HECICO_CSKH.iOS.Renderer
{
    public class ProcessLoading : IProcessLoader
    {
        public void Hide()
        {
            BTProgressHUD.Dismiss();
        }

        public void Show(string title = "Loading")
        {
            BTProgressHUD.Show(title, maskType: ProgressHUD.MaskType.Black);
        }
    }
}