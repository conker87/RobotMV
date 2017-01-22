using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopulateKeyboardControlSet : MonoBehaviour {

	public GameObject ControlSetKeyPrefab;

	public List<GameObject> populatedList = new List<GameObject>();

	void Start () {

		foreach(KeyValuePair<string, KeycodeDetails> entry in InputManager.Current.KeyboardKeys) {

			if (entry.Value.ignoreInSettings) {

				continue;

			}

			GameObject newObject = Instantiate (ControlSetKeyPrefab, gameObject.transform) as GameObject;

			newObject.GetComponent<UI_ControlSetKey> ().SetPrefabKeyID (entry.Key.ToString ());

			newObject.name = "KeyboardKeybindButton_" + entry.Key.ToString();

			newObject.transform.localScale = new Vector3 (1f, 1f, 1f);

			Text[] labels = newObject.GetComponentsInChildren<Text> ();

			labels [0].text = entry.Key.ToString();
			labels [1].text = entry.Value.keyUsed.ToString();

			populatedList.Add (newObject);

		}

	}
	

	void Update () {
		
	}

}
