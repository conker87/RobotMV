using UnityEngine;
using System.Collections;

public abstract class Weapon : Usables {

	public virtual void Shoot (Vector3 ShootLocationPosition, Vector2 Direction) {

		if (stillCoolingDown) {

			return;

		}

		ShootMouse (ShootLocationPosition, Direction);

	}

	// Default Shoot method, will be overritten by more complex firing methods.
	public virtual void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, false, projectileType, Damage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + Cooldown;

		}

	}

}