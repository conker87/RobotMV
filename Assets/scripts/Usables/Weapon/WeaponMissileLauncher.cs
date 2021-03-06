﻿using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (!Player.Current.WeaponMissileLauncher) {

			return;

		}

		// TODO: Change to InputManager.Current.GetButton("Fire Weapon")
		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.WeaponMissileLauncherDamageMod);

			ProjectileBase projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as ProjectileBase;
			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, true, projectileType, CurrentDamage, Level);

			// Prevent firing again until after cooldown time
			CurrentCooldown = InitialCooldown * Player.Current.WeaponMissileLauncherCooldownMod;
			cooldownTime = Time.time + InitialCooldown;

		}

	}

}
