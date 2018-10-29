using PePo.Utilities;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginComponent : MonoBehaviour {
	public InputField IF_Username;
	public InputField IF_Password;

	public void Login() {

		AndroidNativeFunctions.ShowAlert("This is demo app to test UI and visual design, Hope you fun ^^ contact Pepo if you have any problem.",
					"Notice",
					"OK", "", "",
					(action) => {
						print("Hello adb !");
					}
		);

		SceneManager.LoadScene("MainPage");

		return;

		var loginData = new LoginData() {
			email = IF_Username.text,
			password = IF_Password.text
		};

		StartCoroutine(LoginAPI(loginData, res => {
			if (res.downloadHandler.text != null && res.responseCode == 200) {

				AndroidNativeFunctions.HideProgressDialog();

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
		request.uploadHandler = new UploadHandlerRaw(new UTF8Encoding().GetBytes(JsonUtility.ToJson(loginData)));
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		request.SendWebRequest();

		while (!request.isDone) {
			AndroidNativeFunctions.ShowProgressDialog($"Loging {(request.downloadProgress * 100).ToString("D")}%");
			yield return null;
		}

		callBack(request);
	}
}
