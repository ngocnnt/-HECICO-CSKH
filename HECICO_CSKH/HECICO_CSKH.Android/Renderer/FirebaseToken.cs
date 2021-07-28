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
using Firebase.Iid;
using HECICO_CSKH.Droid.Renderer;
using HECICO_CSKH.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseToken))]
namespace HECICO_CSKH.Droid.Renderer
{
    public class FirebaseToken : IFirebase
    {
        public string getFirebaseToken()
        {
            return FirebaseInstanceId.Instance.Token;
        }

        
    }
}