using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using System.Collections.Generic;
using Android.Support.V4.App;

namespace HECICO_CSKH.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                
                Log.Debug(TAG, "From: " + message.From);
                if (message.Data != null)
                {
                    Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                    SendNotification(message.GetNotification().Body, message.Data, message.GetNotification().Title);
                }
            }
            catch (Exception ex)
            {

               
            }
              
          
           
        }
        void SendNotification(string messageBody, IDictionary<string, string> data,string tittle)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            var pendingIntent = PendingIntent.GetActivity(this,
                                                          MainActivity.NOTIFICATION_ID,
                                                          intent,
                                                          PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)

                                      .SetContentTitle(tittle)
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetPriority((int)NotificationPriority.High)
                                      .SetVibrate(new long[0])
                                      .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                                      .SetVisibility((int)NotificationVisibility.Public)
                                      .SetSmallIcon(Resource.Drawable.logo)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }

}