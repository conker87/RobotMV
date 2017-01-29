﻿using UnityEngine;
using System.Collections;

public class WeaponSplitter : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {
		
		if (!Player.Current.Weapon_Splitter) {

			return;

		}

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.SetSettings (Direction, 3f, true, projectileType, Damage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + Cooldown;

		}

	}

}
