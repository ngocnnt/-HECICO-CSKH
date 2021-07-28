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
using Google.Android.Material.AppBar;
using HECICO_CSKH.Droid.Renderer;
using HECICO_CSKH.Interface;
using Plugin.CurrentActivity;
using Qiu.Niorgai;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarRenderer))]
namespace HECICO_CSKH.Droid.Renderer
{
    public class StatusBarRenderer : IStatusBar
    {
        public void translucentStatusBar()
        {
            //CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            //StatusBarCompat.TranslucentStatusBar(CrossCurrentActivity.Current.Activity);
            //StatusBarCompat.SetStatusBarColor(CrossCurrentActivity.Current.Activity, 10);
            StatusBarCompat.TranslucentStatusBar(CrossCurrentActivity.Current.Activity);
            //StatusBarCompat.ChangeToLightStatusBar(CrossCurrentActivity.Current.Activity);
        }
    }
}