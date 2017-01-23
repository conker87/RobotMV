using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorEnemy : Door {

	public List<Enemy> enemies = new List<Enemy>();

	int nullingEnemies = 0;

	[Header("")]
	float timeUntilNextEnemyCheck;
	float timeUntilEnemyReset;

	bool addToReset = false;

	protected override void Start () {
		
		base.Start ();

		nullingEnemies = enemies.Count;

	}

	protected override void Update () {

		DoCircleCheck (willDoorStayOpen, false);

		DoSwitchCheck ();

	}

	void DoSwitchCheck() {

		if (enemies != null && Time.time > timeUntilNextEnemyCheck) {

			foreach (Enemy e in enemies) {

				if (e == null) {

					nullingEnemies--;

				}

			}
			if (nullingEnemies == 0) {

				Debug.Log ("All enemies dead");
				OpenDoor ();

			} else {
				
				nullingEnemies = enemies.Count;

			}

			timeUntilNextEnemyCheck = Time.time + Constants.Tick;

		}

	}

}
