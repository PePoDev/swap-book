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
	public InputField Telephone;
	public InputField Password;
	public InputField RePassword;

	private SignupData signupData;

	public void Signup() {
		var data = new SignupData(){
			username = Username.text,
			name = Fullname.text,
			email = Email.text,
			password = Password.text,
			telephone = Telephone.text
		};
		if (Password.text == RePassword.text) {
			StartCoroutine(SignupAPI(data, res => {
				if (res.responseCode == 200) {
					AndroidNativeFunctions.ShowAlert("We will send email to you, please confirm",
						"Please follow instructer to reset your password",
						"Ok", "", "",
						(action) => { });
					GameObject.Find("UIManager").GetComponent<LoginUIManager>().AnimateSigninGroup();
				} else if (res.responseCode == 400) {
					AndroidNativeFunctions.ShowAlert("We will send email to you, please confirm",
						"Please follow instructer to reset your password",
						"Ok", "", "",
						(action) => { });
				}
			}));
		}
	}

	private static IEnumerator SignupAPI(SignupData data, Action<UnityWebRequest> callBack) {
		var request = new UnityWebRequest($"{Config.URL()}/users", "POST");
		var bodyRaw = new UTF8Encoding().GetBytes(JsonUtility.ToJson(data));
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		yield return request.SendWebRequest();
		callBack(request);
	}
}
