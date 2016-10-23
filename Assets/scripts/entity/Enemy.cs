using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Enemy : Entity {

	string playerTag = "Player";

	[Range(0.0f, 100f)]
	public float powerUpChance = 100f;
	public Transform droppedPowerupsParent;
	public GameObject[] droppedPowerUps;

	[Header("Damage On Touch")]
	public float DamageOnTouch = 3f;

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == playerTag) {
			
			Entity e = other.gameObject.GetComponent<Entity> ();

			e.DamageHealth (DamageOnTouch);

		}

	}

	void OnDestroy() {

		// TODO: Need to add a DroppedPowerUpsChance for each PowerUp and require checking for bombs.
		//	Maybe even try using a set value for each PowerUp; Health, Energy, Bombs, MegaBombs, etc.

		foreach (GameObject g in droppedPowerUps) {

			if (Random.value < (powerUpChance / 100f)) {

				int rand = Random.Range (1, 10);

				for (int i = 0; i < rand; i++) {
				
					Vector3 newTransform = new Vector3(transform.position.x + Random.Range(-1f, 1f),
														transform.position.y + Random.Range(-1f, 1f),
														0f);

					Instantiate (g, newTransform, Quaternion.identity, droppedPowerupsParent);

				}

			}

		}

	}

}
