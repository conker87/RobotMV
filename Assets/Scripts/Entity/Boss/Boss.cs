using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy {

	[Header("Boss Fields")]
	[SerializeField]
	int bossRoomID = -1;

	[SerializeField]
	GameObject BossHealthBar_Parent, BossHealthBar_Fill_Parent, BossHealthBar_BossName;

	void Start() {

		BossHealthBar_Parent = GameObject.Find("BossHealthBar_Parent");
		BossHealthBar_Fill_Parent = GameObject.Find("BossHealthBar_Fill_Parent");
		BossHealthBar_BossName = GameObject.Find("BossHealthBar_BossName");

		BossHealthBar_Parent.SetActive (false);

	}

	// Update is called once per frame
	protected override void Update () {

		base.Update ();

		// TODO: This needs refactoring, I do not like the whole UI aspects in this update of the boss. Maybe move it to Room?
		if (!dead && CameraManager.GetCurrentAreaIndex () == bossRoomID) {

			BossHealthBar_Parent.SetActive(true);
			BossHealthBar_BossName.GetComponent<Text> ().text = EntityNameLocalisationID; // Localisation.GetStringInCurrentLocal
			SetBossHealthBarToHealth ();
			// Debug.Log("Boss should activate: " + this + ", RoomID: " + bossRoomID);

		} else {

			BossHealthBar_Parent.SetActive(false);

		}

	}

	void SetBossHealthBarToHealth() {

		float currentPercentage = (float) HealthCurrent / (float) HealthMax;

		currentPercentage = Mathf.Clamp01 (currentPercentage);

		BossHealthBar_Fill_Parent.transform.localScale = new Vector3 (currentPercentage, 1f, 1f);

	}

}
