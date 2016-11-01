using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void Shoot(Vector3 ShootLocationPosition) {

		if (Player.Current.MissileLauncher) {

			if (Input.GetMouseButtonDown (0)) {
			
				if (Time.time > nextShotTime) {

					if (Player.Current.EnergyTanks > 0 || Player.Current.Energy >= EnergyCost) {

						mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;
				
						int random = Random.Range (0, Projectiles.Length);

						Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

						projectile.Direction = directionToMousePositionInWorld;
						projectile.projectileDamage =	DamagePerTick;
						projectile.weaponLevel = Level;
						projectile.projectileType = projectileType;

						ShootEnd (EnergyCost);

					}

				}

			}

		}

	}

}
