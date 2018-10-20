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
		StartCoroutine(LoginAPI(IF_Email.text, res => {
			if (res.downloadHandler.text != null && res.responseCode == 200 && res.responseCode == 201) {
				
			}
		}));
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
