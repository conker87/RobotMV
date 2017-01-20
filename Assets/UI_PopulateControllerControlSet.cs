using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopulateControllerControlSet : MonoBehaviour {

	public GameObject ControlSetKeyPrefab;

	void Start () {

		foreach(KeyValuePair<string, KeycodeDetails> entry in InputManager.Current.ControllerButtons) {

			if (entry.Value.ignoreInSettings) {

				continue;

			}

			GameObject newObject = Instantiate (ControlSetKeyPrefab, gameObject.transform) as GameObject;

			newObject.transform.localScale = new Vector3 (1f, 1f, 1f);

			Text[] labels = newObject.GetComponentsInChildren<Text> ();

			labels [0].text = entry.Key.ToString();
			labels [1].text = entry.Value.keyUsed.ToString();

		}

	}
	

	void Update () {
		
	}

}
