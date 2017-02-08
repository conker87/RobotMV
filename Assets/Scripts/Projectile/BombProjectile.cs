using UnityEngine;
using System.Collections;

// Refactored 03/02/2017
public class BombProjectile : ProjectileBase {

	protected override void Start () { 

		if (Direction != Vector3.zero && Direction != Direction.normalized) {

			Direction.Normalize();

		}

		if (ProjectileRotatesToDirection && Direction != Vector3.zero) {

			float angle = Mathf.Atan2 (Direction.x, -Direction.y) * Mathf.Rad2Deg + 180;
			Vector3 euler = transform.eulerAngles;
			euler.z = Mathf.LerpAngle (euler.z, angle, Time.deltaTime * 90f);
			transform.eulerAngles = euler;

		}

		if (DestroyInSecondsOn) {
			Invoke ("Die", DestroyInSeconds);
		}

	}

	protected override void Die() {

		if (ProjectileHitAnimation != null) {

			GameObject explosion = Instantiate (ProjectileHitAnimation, transform.position, Quaternion.identity);
			ProjectileBase p = explosion.GetComponent<ProjectileBase> ();


			p.SetSettings (Vector2.zero, 0f, false, ProjectileType.PLAYER, ProjectileDamage, WeaponLevel, true, false, true, DestroyInSeconds);

		}

		Destroy(gameObject);

	}

}