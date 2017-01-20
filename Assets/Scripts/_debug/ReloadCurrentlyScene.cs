using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ReloadCurrentlyScene : MonoBehaviour {

	void Update () {

		if (InputManager.Current.GetButtonDown ("DEBUG_ResetScene")) {

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		}

	}
}
