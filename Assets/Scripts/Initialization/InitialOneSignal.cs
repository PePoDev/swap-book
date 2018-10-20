using UnityEngine.UI;
using UnityEngine;

public class InitialOneSignal : MonoBehaviour {

	public static Text testOneSignal;

	void Start() {
		// OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);

		testOneSignal = GameObject.Find("Text:Log").GetComponent<Text>();

		OneSignal.StartInit("ca066e57-a54e-4704-9020-7aecdcd219f0")
		  .HandleNotificationOpened(HandleNotificationOpened)
		  .EndInit();

		OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
	}

	// Gets called when the player opens the notification.
	private static void HandleNotificationOpened(OSNotificationOpenedResult result) {
		testOneSignal.text = "Eiei";
	}
}
