using UnityEngine;
using System.Collections;

public abstract class Weapon : Usables {

	public virtual void Shoot (Vector3 ShootLocationPosition, Vector2 Direction) {

		if (stillOnCooldown) {

			return;

		}

		ShootMouse (ShootLocationPosition, Direction);

	}

	// Default Shoot method, will be overritten by more complex firing methods.
	public virtual void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);

			ProjectileBase projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as ProjectileBase;

			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, false, projectileType, InitialDamage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + InitialCooldown;

		}

	}

}