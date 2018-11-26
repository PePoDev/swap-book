using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class InitialOneSignal : MonoBehaviour {

	void Start() {
		OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.NONE, OneSignal.LOG_LEVEL.NONE);
		OneSignal.StartInit("ca066e57-a54e-4704-9020-7aecdcd219f0")
		  .HandleNotificationOpened(HandleNotificationOpened)
		  .EndInit();
		OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
		DontDestroyOnLoad(gameObject);
	}

	private static void HandleNotificationOpened(OSNotificationOpenedResult result) {
		OSNotificationPayload payload = result.notification.payload;
		Dictionary<string, object> additionalData = payload.additionalData;
		string message = payload.body;

		print("GameControllerExample:HandleNotificationOpened: " + message);

		if (additionalData != null) {
			if (additionalData.ContainsKey("notification")) {
				if (additionalData["notification"].Equals("offer")) {
					Application.Quit();
				} else if (additionalData["notification"].Equals("confirm")) {
					Application.Quit();
				}
			}
		}
	}
}
