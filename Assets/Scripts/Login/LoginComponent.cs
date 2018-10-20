using PePo.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginComponent : MonoBehaviour {
	public InputField IF_Username;
	public InputField IF_Password;

	public void Login() {
		var loginData = new LoginData() {
			email = IF_Username.text,
			password = IF_Password.text
		};

		StartCoroutine(LoginAPI(loginData, res => {
			print(res.downloadHandler.text);

			if (res.downloadHandler.text != null && res.responseCode == 200) {

				var userData = JsonUtility.FromJson<UserData>(res.downloadHandler.text);

				PlayerPrefs.SetString("token", userData.tokens.token);
				PlayerPrefs.SetString("email", userData.email);
				PlayerPrefs.SetString("username", userData.username);

				SceneManager.LoadScene("MainPage");
			}

		}));
	}

	private static IEnumerator LoginAPI(LoginData loginData, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{API_Config.URL()}/users/login", "POST");
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(loginData));
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
}
