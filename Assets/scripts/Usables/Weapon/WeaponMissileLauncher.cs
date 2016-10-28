using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void Awake() {

		base.Awake ();

	}
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		if (Player.Current.MissileLauncher) {

			if (Input.GetMouseButtonDown (0) && ((Player.Current.EnergyTanks > 1) || (Player.Current.Energy >= EnergyCost))) {
			
				if (Time.time > nextShotTime) {

					mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;
				
					Projectile projectile = Instantiate (Projectile, ShootLocationPosition, Quaternion.identity) as Projectile;

					projectile.Direction =			directionToMousePositionInWorld;
					projectile.projectileDamage =	DamagePerTick;
					projectile.weaponLevel = 		Level;
					projectile.projectileType =		projectileType;


					ShootEnd (EnergyCost);

				}

			}

		}

	}

}
