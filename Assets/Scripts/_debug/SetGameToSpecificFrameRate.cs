using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameToSpecificFrameRate : MonoBehaviour {

	[Range(-1, 300)]
	public int framerate = 60;

	void Update () {
		Application.targetFrameRate = framerate;
	}

}
