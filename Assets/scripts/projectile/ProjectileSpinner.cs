using UnityEngine;
using System.Collections;

public class ProjectileSpinner : Projectile {

	int timesThroughEnemy = 0;
	public int timesThroughEnemyMax = 0;

	protected override void Update () {

		if (PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		base.Update ();

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		// Trigger Base
		if ((other.gameObject.tag == "Player" && ProjectileType == ProjectileType.PLAYER) ||
			(other.gameObject.tag == "Enemy" && ProjectileType == ProjectileType.ENEMY) || 
			other.gameObject.tag == "IgnoreCollision") {

			return;

		}

		if (other.gameObject.tag == "Geometry") {

			if (!IgnoreGeometry) {

				OnDeath ();
				Destroy (gameObject);

				return;

			}

		}

		Entity e;

		if ((e = other.gameObject.GetComponentInParent<Entity> ()) != null) {

			e.DamageVital ("HEALTH", ProjectileDamage);

			// e.DamageHealth(projectileDamage);

			if (timesThroughEnemyMax > 0) {

				DestroyOnHit = (timesThroughEnemy < timesThroughEnemyMax) ? false : true;

				timesThroughEnemy++;

			}

			if (DestroyOnHit) {

				OnDeath ();
				Destroy (gameObject);

			}

		}

	}

}