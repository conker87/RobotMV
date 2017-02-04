using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

[RequireComponent(typeof(EnemyController))]
public class Enemy : Entity {

	[Range(0.0f, 100f)]
	public float[]		droppedPowerUpsChance;
	[Range(1, 20)]
	public int[] 		droppedPowerUpsMax;
	public PowerUp[]	droppedPowerUps;

	[Header("Damage On Touch")]
	public int DamageOnTouch = 1;

	void OnTriggerStay2D(Collider2D other) {

		Entity e;

		if ((e = other.GetComponent<Player>()) != null) {

			e.DamageHealth(DamageOnTouch);

		}

	}

	public override void Die() {

		if (droppedPowerUps.Length == droppedPowerUpsChance.Length && droppedPowerUps.Length == droppedPowerUpsMax.Length) {

			for (int i = 0; i < droppedPowerUps.Length; i++) {

				if (droppedPowerUpsChance [i] == 0f) {

					continue;

				}

				if (droppedPowerUps[i] is PowerUpBombOrb && Player.Current.Bombs_Max == 0) {

					continue;

				}

				if (droppedPowerUps[i] is PowerUpBombMegaOrb && Player.Current.Bombs_Mega_Max == 0) {

					continue;

				}

				for (int k = 0; k < droppedPowerUpsMax [i]; k++) {

					if (Random.value > (droppedPowerUpsChance [i] / 100f)) {

						continue;

					}

					Vector3 newTransform = new Vector3 (transform.position.x + Random.Range (-1f, 1f),
						                      transform.position.y + Random.Range (-1f, 1f),
						                      0f);

					Instantiate (droppedPowerUps [i], newTransform, Quaternion.identity);

				}

			}

		}

		Destroy (gameObject);

	}

}
