using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	#region Singleton

	public static PauseManager Current;

	void Awake() {

		if (Current == null) {

			Current = this;

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

	public List<GameObject> keybindingsPrefabList = new List<GameObject> ();

	[Header("Graphics Controls")]
	public Button SaveGraphics;
	public Button CancelGraphics, RevertGraphics;
	public Toggle WindowedMode;
	public Toggle FullscreenMode;

	public Transform ResolutionPrefabParent;
	public GameObject ResolutionPrefab;

	public List<Toggle> resolutionPrefabList = new List<Toggle> ();

	//public List<GameObject> BindingsList = new List<Keybinds> ();

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

		// Main Menu Listeners
		QuitToMainMenuButton.onClick.AddListener(	delegate() { QuitToMainMenu("MainMenu");		} );
		ControlsMenu.onClick.AddListener(	delegate() { SetStateForce (PauseState.CONTROL);	} );
		GraphicsMenu.onClick.AddListener(	delegate() { SetStateForce (PauseState.GRAPHICS);	} );
		SoundsMenu.onClick.AddListener	(	delegate() { SetStateForce (PauseState.SOUND);		} );

		// Graphics Listeners
		SaveGraphics.onClick.AddListener(	delegate() { GraphicsMenu_Save();		} );

		// Populate lists
//		PopulateKeybindButtons();
//		PopulateResolutionToggles ();

		// Set Defaults
		// Controls

		// Graphics
		WindowedMode.isOn = !Screen.fullScreen;

	}

	void OnEnable() {

		// Populate lists
		PopulateKeybindButtons();
		PopulateResolutionToggles ();

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

//			FullscreenMode.isOn = !WindowedMode.isOn;
//			WindowedMode.isOn = !FullscreenMode.isOn;

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

	#region Main Menu

	public void QuitToMainMenu(string mainMenuScene) {

		UnityEngine.SceneManagement.SceneManager.LoadScene (mainMenuScene);

	}

	public void QuitToDesktop() {

		Application.Quit ();

	}

	#endregion

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

	void PopulateKeybindButtons() {

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

			CSK.labelText.text = key;

			CSK.KeyboardBind.onClick.AddListener (delegate() {
				ControlsMenu_KeyboardBindingOnClick (key, false);
			});

			CSK.KeyboardBindLabel.text = k.Value.KeyboardBinds.ToString();
			CSK.KeyboardBind.interactable = true;

			CSK.ControllerBindLabel.text = "";

			if (k.Value.ControllerBinds != KeyCode.Break) {
				CSK.ControllerBind.onClick.AddListener (delegate() {
					ControlsMenu_KeyboardBindingOnClick (key, true);
				});

				CSK.ControllerBind.interactable = true;
				CSK.ControllerBindLabel.text = k.Value.ControllerBinds.ToString();

			}

			keybindingsPrefabList.Add (buttonGameObject);

		}

	}

	#endregion

	#region Graphics Menu

	public void GraphicsMenu_Save() {

		int width = 1280, height = 720, refreshRate = 60;
		bool fullscreen = !WindowedMode.isOn;

		Debug.Log("GraphicsMenu_Save");

		foreach (Toggle g in resolutionPrefabList) {

			Debug.Log (g);

			if (g.isOn) {

				Text labelText = g.GetComponentInChildren<Text> ();
				string[] split = labelText.text.Split ('x');

				width = int.Parse (split [0]);
				height = int.Parse (split [1]);
				refreshRate = int.Parse (split [2].Remove(split [2].Length - 2));

				break;

			}

		}

		Debug.Log (string.Format("width: {0}, height: {1}, fullscreen: {2}, refreshRate: {3}", width, height, fullscreen, refreshRate));

		Screen.SetResolution (width, height, fullscreen, refreshRate);

	}

	void PopulateResolutionToggles() {

		Resolution[] resolutions = Screen.resolutions;

		resolutions = resolutions.OrderByDescending (item => item.width).ToArray ();

		string previousRes = "", currentRes = "";

		foreach(Resolution r in resolutions) {

			currentRes = r.width + "x" + r.height + " " + r.refreshRate;

			if (currentRes == previousRes) {

				continue;

			}

			previousRes = r.width + "x" + r.height + " " + r.refreshRate;

			// TODO: ControlSetKeyPrefab now has a component that stores the cache of these, use GetComponent<ControlSetKeybind>();
			GameObject toggleGameObject = Instantiate (ResolutionPrefab, ResolutionPrefabParent) as GameObject;
			Toggle toggle = toggleGameObject.GetComponent<Toggle>();
			Text toggleLabel = toggleGameObject.GetComponentInChildren<Text> ();

			if (r.height == Screen.height && r.width == Screen.width) {

				toggle.isOn = true;

			}

			toggleGameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
			toggleGameObject.name = "ResolutionToggle_" + r.width + "x" + r.height + "_" + r.refreshRate;

			toggleLabel.text = r.width + "x" + r.height + "x" + r.refreshRate + "Hz";

			resolutionPrefabList.Add (toggle);

		}

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