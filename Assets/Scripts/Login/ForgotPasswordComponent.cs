using PePo.Utilities;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ForgotPasswordComponent : MonoBehaviour {
	public InputField IF_Email;

	public void ForgotPassword() {
		if (IF_Email.text != null) {
			StartCoroutine(ForgetAPI(IF_Email.text, res => {
				if (res.responseCode == 200) {
					AndroidNativeFunctions.ShowAlert("We will send email to you, please confirm",
						"Please follow instructer to reset your password",
						"Ok", "", "",
						(action) => { });
				} else if (res.responseCode == 400 || res.downloadHandler.text == null) {
					AndroidNativeFunctions.ShowAlert("Check your email and try again . . .",
						"ERROR !",
						"Ok", "", "",
						(action) => { });
				}
			}));
		}
	}
private static IEnumerator ForgetAPI(string email, Action<UnityWebRequest> callBack) {
	var request = new UnityWebRequest($"{Config.URL()}/users/forgot", "POST");
	var bodyRaw = new UTF8Encoding().GetBytes($"{{\"email\":\"{email}\"}}");
	request.uploadHandler = new UploadHandlerRaw(bodyRaw);
	request.downloadHandler = new DownloadHandlerBuffer();
	request.SetRequestHeader("Content-Type", "application/json");
	yield return request.SendWebRequest();
	callBack(request);
}
}
