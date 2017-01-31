using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy {

	[Header("Boss Fields")]
	[SerializeField]
	int bossRoomID = -1;

	public GameObject BossHealthBar_Parent, BossHealthBar_Fill_Parent, BossHealthBar_BossName;

	void Start() {

		// Add GameObject.Find("BossHealthBar_Parent");, etc.



	}

	// Update is called once per frame
	protected override void Update () {

		base.Update ();

		if (CameraManager.GetCurrentAreaIndex () == bossRoomID) {

			BossHealthBar_Parent.SetActive(true);
			BossHealthBar_BossName.GetComponent<Text> ().text = EntityNameLocalisationID; // Localisation.GetStringInCurrentLocal
			SetBossHealthBarToHealth ();
			// Debug.Log("Boss should activate: " + this + ", RoomID: " + bossRoomID);

		} else {

			BossHealthBar_Parent.SetActive(false);

		}

	}

	void SetBossHealthBarToHealth() {

		float currentPercentage = (float) Health_Current / (float) Health_Max;

		currentPercentage = Mathf.Clamp01 (currentPercentage);

		BossHealthBar_Fill_Parent.transform.localScale = new Vector3 (currentPercentage, 1f, 1f);

	}

}
