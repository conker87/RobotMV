using UnityEngine;
using System.Collections;

public class WeaponPhazerBeam : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {
		
		if (!Player.Current.Weapon_PhazerBeam) {

			return;

		}

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.Weapon_PhazerBeam_DamageMod);

			ProjectileBase projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as ProjectileBase;
			projectile.SetSettings (Direction, 3f, true, projectileType, CurrentDamage, Level);

			// Prevent firing again until after cooldown time
			CurrentCooldown = InitialCooldown * Player.Current.Weapon_PhazerBeam_CooldownMod;
			cooldownTime = Time.time + CurrentCooldown;

		}

	}

}
