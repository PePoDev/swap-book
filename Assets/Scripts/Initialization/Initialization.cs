using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour {

	private void Start() {
		// Show status bar
		Screen.fullScreen = false;
		
		// Skip login when user logged.
		if (PlayerPrefs.HasKey("token")) {
			SceneManager.LoadScene("MainPage");
		} else {

			// Detect language the user's operating system is running in for first time.
			if (PlayerPrefs.HasKey("language")) {
				if (Application.systemLanguage == SystemLanguage.Thai) {
					PlayerPrefs.SetString("language", "th");
				} else {
					PlayerPrefs.SetString("language", "en");
				}
			}
			SceneManager.LoadScene("Login");
		}

	}
}