using UnityEngine;
using System.Collections;

// Created 03/02/2017
public class BombExplosionProjectile : ProjectileBase {

	public float attacksPerProjectile = 6f;
	float attackSpeedTime = 0f;

	protected override void OnTriggerEnter2D(Collider2D other) {

		// If the collider is an EnergyShield & the type is not PLAYER then destroy the projectile as it's an Enemy's.
		if (other.GetComponent<EnergyShieldProjectile> () != null && ProjectileType != ProjectileType.PLAYER) {

			Die ();
			return;

		}

		// If the projectile either: hits Player & is type PLAYER, hits an Enemy & is type ENEMY, hits a PowerUp,
		//	hits another Projectile or hits an object that is tagged appropriately then this is ignored.
		if (	(other.GetComponent<Player>() != null && ProjectileType == ProjectileType.PLAYER) ||
			(other.GetComponent<Enemy>() != null && ProjectileType == ProjectileType.ENEMY) ||
			(other.GetComponent<PowerUp>()) ||
			(other.GetComponent<ProjectileBase>()) ||
			other.gameObject.tag == "IgnoreCollision") {

			return;

		}

		if ((e = other.GetComponentInParent<Entity> ()) != null) {

			if (e.tag == "Geometry" && gameObject.tag != "DestroyGeometry") {

				return;

			}

			attackSpeedTime = 0f;

			if (DestroyOnHit) {

				Die ();

			}

			return;

		}

		if (!IgnoreGeometry) {

			Die ();

		}

	}

	void OnTriggerStay2D(Collider2D other) {

		if (Time.time > attackSpeedTime) {

			if ((e = other.GetComponent<Entity> ()) != null) {

				if (e is Player) {

					return;

				}
					
				e.DamageHealth (ProjectileDamage);

				attackSpeedTime = Time.time + (DestroyInSeconds / attacksPerProjectile);

			}

		}

	}

}