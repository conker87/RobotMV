using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

[RequireComponent(typeof(EnemyController))]
public class Enemy : Entity {

	[Range(0.0f, 100f)]
	public float		powerUpChance			= 100f;
	public Transform	droppedPowerupsParent;
	public float[]		droppedPowerUpsChance;
	public int[] 		droppedPowerUpsMax;
	public GameObject[]	droppedPowerUps;

	[Header("Damage On Touch")]
	public int DamageOnTouch = 1;

	void OnTriggerEnter2D(Collider2D other) {

		Entity e;

		if ((e = other.GetComponent<Player>()) != null) {

			e.DamageVital ("HEALTH", DamageOnTouch);

		}

	}

	public override void DoDeath() {

		if (droppedPowerUps.Length == droppedPowerUpsChance.Length || droppedPowerUps.Length == droppedPowerUpsMax.Length) {

			for (int i = 0; i < droppedPowerUps.Length; i++) {

				if (Random.value < (droppedPowerUpsChance [i] / 100f)) {

					int rand = Random.Range (1, droppedPowerUpsMax [i]);

					for (int k = 0; k < rand; k++) {

						Vector3 newTransform = new Vector3 (transform.position.x + Random.Range (-1f, 1f),
							                      transform.position.y + Random.Range (-1f, 1f),
							                      0f);

						Instantiate (droppedPowerUps [i], newTransform, Quaternion.identity, droppedPowerupsParent);

					}

				}

			}

		}

		Debug.Log (transform.root);

		Destroy (transform.parent.gameObject);

	}

}
