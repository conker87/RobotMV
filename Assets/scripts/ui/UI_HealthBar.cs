﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_HealthBar : MonoBehaviour {

	public Entity p;

	void Update () {
	
		float percentageClamped = Mathf.Clamp01 (p.Health / p.HealthMaximum);
		Vector3 percentageScale = new Vector3 (percentageClamped, 1, 1);

		transform.localScale = percentageScale;

	}

}