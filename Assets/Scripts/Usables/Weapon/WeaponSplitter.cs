using UnityEngine;
using System.Collections;

public class WeaponSplitter : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {
		
		if (!Player.Current.Weapon_Splitter) {

			return;

		}

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.Weapon_Splitter_DamageMod);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;
			projectile.SetSettings (Direction, 3f, true, projectileType, CurrentDamage, Level);

			ProjectileChild[] children = projectile.GetComponentsInChildren<ProjectileChild> ();

			foreach (ProjectileChild c in children) {

				c.ProjectileDamage = CurrentDamage;

			}

			// Prevent firing again until after cooldown time
			CurrentCooldown = InitialCooldown * Player.Current.Weapon_Splitter_CooldownMod;
			cooldownTime = Time.time + CurrentCooldown;

		}

	}

}
