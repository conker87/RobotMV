using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	[Header("GUI Parents")]
	public Transform PauseGUI;
	public Transform ControlsGUI;

	[Header("Main Controls")]
	public Button ControlsMenu;

	[Header("Control Controls")]
	public Button SaveControls;
	public Button CancelControls, RevertControls;
	public Toggle ControllerConfigToggle;

	[SerializeField]
	PauseState pause = PauseState.NONE;

	bool canUnpause = true;

	protected bool isCurrentlyPaused = false;

	void Start() {

		ControllerConfigToggle.isOn = InputManager.Current.isUsingController;

		PauseGUI.gameObject.SetActive (false);
		ControlsGUI.gameObject.SetActive (false);

		ControlsMenu.onClick.AddListener(delegate() { SetState (PauseState.CONTROL); });

	}

	public void SetState(PauseState state) {

		pause = state;

	}

	void Update () {

		isCurrentlyPaused = (pause != PauseState.NONE) ? true : false;

		if (pause == PauseState.NONE) {

			PauseGUI.gameObject.SetActive (false);
			ControlsGUI.gameObject.SetActive (false);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.MAIN);

			}

			return;

		}

		if (pause == PauseState.MAIN) {

			PauseGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.NONE);

			}

			return;

		}

		if (pause == PauseState.CONTROL) {

			ControlsGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.MAIN);
				ControlsGUI.gameObject.SetActive (false);

			}

			return;

		}


		
	}

	void ToggleMainPause(PauseState newState) {

		if (PauseGUI != null) {

			PauseGUI.gameObject.SetActive (!PauseGUI.gameObject.activeSelf);

			pause = newState;

			isCurrentlyPaused = PauseGUI.gameObject.activeSelf;

		}

	}

	public void ToggleControls() {

		ControlsGUI.gameObject.SetActive (!ControlsGUI.gameObject.activeSelf);

		canUnpause = !ControlsGUI.gameObject.activeSelf;

	}

	public void ToggleIsUsingController() {

		InputManager.Current.isUsingController = ControllerConfigToggle.isOn;

	}

	public bool checkIfCurrentlyPaused() {

		return isCurrentlyPaused;

	}
}

public enum PauseState { NONE, MAIN, CONTROL, GRAPHICS, SOUND };