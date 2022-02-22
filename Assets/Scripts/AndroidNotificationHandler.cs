using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID


    private const string ChannelId = "notification_channel";

    public void ScheduleNotification(DateTime dateTime)
    {
        /* This is information to create a "Channel", even though we will only be having one you still need to create this so that it can be used
         * The information inside will be what is used to differentiate it from other channels. Then it must be Registered*/
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notificaiton Channel",
            Description = "Some random Desciption",
            Importance = Importance.Default
        };

        // Registering the information above as a channel
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);


        /* Create the actual notification that will be used in the Android Phone that will be seen by the Customer/Player */
        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged",
            Text = "Your energy has recharged, come back again!",
            SmallIcon = "default",
            LargeIcon = "icon_0",
            FireTime = dateTime
        };


        AndroidNotificationCenter.SendNotification(notification, ChannelId);

    }

#endif

}
