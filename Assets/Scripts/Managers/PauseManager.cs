using System;
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

	public Transform KeyboardBindingsParent, ControllerBindingsParent;
	public GameObject BindingsPrefab;

	//public List<GameObject> BindingsList = new List<Keybinds> ();

	public List<GameObject> keybindingsPrefabList = new List<GameObject> ();

	[SerializeField]
	string currentlyChangingKeybindID = "";

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

		// Keyboard Keybindings Itteration
		foreach(KeyValuePair<string, Keybinds> k in InputManager.Current.GetKeybindings()) {
			
			if (k.Value.ignoreInSettings) {

				//continue;

			}

			string key = k.Key;

			// TODO: ControlSetKeyPrefab now has a component that stores the cache of these, use GetComponent<ControlSetKeybind>();
			GameObject buttonGameObject = Instantiate (BindingsPrefab, KeyboardBindingsParent) as GameObject;

			ControlSetKeybind CSK = buttonGameObject.GetComponent<ControlSetKeybind>();

			buttonGameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
			buttonGameObject.name = "KeybindingsButton_" + k.Key;

			CSK.KeyboardBind.onClick.AddListener (delegate() {
				ControlsMenu_KeyboardBindingOnClick (key);
			});

			if (k.Value.ControllerBinds != KeyCode.Break) {
				CSK.ControllerBind.onClick.AddListener (delegate() {
					ControlsMenu_KeyboardBindingOnClick (key);
				});
			}

			CSK.labelText.text = key;

			CSK.KeyboardBindLabel.text = k.Value.KeyboardBinds.ToString();
			CSK.ControllerBindLabel.text = k.Value.ControllerBinds.ToString();;

			keybindingsPrefabList.Add (buttonGameObject);

		}

	}

	void Update () {

		isCurrentlyPaused = (pause != PauseState.NONE) ? true : false;

		if (pause == PauseState.SETTING_CONTROLS) {

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetStateForce(PauseState.CONTROL);

			}

			// Listen for Keyboard input
			if (Input.anyKeyDown) {

				Dictionary<string, Keybinds> changableKeybindsDictionary = InputManager.Current.GetChangableKeybindings();

				foreach (KeyCode code in Enum.GetValues (typeof(KeyCode))) {
	
					if (Input.GetKeyDown (code)) {

						for (int i = 0; i < changableKeybindsDictionary.Count; i++) {
							
							//if (changableKeybindsDictionary[i].key_id == currentlyChangingKeybindID) {

								//changableKeybindsDictionary[i] = new Keybinds(code, false, currentlyChangingKeybindID);

								currentlyChangingKeybindID = "";

								SetStateForce (PauseState.CONTROL);

								return;

							//}
						}


						//InputManager.Current.publicKeyboardKeys.Add ( new Keybinds (code, false, currentlyChangingKeybindID) );

						currentlyChangingKeybindID = "";
						SetStateForce(PauseState.CONTROL);
	
					}
	
				}
	
			}

			return;

		}

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
		
	#region Controls Menu
	public void ControlsMenu_ToggleIsUsingController() {

		InputManager.Current.isUsingController = ControllerConfigToggle.isOn;

	}

	public void ControlsMenu_KeyboardBindingOnClick(string Key_ID) {

		SetStateForce(PauseState.SETTING_CONTROLS);

		currentlyChangingKeybindID = Key_ID;

		Debug.Log (Key_ID);

	}

	public void ControlsMenu_CancelChangedBindings() {

		InputManager.Current.RevertChangesToCurrent ();

	}

	public void ControlsMenu_SaveBindings() {

		// TODO: Itterate through InputManager.Current.KeyboardKeys && ControllerKeys, find matching keys from
		// 	this.PublicKeyboardKeys & publicControllerKeys (!! CREATE OWN LISTS IN THIS MANAGER !!), if they match
		//	then replace the dictionary entry.
		//InputManager.Current.SaveNewKeybindsListsToDictionary(changingKeyboardKeybindings, changingControllerKeybindings);
  
	}

	public void ControlsMenu_RevertToDefaultBindings() {

		InputManager.Current.RevertToDefaultBindings ();

	}
	#endregion

	public void QuitToMainMenu(string mainMenuScene) {

		UnityEngine.SceneManagement.SceneManager.LoadScene (mainMenuScene);

	}

	public void QuitToDesktop() {

		Application.Quit ();

	}
		
	public bool checkIfCurrentlyPaused() {

		return isCurrentlyPaused;

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

	public void SetStateForce(PauseState state) {

		if (pause == state) {

			pause = PauseState.MAIN;

			return;

		}

		pause = state;

	}
}

public enum PauseState { NONE, MAIN, CONTROL, SETTING_CONTROLS, GRAPHICS, SOUND, CHANGING_STATE };