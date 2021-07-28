using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Gms.Common;
using Plugin.CurrentActivity;
using Qiu.Niorgai;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Plugin.FirebasePushNotification;
using Android.Content;

namespace HECICO_CSKH.Droid
{
    [Activity(Label = "CSKH", Icon = "@mipmap/logo", Theme = "@style/MainTheme.Base", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            Forms.SetFlags(new string[] { "Brush_Experimental", "SwipeView_Experimental", "CarouseView_Experimental", "IndicatorView_Experimental" });
           
            base.OnCreate(savedInstanceState);
          
            Rg.Plugins.Popup.Popup.Init(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
         
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            //{
            //    // for covering the full screen in android..
            //   // Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);

            //    // clear FLAG_TRANSLUCENT_STATUS flag:
            //    Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            //    // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
            //    Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            //    Window.DecorView.SystemUiVisibility = 0;

            //    Window.SetStatusBarColor(Android.Graphics.Color.Transparent);

            //}
            //StatusBarCompat.TranslucentStatusBar(CrossCurrentActivity.Current.Activity);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            IsPlayServicesAvailable();
            CreateNotificationChannel();
           // string id = FirebaseInstanceId.Instance.Token;
            LoadApplication(new App());
           
            //FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
        
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                  GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                   // msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        
    }
}