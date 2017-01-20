using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour {

	#region Singleton
	private static InputManager _current;
	public static InputManager Current {
		get {
		
			if (_current == null)
			{
				_current = GameObject.FindObjectOfType<InputManager>();
			}

			return _current;

		}
	}
	#endregion

	void Awake() {
		
		DontDestroyOnLoad(gameObject);

	}

	public Dictionary<string, KeycodeDetails> KeyboardKeys, ControllerButtons;

	public bool isUsingController = false;

    void OnEnable() {
		
		KeyboardKeys = new Dictionary<string, KeycodeDetails>();
		ControllerButtons = new Dictionary<string, KeycodeDetails>();

        // TODO:  Consider reading these from a user preferences file

		// Keyboard Settings
		KeyboardKeys["Up"]  = 						new KeycodeDetails(KeyCode.W);
		KeyboardKeys["Down"]  = 					new KeycodeDetails(KeyCode.S);
		KeyboardKeys["Left"]  = 					new KeycodeDetails(KeyCode.A);
		KeyboardKeys["Right"] = 					new KeycodeDetails(KeyCode.D);
		KeyboardKeys["Jump"]  = 					new KeycodeDetails(KeyCode.Space);
		KeyboardKeys["Fire Weapon"]  = 				new KeycodeDetails(KeyCode.Return);
		KeyboardKeys["Use Item"]	= 				new KeycodeDetails(KeyCode.F);
		KeyboardKeys["Fix Location"]  =				new KeycodeDetails(KeyCode.LeftShift);

		KeyboardKeys["UIBack"]	= 					new KeycodeDetails(KeyCode.Escape, true);	// B

		KeyboardKeys["Pause"]	= 					new KeycodeDetails(KeyCode.Escape, true);

		KeyboardKeys["DEBUG_ResetScene"] = 			new KeycodeDetails(KeyCode.P, true);

		// Controller Settings
		ControllerButtons["Jump"]  = 				new KeycodeDetails(KeyCode.JoystickButton2);	// X
		ControllerButtons["Fire Weapon"]	= 		new KeycodeDetails(KeyCode.JoystickButton0);	// A
		ControllerButtons["Fix Location"]  =		new KeycodeDetails(KeyCode.JoystickButton4);	// leftButton
		ControllerButtons["Use Item"]	= 			new KeycodeDetails(KeyCode.JoystickButton1);	// B

		ControllerButtons["UIBack"]	= 				new KeycodeDetails(KeyCode.JoystickButton1, true);	// B

		ControllerButtons["Pause"]	= 				new KeycodeDetails(KeyCode.JoystickButton7, true);

		ControllerButtons["DEBUG_ResetScene"] = 	new KeycodeDetails(KeyCode.P, true);

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

    public string[] GetButtonNames() {
		
        return KeyboardKeys.Keys.ToArray();

    }

	public string GetKeyNameForButton( string buttonName ) {
        
		if (KeyboardKeys.ContainsKey(buttonName) == false) {
			
            Debug.LogError("InputManager::GetKeyNameForButton -- No button named: " + buttonName);
            return "N/A";

        }

        return KeyboardKeys[buttonName].keyUsed.ToString();
    }

	public void SetButtonForKey( string buttonName, KeyCode keyCode, bool ignoreInSettings = false) {

		KeycodeDetails newKey = new KeycodeDetails (keyCode, ignoreInSettings);

		KeyboardKeys [buttonName] = newKey;

    }

}

public struct KeycodeDetails {

	public KeycodeDetails(KeyCode KeyUsed, bool IgnoreInSettings = false) {

		this.keyUsed = KeyUsed;
		this.ignoreInSettings = IgnoreInSettings;

	}

	public void Add(KeyCode KeyUsed, bool IgnoreInSettings) {

		this.keyUsed = KeyUsed;
		this.ignoreInSettings = IgnoreInSettings;

	}

	public KeyCode		keyUsed;
	public bool			ignoreInSettings;

}