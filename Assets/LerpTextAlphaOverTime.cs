using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpTextAlphaOverTime : MonoBehaviour {
	
	public float timeIn;
	[SerializeField] float time;

	public float fromAlpha = 1f, toAlpha = 0f;

	Text text;

	void Start() {

		text = GetComponent<Text> ();

	}


	// Update is called once per frame
	void Update () {



	}

}
