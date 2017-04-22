using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour {

	#region Singleton

	public static InputManager Current;

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

	public List<KeyCode> bannedKeybinds = new List<KeyCode> ();

	public Dictionary<string, Keybinds> SavedKeybindings, UnsavedKeybindings;

	public bool isUsingController = false;

    void OnEnable() {
		
		SavedKeybindings = new Dictionary<string, Keybinds> ();

		SavedKeybindings.Clear ();

		// TODO: Load keybinds from file.
		RevertToDefaultBindings ();

		// Replaces the working changes to the current keybinds.
		RevertChangesToCurrent ();

    }

	void Update () {

	}

	#region Get Button Methods

	public float GetAxis (string axisName) {

		return Input.GetAxisRaw (axisName);

	}

	public bool GetButton(string buttonName)  {
		
		if (!SavedKeybindings.ContainsKey (buttonName)) return false;
			
		return (isUsingController ? Input.GetKey(SavedKeybindings[buttonName].ControllerBinds) : Input.GetKey(SavedKeybindings[buttonName].KeyboardBinds));

	}

	public bool GetButtonDown(string buttonName) {

		if (!SavedKeybindings.ContainsKey (buttonName)) return false;

		return (isUsingController ? Input.GetKeyDown(SavedKeybindings[buttonName].ControllerBinds) : Input.GetKeyDown(SavedKeybindings[buttonName].KeyboardBinds));

	}

	public bool GetButtonUp(string buttonName)  {

		if (!SavedKeybindings.ContainsKey (buttonName)) return false;

		return (isUsingController ? Input.GetKeyUp(SavedKeybindings[buttonName].ControllerBinds) : Input.GetKeyUp(SavedKeybindings[buttonName].KeyboardBinds));

	}

    public string[] GetButtonNames() {
		
		return SavedKeybindings.Keys.ToArray();

    }

	public string[] GetNamesForButton( string buttonName ) {
        
		if (SavedKeybindings.ContainsKey(buttonName) == false) {
			
            Debug.LogError("InputManager::GetKeyNameForButton -- No button named: " + buttonName);
            return null;

        }

		return new string[] { SavedKeybindings[buttonName].KeyboardBinds.ToString(), SavedKeybindings[buttonName].ControllerBinds.ToString() } ;
    }

	#endregion

	#region Keybinding Methods

	public Dictionary<string, Keybinds> GetKeybindings() {

		return SavedKeybindings;

	}

	public void SaveKeybindings() {

		SavedKeybindings = UnsavedKeybindings;

	}

	public Dictionary<string, Keybinds> GetUnsavedKeybindings() {

		return UnsavedKeybindings;

	}

	// TODO: The PauseManager Controller area should probably use this?
	public void SetUnsavedKeybindings(Keybinds keybind) {

		UnsavedKeybindings[keybind.key_id] = keybind;

	}

	public void RevertToDefaultBindings() {

		SavedKeybindings["Up"]					= new Keybinds ("Up", KeyCode.W, KeyCode.Break, true);
		SavedKeybindings["Down"]				= new Keybinds ("Down", KeyCode.S, KeyCode.Break, true);
		SavedKeybindings["Left"]				= new Keybinds ("Left", KeyCode.A, KeyCode.Break, true);
		SavedKeybindings["Right"]				= new Keybinds ("Right", KeyCode.D, KeyCode.Break, true);
		SavedKeybindings["Jump"]				= new Keybinds ("Jump", KeyCode.Space, KeyCode.JoystickButton2,	true);				// X
		SavedKeybindings["Fire Weapon"]			= new Keybinds ("Fire Weapon", KeyCode.Return, KeyCode.JoystickButton0,	true);		// A
		SavedKeybindings["Change Weapon"]		= new Keybinds ("Change Weapon", KeyCode.Q, KeyCode.JoystickButton1,	true);		// B
		SavedKeybindings["Use Item"]			= new Keybinds ("Use Item", KeyCode.E, KeyCode.JoystickButton5,	true);				// RightButton
		SavedKeybindings["Change Item"]			= new Keybinds ("Change Item", KeyCode.R, KeyCode.JoystickButton3,	true);			// Y
		SavedKeybindings["Fix Location"]		= new Keybinds ("Fix Location", KeyCode.LeftShift, KeyCode.JoystickButton4,	true);	// LeftButton

		SavedKeybindings["DEBUG_ResetScene"]	= new Keybinds ("DEBUG_ResetScene", 		KeyCode.P, KeyCode.Break,	true);
		SavedKeybindings["UIBack"]				= new Keybinds ("UIBack", KeyCode.Escape,	KeyCode.JoystickButton1,	true);		// B
		SavedKeybindings["Pause"]				= new Keybinds ("Pause", KeyCode.Escape, 	KeyCode.JoystickButton7,	true);

	}

	public void RevertChangesToCurrent() {

		UnsavedKeybindings = SavedKeybindings;

	}
	#endregion

	public void SetButtonForKey_Unsaved(string Key_ID, KeyCode KeyboardBinds, KeyCode ControllerBinds, bool IgnoreInSettings = false) {

		Keybinds newKeybinding = new Keybinds (Key_ID, KeyboardBinds, ControllerBinds, IgnoreInSettings);

		UnsavedKeybindings [Key_ID] = newKeybinding;

    }

}

[Serializable]
public struct Keybinds {

	public Keybinds(string Key_ID, KeyCode KeyboardBinds, KeyCode ControllerBinds, bool IgnoreInSettings = false) {

		this.key_id = Key_ID;
		this.KeyboardBinds = KeyboardBinds;
		this.ControllerBinds = ControllerBinds;
		this.ignoreInSettings = IgnoreInSettings;

	}

	public void Add(string Key_ID, KeyCode KeyboardBinds, KeyCode ControllerBinds, bool IgnoreInSettings = false) {

		this.key_id = Key_ID;
		this.KeyboardBinds = KeyboardBinds;
		this.ControllerBinds = ControllerBinds;
		this.ignoreInSettings = IgnoreInSettings;

	}

	public string 		key_id;
	public KeyCode		KeyboardBinds;
	public KeyCode		ControllerBinds;
	public bool			ignoreInSettings;

}

public enum KeybindingScheme { KEYBOARD, CONTROLLER };