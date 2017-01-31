﻿using UnityEngine;
using System.Collections;

public class WeaponBlackHoleBurst : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (!Player.Current.Weapon_BlackHoleBurst) {

			return;

		}

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.Weapon_BlackHoleBurst_DamageMod);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;
			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, false, projectileType, CurrentDamage, Level);

			projectile.DestroyInSeconds = InitialAttackLength;

			// Prevent firing again until after cooldown time
			CurrentCooldown = InitialCooldown * Player.Current.Weapon_BlackHoleBurst_CooldownMod;
			cooldownTime = Time.time + CurrentCooldown;

		}

	}

}
