using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToggleGroupFromParent : MonoBehaviour {

	Toggle toggle;

	void OnEnable () {

		toggle = GetComponent<Toggle> ();

		toggle.group = GetComponentInParent<ToggleGroup> ();



	}

}
