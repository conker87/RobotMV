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
	public Transform ControlsGUI, GraphicsGUI, SoundGUI;

	[Header("Main Controls")]
	public Button ControlsMenu;
	public Button GraphicsMenu;
	public Button SoundsMenu;

	public Button ResumeButton, QuitToMainMenuButton, QuitToDesktopButton;

	[Header("Control Controls")]
	public Button SaveControls;
	public Button RevertControls;
	public Toggle ControllerConfigToggle;

	[SerializeField]
	PauseState pause = PauseState.NONE, changingStates;

	bool canUnpause = true;

	protected bool isCurrentlyPaused = false;

	void Start() {

		// ControllerConfigToggle.isOn = InputManager.Current.isUsingController;

		PauseGUI.gameObject.SetActive (false);
		ControlsGUI.gameObject.SetActive (false);
		GraphicsGUI.gameObject.SetActive (false);
		SoundGUI.gameObject.SetActive (false);

		ResumeButton.onClick.AddListener(	delegate() { SetState (PauseState.NONE, out changingStates);		});

		ControlsMenu.onClick.AddListener(	delegate() { SetState (PauseState.CONTROL, out changingStates);		});
		GraphicsMenu.onClick.AddListener(	delegate() { SetState (PauseState.GRAPHICS, out changingStates);	});
		SoundsMenu.onClick.AddListener	(	delegate() { SetState (PauseState.SOUND, out changingStates);		});

	}

	public void SetState(PauseState state, out PauseState outNewStat) {

		if (pause == state) {

			pause = PauseState.MAIN;
			outNewStat = pause;

			return;

		}

		pause = PauseState.CHANGING_STATE;
		outNewStat = state;

	}

	void Update () {

		isCurrentlyPaused = (pause != PauseState.NONE) ? true : false;

		if (pause == PauseState.NONE) {

			PauseGUI.gameObject.SetActive (false);
			ControlsGUI.gameObject.SetActive (false);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.MAIN, out changingStates);

			}

			return;

		}

		if (pause == PauseState.MAIN) {

			PauseGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.NONE, out changingStates);

			}

			return;

		}

		if (pause == PauseState.CONTROL) {

			ControlsGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.MAIN, out changingStates);
				ControlsGUI.gameObject.SetActive (false);

			}

			return;

		}

		if (pause == PauseState.GRAPHICS) {

			GraphicsGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.MAIN, out changingStates);
				GraphicsGUI.gameObject.SetActive (false);

			}

			return;

		}

		if (pause == PauseState.SOUND) {

			SoundGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetState (PauseState.MAIN, out changingStates);
				SoundGUI.gameObject.SetActive (false);

			}

			return;

		}

		if (pause == PauseState.CHANGING_STATE) {

			pause = changingStates;

			ControlsGUI.gameObject.SetActive (false);
			GraphicsGUI.gameObject.SetActive (false);
			SoundGUI.gameObject.SetActive (false);

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

	public void QuitToMainMenu(string mainMenuScene) {

		UnityEngine.SceneManagement.SceneManager.LoadScene (mainMenuScene);

	}

	public void QuitToDesktop() {

		Application.Quit ();

	}

	public bool checkIfCurrentlyPaused() {

		return isCurrentlyPaused;

	}
}

public enum PauseState { NONE, MAIN, CONTROL, GRAPHICS, SOUND, CHANGING_STATE };