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
		var loginData = new LoginData() {
			login = IF_Username.text,
			password = IF_Password.text
		};

		StartCoroutine(LoginAPI(loginData, res => {
			if (res.downloadHandler.text != null && res.responseCode == 200) {

				var userData = JsonUtility.FromJson<UserData>(res.downloadHandler.text);
				
				PlayerPrefs.SetString("token", res.GetResponseHeaders()["x-auth"]);
				PlayerPrefs.SetString("user_id", userData.user_id);
				PlayerPrefs.SetString("telephone", userData.telephone);
				PlayerPrefs.SetString("name", userData.name);
				PlayerPrefs.SetString("email", userData.email);
				PlayerPrefs.SetString("username", userData.username);

				OneSignal.SetEmail(userData.email);

				SceneManager.LoadScene("MainPage");
			} else if (res.responseCode == 400) {
				AndroidNativeFunctions.ShowAlert("Check your email or password and try again . . .",
						"Error !",
						"Ok", "", "",
						(action) => { });
			}
		}));
	}

	private static IEnumerator LoginAPI(LoginData loginData, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/users/login", "POST");
		request.uploadHandler = new UploadHandlerRaw(new UTF8Encoding().GetBytes(JsonUtility.ToJson(loginData)));
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		request.SendWebRequest();
		AndroidNativeFunctions.ShowProgressDialog($"Loading . . .");
		while (!request.isDone) {
			yield return null;
		}
		AndroidNativeFunctions.HideProgressDialog();

		callBack(request);
	}
}
