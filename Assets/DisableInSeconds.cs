using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInSeconds : MonoBehaviour {

	public bool isOn = false;
	public float timeIn;
	float time;

	// Update is called once per frame
	void Update () {

		if (isOn) {

			if (Time.time > time) {

				isOn = false;
				gameObject.SetActive (false);

			}

		}

	}

	public void Reset(float seconds) {

		gameObject.SetActive (true);
		isOn = true;
		time = Time.time + seconds;

	}

}
