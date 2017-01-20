using UnityEngine;
using System.Collections;

public abstract class Weapon : Usables {

	protected bool canContinue = false;

	public virtual void Shoot (Vector3 ShootLocationPosition, Vector2 Direction) {

		canContinue = true;

		if (!Player.Current.CollectablesD.ContainsKey (CollectableID)) {

			canContinue = false;

			Debug.LogWarning ("WARNING: " + this + " has reference to a CollectableID that is invalid! '" + CollectableID + "'");

		}

		if (canContinue && (!Player.Current.CollectablesD[CollectableID] || stillCoolingDown)) {

			canContinue = false;

		}

		if (!canContinue) {

			return;

		}

		ShootMouse (ShootLocationPosition, Direction);

	}

	// Default Shoot method, will be overritten by more complex firing methods.
	public virtual void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (InputManager.Current.GetButtonDown("Fire")) {

			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, false, projectileType, Damage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + Cooldown;

		}

	}

}