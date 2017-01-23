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

	public Dictionary<string, Keybinds> CurrentKeybindings, ChangingKeybindings;

	public bool isUsingController = false;

    void OnEnable() {
		
		CurrentKeybindings = new Dictionary<string, Keybinds> ();

		CurrentKeybindings.Clear ();

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
		
		if (CurrentKeybindings.ContainsKey (buttonName) == false) {

			Debug.LogError ("InputManager::GetButton -- No button named: " + buttonName);

			return false;

		}

		if (isUsingController) {
			
			return Input.GetKey(CurrentKeybindings[buttonName].ControllerBinds);

		} else {


			return Input.GetKey(CurrentKeybindings[buttonName].KeyboardBinds);


		}

	}

	public bool GetButtonDown(string buttonName) {

		if (CurrentKeybindings.ContainsKey (buttonName) == false) {

			Debug.LogError ("InputManager::GetButton -- No button named: " + buttonName);

			return false;

		}

		if (isUsingController) {

			return Input.GetKeyDown(CurrentKeybindings[buttonName].ControllerBinds);

		} else {
			
			return Input.GetKeyDown(CurrentKeybindings[buttonName].KeyboardBinds);

		}

	}

	public bool GetButtonUp(string buttonName)  {

		if (CurrentKeybindings.ContainsKey (buttonName) == false) {

			Debug.LogError ("InputManager::GetButton -- No button named: " + buttonName);

			return false;

		}

		if (isUsingController) {

			return Input.GetKeyUp(CurrentKeybindings[buttonName].ControllerBinds);

		} else {


			return Input.GetKeyUp(CurrentKeybindings[buttonName].KeyboardBinds);


		}

	}

    public string[] GetButtonNames() {
		
		return CurrentKeybindings.Keys.ToArray();

    }

	public string[] GetNamesForButton( string buttonName ) {
        
		if (CurrentKeybindings.ContainsKey(buttonName) == false) {
			
            Debug.LogError("InputManager::GetKeyNameForButton -- No button named: " + buttonName);
            return null;

        }

		return new string[] { CurrentKeybindings[buttonName].KeyboardBinds.ToString(), CurrentKeybindings[buttonName].ControllerBinds.ToString() } ;
    }

	#endregion

	#region Keybinding Methods

	public Dictionary<string, Keybinds> GetKeybindings() {

		return CurrentKeybindings;

	}

	public void SetKeybindings() {

		CurrentKeybindings = ChangingKeybindings;

	}

	public Dictionary<string, Keybinds> GetChangableKeybindings() {

		return ChangingKeybindings;

	}

	public void SetChangableKeybindings(Keybinds keybind) {

		ChangingKeybindings[keybind.key_id] = keybind;

	}

	public void RevertToDefaultBindings() {

		CurrentKeybindings["DEBUG_ResetScene"]	= new Keybinds ("DEBUG_ResetScene", 		KeyCode.P, KeyCode.Break,	true);
		CurrentKeybindings["UIBack"]			= new Keybinds ("UIBack", KeyCode.Escape,	KeyCode.JoystickButton1,	true);		// B
		CurrentKeybindings["Pause"]				= new Keybinds ("Pause", KeyCode.Escape, 	KeyCode.JoystickButton7,	true);

		CurrentKeybindings["Up"]				= new Keybinds ("Up", KeyCode.W, KeyCode.Break, true);
		CurrentKeybindings["Down"]				= new Keybinds ("Down", KeyCode.S, KeyCode.Break, true);
		CurrentKeybindings["Left"]				= new Keybinds ("Left", KeyCode.A, KeyCode.Break, true);
		CurrentKeybindings["Right"]				= new Keybinds ("Right", KeyCode.D, KeyCode.Break, true);
		CurrentKeybindings["Jump"]				= new Keybinds ("Jump", KeyCode.Space, KeyCode.JoystickButton2,	true);				// X
		CurrentKeybindings["Fire Weapon"]		= new Keybinds ("Fire Weapon", KeyCode.Return, KeyCode.JoystickButton0,	true);		// A
		CurrentKeybindings["Change Weapon"]		= new Keybinds ("Change Weapon", KeyCode.Q, KeyCode.JoystickButton1,	true);		// B
		CurrentKeybindings["Use Item"]			= new Keybinds ("Use Item", KeyCode.E, KeyCode.JoystickButton5,	true);				// RightButton
		CurrentKeybindings["Change Item"]		= new Keybinds ("Change Item", KeyCode.R, KeyCode.JoystickButton3,	true);			// Y
		CurrentKeybindings["Fix Location"]		= new Keybinds ("Fix Location", KeyCode.LeftShift, KeyCode.JoystickButton4,	true);	// LeftButton

	}

	public void RevertChangesToCurrent() {

		ChangingKeybindings = CurrentKeybindings;

	}
	#endregion

	public Vector2 GetShootingDirection(bool currentlyLookingLeft = true, bool isCurrentlyCrouching = false) {

		Vector2 Direction = Vector2.zero;
		float x, y;

		if (isUsingController) {

			x = GetAxis ("Horizontal");
			y = GetAxis ("Vertical");

			x = (x != 0f) ? Math.Sign(x) * 1f : 0f;
			y = (y != 0f) ? Math.Sign(y) * 1f : 0f;

			if (x == 0 && y == 0) {

				x = (currentlyLookingLeft) ? -1f : 1f;

			}

			Direction = new Vector2 (x, y);

		} else {

			if (GetButton ("Down")) {

				y = -1f;

			} else if (GetButton ("Up")) {

				y = 1f;

			} else {

				y = 0f;

			}

			if (GetButton ("Left")) {

				x = -1f;

			} else if (GetButton ("Right")) {

				x = 1f;

			} else {

				if (!GetButton ("Up") && !GetButton ("Down")) {
					x = (currentlyLookingLeft) ? -1f : 1f;
				} else {
					x = 0f;
				}

			}

			Direction = new Vector2 (x, y);

		}

		// Debug.Log (Direction);

		return Direction;

	}

	public void SetButtonForKey(string Key_ID, KeyCode KeyboardBinds, KeyCode ControllerBinds, bool IgnoreInSettings = false) {

		Keybinds newKeybinding = new Keybinds (Key_ID, KeyboardBinds, ControllerBinds, IgnoreInSettings);

		ChangingKeybindings [Key_ID] = newKeybinding;

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