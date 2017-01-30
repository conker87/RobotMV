﻿using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		// TODO: Change to InputManager.Current.GetButton("Fire Weapon")
		if (InputManager.Current.GetButtonDown("Fire Weapon") || Input.GetMouseButtonDown (0)) {

			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, true, projectileType, InitialDamage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + InitialCooldown;

		}

	}

}
