using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogOnDisable : MonoBehaviour {

	// Use this for initialization
	void OnDisable() {

		Debug.Log (this + " has been disabled.");

	}

}
