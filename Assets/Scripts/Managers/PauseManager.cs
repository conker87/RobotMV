using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	#region Singleton

	public static PauseManager Current;

	void Awake() {
		
		//Check if instance already exists
		if (Current == null) {
			//if not, set instance to this
			Current = this;

		//If instance already exists and it's not this:
		} else if (Current != this) {
			
			Destroy(gameObject);
		
		}  

		//Sets this to not be destroyed when reloading scene
		// DontDestroyOnLoad(gameObject);

	}

	#endregion

	[Header("GUI Parents")]
	public Transform PauseGUI;
	public Transform ControlsGUI, GraphicsGUI, SoundGUI;

	[Header("Main Controls")]
	public Button ControlsMenu;
	public Button GraphicsMenu;
	public Button SoundsMenu;

	public Button QuitToMainMenuButton;

	[Header("Control Controls")]
	public Button SaveControls;
	public Button RevertControls;
	public Toggle ControllerConfigToggle;

	public Transform BindingListsParent;
	public GameObject BindingsPrefab;

	//public List<GameObject> BindingsList = new List<Keybinds> ();

	public List<GameObject> keybindingsPrefabList = new List<GameObject> ();

	[SerializeField]
	string currentlyChangingKeybindID = "";
	bool isControllerKeybind = false;

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

		QuitToMainMenuButton.onClick.AddListener(	delegate() { QuitToMainMenu("MainMenu");		});

		ControlsMenu.onClick.AddListener(	delegate() { SetStateForce (PauseState.CONTROL);	});
		GraphicsMenu.onClick.AddListener(	delegate() { SetStateForce (PauseState.GRAPHICS);	});
		SoundsMenu.onClick.AddListener	(	delegate() { SetStateForce (PauseState.SOUND);		});

		// Keyboard Keybindings Itteration
		foreach(KeyValuePair<string, Keybinds> k in InputManager.Current.GetKeybindings()) {
			
			if (k.Value.ignoreInSettings) {

				//continue;

			}

			string key = k.Key;

			// TODO: ControlSetKeyPrefab now has a component that stores the cache of these, use GetComponent<ControlSetKeybind>();
			GameObject buttonGameObject = Instantiate (BindingsPrefab, BindingListsParent) as GameObject;

			ControlSetKeybind CSK = buttonGameObject.GetComponent<ControlSetKeybind>();

			buttonGameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
			buttonGameObject.name = "KeybindingsButton_" + k.Key;

			CSK.KeyboardBind.onClick.AddListener (delegate() {
				ControlsMenu_KeyboardBindingOnClick (key, false);
			});

			if (k.Value.ControllerBinds != KeyCode.Break) {
				CSK.ControllerBind.onClick.AddListener (delegate() {
					ControlsMenu_KeyboardBindingOnClick (key, true);
				});
			}

			CSK.labelText.text = key;

			CSK.KeyboardBindLabel.text = k.Value.KeyboardBinds.ToString();
			CSK.ControllerBindLabel.text = k.Value.ControllerBinds.ToString();;

			keybindingsPrefabList.Add (buttonGameObject);

		}

		// 

	}

	void Update () {

		if (pause == PauseState.SETTING_CONTROLS) {

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				SetStateForce(PauseState.CONTROL);
				return;

			}

			// Listen for Keyboard input
			if (Input.anyKeyDown) {

				Dictionary<string, Keybinds> changableKeybindsDictionary = InputManager.Current.GetChangableKeybindings();

				foreach (KeyCode code in Enum.GetValues (typeof(KeyCode))) {
	
					if (Input.GetKeyDown (code)) {
						
						int index = GetIndexOfPrefabList (currentlyChangingKeybindID);
						ControlSetKeybind CSK = keybindingsPrefabList [index].GetComponent<ControlSetKeybind> ();

						if (isControllerKeybind) {

							// TODO: This requires further validation to prevent none JoystickButtons# from being used.

							Keybinds newBind = new Keybinds (InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID].key_id,
																InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID].KeyboardBinds,
																code,
																InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID].ignoreInSettings);

							InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID] = newBind;

							CSK.ControllerBindLabel.text = code.ToString();

						} else {

							// TODO: This required validation to prevent Keycode.Break being used as this is our reserved Keycode.

							Keybinds newBind = new Keybinds (InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID].key_id,
																code,
																InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID].ControllerBinds,
																InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID].ignoreInSettings);

							InputManager.Current.ChangingKeybindings [currentlyChangingKeybindID] = newBind;

							CSK.KeyboardBindLabel.text = code.ToString();

						}

						currentlyChangingKeybindID = "";
						SetStateForce(PauseState.CONTROL);
	
					}
	
				}
	
			}

			return;

		}

		isCurrentlyPaused = (pause != PauseState.NONE) ? true : false;

		if (pause == PauseState.NONE) {

			PauseGUI.gameObject.SetActive (false);
			ControlsGUI.gameObject.SetActive (false);
			GraphicsGUI.gameObject.SetActive (false);
			SoundGUI.gameObject.SetActive (false);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				pause = PauseState.MAIN;

			}

			return;

		}

		if (pause == PauseState.MAIN) {

			PauseGUI.gameObject.SetActive (true);
			ControlsGUI.gameObject.SetActive (false);
			GraphicsGUI.gameObject.SetActive (false);
			SoundGUI.gameObject.SetActive (false);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				pause = PauseState.NONE;

			}

			return;

		}

		if (pause == PauseState.CONTROL) {

			ControlsGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				pause = PauseState.MAIN;

			}

			return;

		}

		if (pause == PauseState.GRAPHICS) {

			GraphicsGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				pause = PauseState.MAIN;

			}

			return;

		}

		if (pause == PauseState.SOUND) {

			SoundGUI.gameObject.SetActive (true);

			if (InputManager.Current.GetButtonDown ("Pause") || InputManager.Current.GetButtonDown ("UIBack")) {

				pause = PauseState.MAIN;

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

	int GetIndexOfPrefabList(string Key_ID) {

		for (int i = 0; i < keybindingsPrefabList.Count; i++) {

			if (keybindingsPrefabList[i].GetComponent<ControlSetKeybind>().labelText.text == Key_ID) {

				return i;

			}

		}

		return -1;


	}

	#region Controls Menu

	public void ControlsMenu_ToggleIsUsingController() {

		InputManager.Current.isUsingController = ControllerConfigToggle.isOn;

	}

	public void ControlsMenu_KeyboardBindingOnClick(string Key_ID, bool isController) {

		SetStateForce(PauseState.SETTING_CONTROLS);

		currentlyChangingKeybindID = Key_ID;
		isControllerKeybind = isController;

		Debug.Log (Key_ID + " : " + isController);

	}

	public void ControlsMenu_CancelChangedBindings() {

		InputManager.Current.RevertChangesToCurrent ();

	}

	public void ControlsMenu_SaveBindings() {

		// TODO: Itterate through InputManager.Current.KeyboardKeys && ControllerKeys, find matching keys from
		// 	this.PublicKeyboardKeys & publicControllerKeys (!! CREATE OWN LISTS IN THIS MANAGER !!), if they match
		//	then replace the dictionary entry.
		//InputManager.Current.SaveNewKeybindsListsToDictionary(changingKeyboardKeybindings, changingControllerKeybindings);

		//InputManager.Current.SetKeybindings ();
  
	}

	public void ControlsMenu_RevertToDefaultBindings() {

		InputManager.Current.RevertToDefaultBindings ();

	}

	#endregion

	#region Main Menu

	public void QuitToMainMenu(string mainMenuScene) {

		UnityEngine.SceneManagement.SceneManager.LoadScene (mainMenuScene);

	}

	public void QuitToDesktop() {

		Application.Quit ();

	}

	#endregion
		
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