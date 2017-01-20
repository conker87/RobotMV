using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ControlSetPopulate : MonoBehaviour {

	public GameObject ControlSetKeyPrefab;


	void Start () {

		foreach(KeyValuePair<string, KeycodeDetails> entry in InputManager.Current.KeyboardKeys) {
			
			GameObject newObject = Instantiate (ControlSetKeyPrefab, gameObject.transform) as GameObject;

			newObject.transform.localScale = new Vector3 (1f, 1f, 1f);

		}

	}
	

	void Update () {
		
	}

}
