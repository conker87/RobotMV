using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_EnergyBar : MonoBehaviour {

	void Update () {
	
		float percentageClamped = Mathf.Clamp01 (PlayerAbilities.Current.Energy / PlayerAbilities.Current.EnergyMaximum);
		Vector3 percentageScale = new Vector3 (percentageClamped, 1, 1);

		transform.localScale = percentageScale;

	}

}
