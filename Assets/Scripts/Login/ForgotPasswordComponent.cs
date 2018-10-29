using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using PePo.Utilities;
using UnityEngine.UI;

public class ForgotPasswordComponent : MonoBehaviour {
	public InputField IF_Email;

	public void ForgotPassword() {

		AndroidNativeFunctions.ShowAlert("(DEMO No email sent)Email has been sent",
					"Please activate this action in your email to reset your password",
					"Got it", "", "",
					(action) => {
						print("Hello adb !");
					}
		);

		return;

		if (IF_Email.text != null) {
			AndroidNativeFunctions.ShowAlert("We will send email to you, please confirm",
					"Please follow instructer to reset your password",
					"Ok","","",
					(action) => {
						if (action.Equals(DialogInterface.Positive)) {
							StartCoroutine(LoginAPI(IF_Email.text, res => {
								if (res.downloadHandler.text != null && (res.responseCode == 200 || res.responseCode == 201)) {

								}
							}));
						}
					}
			);
		}
	}
	private static IEnumerator LoginAPI(string email, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{API_Config.URL()}/users/forgot", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes($"\"email\":\"{email}\"");
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
}
