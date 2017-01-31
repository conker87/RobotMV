using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (!Player.Current.Weapon_MissileLauncher) {

			return;

		}

		// TODO: Change to InputManager.Current.GetButton("Fire Weapon")
		if (InputManager.Current.GetButtonDown("Fire Weapon") || Input.GetMouseButtonDown (0)) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.Weapon_MissileLauncher_DamageMod);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;
			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, true, projectileType, CurrentDamage, Level);

			// Prevent firing again until after cooldown time
			CurrentCooldown = InitialCooldown * Player.Current.Weapon_MissileLauncher_CooldownMod;
			cooldownTime = Time.time + InitialCooldown;

		}

	}

}
