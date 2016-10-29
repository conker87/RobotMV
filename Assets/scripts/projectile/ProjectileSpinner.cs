using UnityEngine;
using System.Collections;

public class ProjectileSpinner : Projectile {

	int timesThroughEnemy = 0;
	public int timesThroughEnemyMax = 0;

	protected override void OnTriggerEnter2D(Collider2D other) {

		// Trigger Base
		if ((other.gameObject.tag == "Player" && projectileType == ProjectileType.PLAYER) ||
			(other.gameObject.tag == "Enemy" && projectileType == ProjectileType.ENEMY) || 
			other.gameObject.tag == "IgnoreCollision") {

			return;

		}

		if (other.gameObject.tag == "Geometry") {

			if (!ignoreGeometry) {

				OnDeath ();
				Destroy (gameObject);

				return;

			}

		}

		Entity e;

		if ((e = other.gameObject.GetComponentInParent<Entity> ()) != null) {

			e.DamageHealth(projectileDamage);

			if (timesThroughEnemyMax > 0) {

				destroyOnHit = (timesThroughEnemy < timesThroughEnemyMax) ? false : true;

				timesThroughEnemy++;

			}

			if (destroyOnHit) {

				OnDeath ();
				Destroy (gameObject);

			}

		}

	}

}