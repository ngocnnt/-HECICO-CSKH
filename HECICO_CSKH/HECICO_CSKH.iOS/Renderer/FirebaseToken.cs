using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using HECICO_CSKH.Interface;
using HECICO_CSKH.iOS.Renderer;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseToken))]
namespace HECICO_CSKH.iOS.Renderer
{
  public  class FirebaseToken : IFirebase
    {
        public string getFirebaseToken()
        {
            return "";
        }
    }
}