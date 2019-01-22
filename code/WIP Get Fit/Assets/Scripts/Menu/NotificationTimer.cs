using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NotificationTimer : MonoBehaviour {
    public UnityEngine.UI.InputField hours, minutes;
    public UnityEngine.UI.Toggle notificationToggle;

    public static int h, m;

    public void OnEnable() {
        hours.text = h.ToString(); minutes.text = m.ToString();

        if (!GameManager.instance.hasUnlockedNotifications) {
            notificationToggle.interactable = false;
            notificationToggle.GetComponentInChildren<UnityEngine.UI.Text>().text = "[Gesperrt]";
        } else {
            notificationToggle.interactable = true;
            notificationToggle.GetComponentInChildren<UnityEngine.UI.Text>().text = "Aktiv?";
        }

    }

    public void OnToggle() {
        if (GameManager.instance.hasUnlockedNotifications) {
            if (notificationToggle.isOn) {
                int _h = int.Parse(hours.text); int _m = int.Parse(minutes.text);
                h = _h; m = _m;

                DateTime dt = DateTime.Now.Date.AddDays(1);
                TimeSpan ts = new TimeSpan(_h, _m, 0);
                dt = dt.Date + ts;

                TimeSpan delay = dt - DateTime.Now;
                int delayInMin = (int)delay.TotalMinutes;

                SetupLocalNotifications(delayInMin, 7);
            } else {
                NativeToolkit.ClearAllLocalNotifications();
            }
        }
    }

    public void SetupLocalNotifications(int delayInMin, int days) {
        for (int i = 0; i < days; i++) {
            NativeToolkit.ScheduleLocalNotification("GET FIT", "Wie wär's mit einem Workout?", 0, delayInMin + (1440 * i),
                                                "default_sound", true, "ic_notification", "ic_notification_large");
        }
    }

    public static void SetupLocalNotificationsOnReboot(int h, int m, int days) {
        NativeToolkit.ClearAllLocalNotifications();

        DateTime dt = DateTime.Now.Date.AddDays(1);
        TimeSpan ts = new TimeSpan(h, m, 0);
        dt = dt.Date + ts;

        TimeSpan delay = dt - DateTime.Now;
        int delayInMin = (int)delay.TotalMinutes;

        for (int i = 0; i < days; i++) {
            NativeToolkit.ScheduleLocalNotification("GET FIT", "Hi, wie wär's mit einem Workout?", 0, delayInMin + (1440 * i),
                                                "default_sound", true, "ic_notification", "ic_notification_large");
        }
    }

}
