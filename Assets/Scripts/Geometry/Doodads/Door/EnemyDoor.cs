using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Refactored: 02/02/2017
public class EnemyDoor : DoorBase {

	public List<Enemy> enemies = new List<Enemy>();

	int nullingEnemies = 0;

	[Header("")]
	float timeUntilNextEnemyCheck;
	float timeUntilEnemyReset;

	protected override void Start () {
		
		base.Start ();

		// EnemyDoor's stay open when all the enemies are kill, this will make this so much fucking easier in the long term.
		WillDoorStayOpen = true;

	}

	protected override void Update () {

		// If the door is open and the door will stay open then checking is no longer needed.
		if (!(IsDoorOpen () && WillDoorStayOpen)) {

			CheckForOwnedEnemies ();

		}

	}

	void CheckForOwnedEnemies() {

		if (enemies == null || IsDoorOpen()) {

			return;

		}

		if (Time.time > timeUntilNextEnemyCheck) {

			nullingEnemies = enemies.Count;

			foreach (Enemy e in enemies) {

				if (e == null) {

					nullingEnemies--;

				}

			}

			if (nullingEnemies == 0) {

				OpenDoor ();

			}

			timeUntilNextEnemyCheck = Time.time + Constants.DoorCheckingTick;

		}

	}

}
