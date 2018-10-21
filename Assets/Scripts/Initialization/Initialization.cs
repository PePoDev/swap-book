using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour {

	private void Start() {

		// Detect language the user's operating system is running in.
		if (PlayerPrefs.HasKey("language")) {
			if (Application.systemLanguage == SystemLanguage.Thai) {
				PlayerPrefs.SetString("language", "th");
			} else {
				PlayerPrefs.SetString("language", "en");
			}
		}

		// Skip login when user ever logged.
		if (PlayerPrefs.HasKey("token")) {
			SceneManager.LoadScene("MainPage");
		} else {
			SceneManager.LoadScene("Login");
		}

	}
}
