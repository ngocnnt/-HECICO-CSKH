using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using CarouselView.FormsPlugin.iOS;
using Syncfusion.XForms.iOS.TextInputLayout;
using Plugin.FirebasePushNotification;
using UserNotifications;
using Firebase.CloudMessaging;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using Firebase.InstanceID;

namespace HECICO_CSKH.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate , IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags(new string[] { "CollectionView_Experimental", "Brush_Experimental", "SwipeView_Experimental", "CarouseView_Experimental", "IndicatorView_Experimental" });
            global::Xamarin.Forms.Forms.Init();         
            Rg.Plugins.Popup.Popup.Init();
            Core.Init();
            SfButtonRenderer.Init();
            Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfGradientViewRenderer.Init();
            CarouselViewRenderer.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            Syncfusion.XForms.iOS.BadgeView.SfBadgeViewRenderer.Init();
           
            LoadApplication(new App());
            //FirebasePushNotificationManager.Initialize(options, true);            
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            //RequestPushPermissionsAsync();
            _launchoptions = options;
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            //Firebase.CloudMessaging.Messaging.SharedInstance.Delegate = this;



            return base.FinishedLaunching(app, options);
        }

        //public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        //}

        //public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //{
        //    FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);

        //}
        //// To receive notifications in foregroung on iOS 9 and below.
        //// To receive notifications in background in any iOS version
        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        //{
        //    // If you are receiving a notification message while your app is in the background,
        //    // this callback will not be fired 'till the user taps on the notification launching the application.

        //    // If you disable method swizzling, you'll need to call this method. 
        //    // This lets FCM track message delivery and analytics, which is performed
        //    // automatically with method swizzling enabled.
        //    FirebasePushNotificationManager.DidReceiveMessage(userInfo);
        //    // Do your magic to handle the notification data
        //    System.Console.WriteLine(userInfo);

        //    completionHandler(UIBackgroundFetchResult.NewData);
        //}

        NSDictionary _launchoptions;
        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            if (_launchoptions != null && _launchoptions.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
            {
                var notfication = _launchoptions[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
                PresentNotification(notfication);
            }
            _launchoptions = null;
        }

        async Task RequestPushPermissionsAsync()
        {
            var requestResult = await UNUserNotificationCenter.Current.RequestAuthorizationAsync(
                 UNAuthorizationOptions.Alert
                | UNAuthorizationOptions.Badge
                | UNAuthorizationOptions.Sound);

            bool approved = requestResult.Item1;
            NSError error = requestResult.Item2;
            if (error == null)
            {
                if (!approved)
                {
                    Console.Write("Permission to receive notification was not granted");
                    return;
                }

                var currentSettings = await UNUserNotificationCenter.Current.GetNotificationSettingsAsync();
                if (currentSettings.AuthorizationStatus != UNAuthorizationStatus.Authorized)
                {
                    Console.WriteLine("Permissions were requested in the past but have been revoked (-Settings  app)");
                    return;
                }
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                Console.Write($"Error requesting permissions: {error}.");
            }
        }
        public override void RegisteredForRemoteNotifications(
UIApplication application, NSData deviceToken)
        {
            try
            {
                // Firebase.CloudMessaging.Messaging.SharedInstance.SetApnsToken(deviceToken, Firebase.CloudMessaging.ApnsTokenType.Sandbox);
               
                Debug.WriteLine($"RegisteredForRemoteNotifications token:'{deviceToken}'");
                var DeviceToken = deviceToken.Description;
                if (!string.IsNullOrWhiteSpace(DeviceToken))
                {
                    DeviceToken = DeviceToken.Trim('<').Trim('>');
                }

                // Get previous device token
                var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

                // Has the token changed?
                if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
                {
                    //TODO: Put your own logic here to notify your server that the device token has changed/been created!
                }

                // Save new device token
                NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
                //await SendRegistrationTokenToMainPRoject(deviceToken);
            }
            catch (Exception ex)
            {


            }
            // Get current device token

        }
        //public async override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    if (deviceToken == null)
        //    {
        //        return;
        //    }
        //    Console.WriteLine($"Token received: {deviceToken}");
        //    await SendRegistrationTokenToMainPRoject(deviceToken);
        //}

        async Task SendRegistrationTokenToMainPRoject(NSData deviceToken)
        {
            MessagingCenter.Send<object, string>(this, "fcmtoken", deviceToken.ToString());
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Console.WriteLine($"Failed to register for remote notifications: {error.Description}");
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo,
            Action<UIBackgroundFetchResult> completionHandler)
        {
            PresentNotification(userInfo);
            completionHandler(UIBackgroundFetchResult.NoData);
            try
            {
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
                var message = (aps[new NSString("webContentList")] as NSString).ToString();
                // LoadApplication(new App("", message));
            }
            catch (Exception ex)
            {
                //LogInfo.ReportErrorInfo(ex.Message, ex.StackTrace, "AppDelegate-DidReceiveRemoteNotification");
            }
        }

        [Obsolete]

        void PresentNotification(NSDictionary userInfo)
        {
            try
            {
                NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

                var msg = string.Empty;

                if (aps.ContainsKey(new NSString("alert")))
                {
                    msg = (aps[new NSString("alert")] as NSString).ToString();
                }
                if (string.IsNullOrEmpty(msg))
                {
                    msg = "(unable to parse)";
                }
                // MessagingCenter.Send<object, string>(this, App.NotificationReceivedKey, msg);
            }
            catch (Exception ex)
            {


            }


        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public async void DidReceiveRegistrationToken(Firebase.CloudMessaging.Messaging messaging, string fcmToken)
        {
            messaging.ApnsToken = fcmToken;
            await SendRegistrationTokenToMainPRoject(fcmToken);
        }




    }
}
