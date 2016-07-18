using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_HealthBar : MonoBehaviour {

	void Update () {
	
		float percentageClamped = Mathf.Clamp01 (Player.Current.Health / Player.Current.HealthMaximum);
		Vector3 percentageScale = new Vector3 (percentageClamped, 1, 1);

		transform.localScale = percentageScale;

	}

}
