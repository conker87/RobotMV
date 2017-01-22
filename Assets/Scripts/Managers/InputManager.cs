using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour {

	#region Singleton
	private static InputManager _current;
	public static InputManager Current {
		get {
		
			if (_current == null) {
				_current = GameObject.FindObjectOfType<InputManager>();
			}

			return _current;

		}
	}
	#endregion

	public List<KeyCode> bannedKeybinds = new List<KeyCode> ();

	void Awake() {
		
		DontDestroyOnLoad(gameObject);

	}

	public Dictionary<string, KeycodeDetails> KeyboardKeys, ControllerButtons;

	public List<KeycodeDetails> publicKeyboardKeys = new List<KeycodeDetails> ();
	public List<KeycodeDetails> publicControllerButtons = new List<KeycodeDetails> ();

	public bool isUsingController = false;

    void OnEnable() {
		
		KeyboardKeys = new Dictionary<string, KeycodeDetails> ();
		ControllerButtons = new Dictionary<string, KeycodeDetails> ();

		KeyboardKeys.Clear ();
		ControllerButtons.Clear ();

		KeyboardKeys ["DEBUG_ResetScene"] = new KeycodeDetails (KeyCode.P, true, "DEBUG_ResetScene");
		KeyboardKeys ["UIBack"]	= new KeycodeDetails (KeyCode.Escape, true, "UIBack");	// B
		KeyboardKeys ["Pause"]	= new KeycodeDetails (KeyCode.Escape, true, "Pause");

		ControllerButtons ["DEBUG_ResetScene"] = new KeycodeDetails (KeyCode.P, true, "DEBUG_ResetScene");
		ControllerButtons ["UIBack"]	= new KeycodeDetails (KeyCode.JoystickButton1, true, "UIBack");	// B
		ControllerButtons ["Pause"]	= new KeycodeDetails (KeyCode.JoystickButton7, true, "Pause");

		// TODO:  Consider reading these from a user preferences file
		if (publicKeyboardKeys == null) {
			
			// Keyboard Settings
			KeyboardKeys ["Up"] = new KeycodeDetails (KeyCode.W, false, "Up");
			KeyboardKeys ["Down"] = new KeycodeDetails (KeyCode.S, false, "Down");
			KeyboardKeys ["Left"] = new KeycodeDetails (KeyCode.A, false, "Left");
			KeyboardKeys ["Right"] = new KeycodeDetails (KeyCode.D, false, "Right");
			KeyboardKeys ["Jump"] = new KeycodeDetails (KeyCode.Space, false, "Jump");
			KeyboardKeys ["Fire Weapon"] = new KeycodeDetails (KeyCode.Return, false, "Fire Weapon");
			KeyboardKeys ["Use Item"]	= new KeycodeDetails (KeyCode.F, false, "Use Item");
			KeyboardKeys ["Fix Location"] = new KeycodeDetails (KeyCode.LeftShift, false, "Fix Location");

		} else {

			foreach (KeycodeDetails k in publicKeyboardKeys) {

				// This should not require validation as this is from the editor.
				KeyboardKeys.Add (k.key_id, new KeycodeDetails (k.keyUsed, k.ignoreInSettings, k.key_id));

			}

		}

		if (publicControllerButtons == null) {

			// Controller Settings
			ControllerButtons ["Jump"] = new KeycodeDetails (KeyCode.JoystickButton2, false, "Jump");	// X
			ControllerButtons ["Fire Weapon"]	= new KeycodeDetails (KeyCode.JoystickButton0, false, "Fire Weapon");	// A
			ControllerButtons ["Fix Location"] = new KeycodeDetails (KeyCode.JoystickButton4, false, "Fix Location");	// leftButton
			ControllerButtons ["Use Item"]	= new KeycodeDetails (KeyCode.JoystickButton1, false, "Use Item");	// B

		} else {

			foreach (KeycodeDetails k in publicControllerButtons) {

				ControllerButtons.Add (k.key_id, new KeycodeDetails (k.keyUsed, k.ignoreInSettings, k.key_id));

			}

		}

		publicControllerButtons.Clear ();
		publicKeyboardKeys.Clear ();

    }

	void Update () {

//		if (Input.anyKeyDown) {
//
//			foreach (KeyCode code in Enum.GetValues (typeof(KeyCode))) {
//
//				if (Input.GetKeyDown (code)) {
//
//					//Debug.Log (code);
//
//				}
//
//			}
//
//		}

	}

	#region Get Button Methods
	public float GetAxis (string axisName) {

		return Input.GetAxisRaw (axisName);

	}

	public bool GetButton(string buttonName)  {

		if (isUsingController) {
			
			if (ControllerButtons.ContainsKey (buttonName) == false) {

				Debug.LogError ("InputManager::GetButtonDown -- No button named: " + buttonName);

				return false;
			
			}

			return Input.GetKey(ControllerButtons[buttonName].keyUsed);

		} else {

			if (KeyboardKeys.ContainsKey(buttonName) == false) {

				Debug.LogError("InputManager::GetButtonDown -- No button named: " + buttonName);

				return false;
			}

			return Input.GetKey(KeyboardKeys[buttonName].keyUsed);


		}

	}

	public bool GetButtonDown(string buttonName) {

		if (isUsingController) {

			if (ControllerButtons.ContainsKey (buttonName) == false) {

				Debug.LogError ("InputManager::GetButtonDown -- No button named: " + buttonName);

				return false;

			}

			return Input.GetKeyDown (ControllerButtons [buttonName].keyUsed);

		} else {
			
			if (KeyboardKeys.ContainsKey (buttonName) == false) {

				Debug.LogError ("InputManager::GetButtonDown -- No button named: " + buttonName);

				return false;

			}

			return Input.GetKeyDown(KeyboardKeys[buttonName].keyUsed);

		}

	}

	public bool GetButtonUp(string buttonName)  {

		if (isUsingController) {

			if (ControllerButtons.ContainsKey (buttonName) == false) {

				Debug.LogError ("InputManager::GetButtonDown -- No button named: " + buttonName);

				return false;

			}

			return Input.GetKeyUp (ControllerButtons [buttonName].keyUsed);

		} else {

			if (KeyboardKeys.ContainsKey (buttonName) == false) {

				Debug.LogError ("InputManager::GetButtonDown -- No button named: " + buttonName);

				return false;

			}

			return Input.GetKeyUp(KeyboardKeys[buttonName].keyUsed);

		}

	}

    public string[] GetButtonNames() {
		
        return KeyboardKeys.Keys.ToArray();

    }

	public string GetKeyboardNameForButton( string buttonName ) {
        
		if (KeyboardKeys.ContainsKey(buttonName) == false) {
			
            Debug.LogError("InputManager::GetKeyNameForButton -- No button named: " + buttonName);
            return "N/A";

        }

        return KeyboardKeys[buttonName].keyUsed.ToString();
    }
	#endregion

	#region Keybinding Methods

	public Dictionary<string, KeycodeDetails> GetKeyboardKeybinding() {

		return KeyboardKeys;

	}

	public Dictionary<string, KeycodeDetails> GetControllerKeybinding() {

		return ControllerButtons;

	}

	public void SaveNewKeybindsListsToDictionary(List<KeycodeDetails> keyboardKeybinds, List<KeycodeDetails> controllerKeybinds) {

		foreach (KeycodeDetails k in keyboardKeybinds) {

			if (KeyboardKeys.ContainsKey (k.key_id)) {

				KeyboardKeys [k.key_id] = new KeycodeDetails (k.keyUsed, k.ignoreInSettings, k.key_id);

			}

		}

		foreach (KeycodeDetails c in controllerKeybinds) {

			if (ControllerButtons.ContainsKey (c.key_id)) {

				ControllerButtons [c.key_id] = new KeycodeDetails (c.keyUsed, c.ignoreInSettings, c.key_id);

			}

		}

//		for (int i = 0; i < InputManager.Current.publicKeyboardKeys.Count; i++) {
//
//			if (InputManager.Current.publicKeyboardKeys[i].key_id == currentlyChangingKeybindID) {
//
//				InputManager.Current.publicKeyboardKeys[i] = new KeycodeDetails(code, false, currentlyChangingKeybindID);
//
//				currentlyChangingKeybindID = "";
//
//
//
//				return;
//
//			}
//		}
//
//
//		InputManager.Current.publicKeyboardKeys.Add ( new KeycodeDetails (code, false, currentlyChangingKeybindID) );

	}

	public void RevertToDefaultBindings() {

		// Keyboard Settings
		KeyboardKeys.Clear ();
		KeyboardKeys ["Up"] = new KeycodeDetails (KeyCode.W);
		KeyboardKeys ["Down"] = new KeycodeDetails (KeyCode.S);
		KeyboardKeys ["Left"] = new KeycodeDetails (KeyCode.A);
		KeyboardKeys ["Right"] = new KeycodeDetails (KeyCode.D);
		KeyboardKeys ["Jump"] = new KeycodeDetails (KeyCode.Space);
		KeyboardKeys ["Fire Weapon"] = new KeycodeDetails (KeyCode.Return);
		KeyboardKeys ["Use Item"]	= new KeycodeDetails (KeyCode.F);
		KeyboardKeys ["Fix Location"] = new KeycodeDetails (KeyCode.LeftShift);

		// Controller Settings
		ControllerButtons.Clear();
		ControllerButtons ["Jump"] = new KeycodeDetails (KeyCode.JoystickButton2);	// X
		ControllerButtons ["Fire Weapon"]	= new KeycodeDetails (KeyCode.JoystickButton0);	// A
		ControllerButtons ["Fix Location"] = new KeycodeDetails (KeyCode.JoystickButton4);	// leftButton
		ControllerButtons ["Use Item"]	= new KeycodeDetails (KeyCode.JoystickButton1);	// B

		// Debug & Menu UI keys.
		KeyboardKeys ["DEBUG_ResetScene"] = new KeycodeDetails (KeyCode.P, true);
		KeyboardKeys ["UIBack"]	= new KeycodeDetails (KeyCode.Escape, true);	// B
		KeyboardKeys ["Pause"]	= new KeycodeDetails (KeyCode.Escape, true);

		ControllerButtons ["DEBUG_ResetScene"] = new KeycodeDetails (KeyCode.P, true);
		ControllerButtons ["UIBack"]	= new KeycodeDetails (KeyCode.JoystickButton1, true);	// B
		ControllerButtons ["Pause"]	= new KeycodeDetails (KeyCode.JoystickButton7, true);

	}

	#endregion


	public void ClearPublicKeybindingLists() {

		publicKeyboardKeys.Clear ();
		publicControllerButtons.Clear ();

	}

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

	public void SetButtonForKey( string buttonName, KeyCode keyCode, bool ignoreInSettings = false) {

		KeycodeDetails newKey = new KeycodeDetails (keyCode, ignoreInSettings);

		KeyboardKeys [buttonName] = newKey;

    }

}

[Serializable]
public struct KeycodeDetails {

	public KeycodeDetails(KeyCode KeyUsed, bool IgnoreInSettings = false, string Key_ID = "") {

		this.key_id = Key_ID;
		this.keyUsed = KeyUsed;
		this.ignoreInSettings = IgnoreInSettings;

	}

	public void Add(KeyCode KeyUsed, bool IgnoreInSettings = false, string Key_ID = "") {

		this.keyUsed = KeyUsed;
		this.ignoreInSettings = IgnoreInSettings;
		this.key_id = Key_ID;

	}

	public string 		key_id;
	public KeyCode		keyUsed;
	public bool			ignoreInSettings;

}

public enum KeybindingScheme { KEYBOARD, CONTROLLER };