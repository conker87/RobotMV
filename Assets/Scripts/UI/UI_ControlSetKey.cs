using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ControlSetKey : MonoBehaviour {

	public Text LabelText, ButtonLabel;
	public Button button;

	string prefabKeyID = "";

	// Use this for initialization
	void Start () {

		button.onClick.AddListener ( delegate() { PauseManager.Current.ControlsMenu_KeyboardBindingOnClick( prefabKeyID ); } );

	} 

	public void SetPrefabKeyID (string Key_ID) {

		prefabKeyID = Key_ID;

	}

}
