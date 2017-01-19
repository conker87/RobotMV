using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

	#region Singleton
	private static PauseManager _current;
	public static PauseManager Current {
		get {

			if (_current == null)
			{
				_current = GameObject.FindObjectOfType<PauseManager>();
			}

			return _current;

		}
	}

	void Awake() {

		DontDestroyOnLoad(gameObject);

	}
	#endregion

	public Transform PauseGUI;

	protected bool isCurrentlyPaused = false;

	// Update is called once per frame
	void Update () {

		// Time.timeScale = (PauseGUI.gameObject.activeSelf) ? 0f : 1f;

		if (InputManager.Current.GetButtonDown ("Pause")) {

			TogglePause ();

		}
		
	}

	void TogglePause() {

		if (PauseGUI != null) {

			PauseGUI.gameObject.SetActive (!PauseGUI.gameObject.activeSelf);

			isCurrentlyPaused = PauseGUI.gameObject.activeSelf;

		}

	}

	public bool checkIfCurrentlyPaused() {

		return isCurrentlyPaused;

	}
}
