using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour {

	private void Start () {
		if (PlayerPrefs.HasKey("token")) {
			SceneManager.LoadScene("MainPage");
		} else {
			SceneManager.LoadScene("Login");
		}
	}
}
