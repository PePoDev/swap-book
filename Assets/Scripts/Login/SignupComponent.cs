using PePo.Utilities;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SignupComponent : MonoBehaviour {
	public InputField Username;
	public InputField Fullname;
	public InputField Email;
	public InputField Password;
	public InputField RePassword;

	private SignupData signupData;

	public void Signup() {

		AndroidNativeFunctions.ShowAlert("(DEMO No email sent)Confirm your sign up",
					"Activate your account by click activate link in your email",
					"Got it", "", "",
					(action) => {
						print("Hello adb !");
					}
		);

		if (Password.text != RePassword.text) {
			
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
