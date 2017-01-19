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

	Dictionary<string, KeycodeDetails> KeyboardKeys, ControllerButtons;

	public bool isUsingController = false;

	float valueForMovementInput = 1f;

    void OnEnable() {
		
		KeyboardKeys = new Dictionary<string, KeycodeDetails>();
		ControllerButtons = new Dictionary<string, KeycodeDetails>();

        // TODO:  Consider reading these from a user preferences file

		// Keyboard Settings
		KeyboardKeys["Jump"]  = 					new KeycodeDetails(KeyCode.Space);
		KeyboardKeys["Fire"]  = 					new KeycodeDetails(KeyCode.Return);
		KeyboardKeys["FixLocation"]  =				new KeycodeDetails(KeyCode.LeftShift);
		KeyboardKeys["Up"]  = 						new KeycodeDetails(KeyCode.W, MovementAxis.Y,	1);
		KeyboardKeys["Left"]  = 					new KeycodeDetails(KeyCode.A, MovementAxis.X,	-1);
		KeyboardKeys["Down"]  = 					new KeycodeDetails(KeyCode.S, MovementAxis.Y,	-1);
		KeyboardKeys["Right"] = 					new KeycodeDetails(KeyCode.D, MovementAxis.X,	1);
		KeyboardKeys["Item"]	= 					new KeycodeDetails(KeyCode.F);

		KeyboardKeys["Pause"]	= 					new KeycodeDetails(KeyCode.Escape);

		KeyboardKeys["DEBUG_ResetScene"] = 			new KeycodeDetails(KeyCode.P);

		// Controller Settings
		ControllerButtons["Jump"]  = 				new KeycodeDetails(KeyCode.JoystickButton2);	// X
		ControllerButtons["Fire"]	= 				new KeycodeDetails(KeyCode.JoystickButton0);	// A
		ControllerButtons["FixLocation"]  =			new KeycodeDetails(KeyCode.JoystickButton4);	// leftButton
		ControllerButtons["Item"]	= 				new KeycodeDetails(KeyCode.JoystickButton1);	// B

		ControllerButtons["Pause"]	= 				new KeycodeDetails(KeyCode.JoystickButton7);

		ControllerButtons["DEBUG_ResetScene"] = 	new KeycodeDetails(KeyCode.P);

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



		return false;
	}

	public bool GetButtonDown(string buttonName) {
		// TODO: Check to see if the game is supposed to be paused
		//  Or maybe if you're in a different input mode (like a window
		//  is open, or if the player is typing in a text box)

		if ((KeyboardKeys.ContainsKey(buttonName) == false || ControllerButtons.ContainsKey(buttonName) == false)) {

			Debug.LogError("InputManager::GetButtonDown -- No button named: " + buttonName);

			return false;

		}

		return (Input.GetKeyDown( KeyboardKeys[buttonName].keyUsed)) ? Input.GetKeyDown( KeyboardKeys[buttonName].keyUsed) : Input.GetKeyDown( ControllerButtons[buttonName].keyUsed);

	}

	public bool GetButtonUp( string buttonName )  {

		if (KeyboardKeys.ContainsKey(buttonName) == false || ControllerButtons.ContainsKey(buttonName) == false) {

			Debug.LogError("InputManager::GetButtonDown -- No button named: " + buttonName);

			return false;
		}

		return (Input.GetKeyUp( KeyboardKeys[buttonName].keyUsed)) ? Input.GetKeyDown( KeyboardKeys[buttonName].keyUsed) : Input.GetKeyDown( ControllerButtons[buttonName].keyUsed);

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

	public bool GetMovementButton( string buttonName )  {

		if (KeyboardKeys.ContainsKey(buttonName) == false) {

			Debug.LogError("InputManager::GetButtonDown -- No button named: " + buttonName);
			return false;

		}

		if (Input.GetKey(KeyboardKeys[buttonName].keyUsed)) {

			if (KeyboardKeys [buttonName].movementValue != 0) {

				return Input.GetKey(KeyboardKeys[buttonName].keyUsed);

			}

		}

		return false;

	}

	public int GetMovementButtonValue( string buttonName )  {

		if (KeyboardKeys.ContainsKey(buttonName) == false) {

			Debug.LogError("InputManager::GetButtonDown -- No button named: " + buttonName);
			return 0;

		}

		return KeyboardKeys[buttonName].movementValue;

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

	public void SetButtonForKey( string buttonName, KeyCode keyCode, MovementAxis movementType = MovementAxis.NONE ) {

		KeycodeDetails newKey = new KeycodeDetails (keyCode, movementType, KeyboardKeys [buttonName].movementValue);

		KeyboardKeys [buttonName] = newKey;

    }

}

public struct KeycodeDetails {

	public KeycodeDetails(KeyCode KeyUsed, MovementAxis Movement = MovementAxis.NONE, int MovementValue = 0) {

		this.keyUsed = KeyUsed;
		this.movement = Movement;
		this.movementValue = MovementValue;

	}

	public void Add(KeyCode KeyUsed, int MovementValue) {

		this.keyUsed = KeyUsed;
		this.movementValue = MovementValue;

	}

	public KeyCode		keyUsed;
	public MovementAxis movement;
	public int			movementValue;

}

public enum MovementAxis { NONE, X, Y }