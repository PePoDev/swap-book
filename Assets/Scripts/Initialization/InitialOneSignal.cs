using UnityEngine.UI;
using UnityEngine;

public class InitialOneSignal : MonoBehaviour {

	void Start() {
		OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.NONE, OneSignal.LOG_LEVEL.NONE);
		OneSignal.StartInit("ca066e57-a54e-4704-9020-7aecdcd219f0")
		  .HandleNotificationOpened(HandleNotificationOpened)
		  .EndInit();
		OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
	}

	private static void HandleNotificationOpened(OSNotificationOpenedResult result) {

	}
}
