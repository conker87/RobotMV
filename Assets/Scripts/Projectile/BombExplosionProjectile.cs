using UnityEngine;
using System.Collections;

// Created 03/02/2017
public class BombExplosionProjectile : ProjectileBase {

	public float attacksPerProjectile = 6f;
	float attackSpeedTime = 0f;

	protected override void OnTriggerEnter2D(Collider2D other) {

	}

	void OnTriggerStay2D(Collider2D other) {

		if (Time.time > attackSpeedTime) {

			Entity e;

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