using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ControlSetKey : MonoBehaviour {

	public Text LabelText, ButtonLabel;
	public Button button;

	// Use this for initialization
	void Start () {

		button.onClick.AddListener ( delegate() { PauseManager.Current.ControlMenu_KeyboardBindingOnClick( LabelText.text ); } );

	} 

}
