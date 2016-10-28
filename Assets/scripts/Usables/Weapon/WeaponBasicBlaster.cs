using UnityEngine;
using System.Collections;

public class WeaponBasicBlaster : Weapon {

	public float chargedShotMultiplier = 3f;
	public int chargedShotLevel = 3;

	public Projectile	chargedShotProjectile;

	float chargedShotTimer, chargedShotTime = .5f;
	bool fireChargedShot = false;
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		if (Player.Current.BasicBlaster) {
			
			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;
	
			// Blaster Shot
			if (Input.GetMouseButtonDown (0) && ((Player.Current.EnergyTanks > 1) || (Player.Current.Energy >= EnergyCost))) {

				if (Time.time > nextShotTime) {

//					GameObject projectile = Instantiate (Projectile, ShootLocationPosition, Quaternion.identity) as GameObject;
//
//					Projectile projectileComp = projectile.GetComponent<Projectile> ();
//
//					projectileComp.Direction =			directionToMousePositionInWorld;
//					projectileComp.projectileDamage =	DamagePerTick;
//					projectileComp.weaponLevel = 		Level;
//					projectileComp.projectileType =		projectileType;

					Projectile projectile = Instantiate (Projectile, ShootLocationPosition, Quaternion.identity) as Projectile;

					projectile.Direction =			directionToMousePositionInWorld;
					projectile.projectileDamage =	DamagePerTick;
					projectile.weaponLevel = 		Level;
					projectile.projectileType =		projectileType;

					ShootEnd (EnergyCost);

				}

			}

			// Blaster Charged Shot
			if (Player.Current.BasicBlasterChargeShot) {

				if (Input.GetMouseButton (0) && ((Player.Current.EnergyTanks > 1) || (Player.Current.Energy >= EnergyCost * chargedShotMultiplier))) {

					if (Time.time > nextShotTime) {
						
						if (chargedShotTimer < chargedShotTime && !fireChargedShot) {

							chargedShotTimer += Time.deltaTime;
							fireChargedShot = false;

						}

						if (chargedShotTimer >= chargedShotTime) {
					
							chargedShotTimer = 0f;

							fireChargedShot = true;

						}

					}

				}

				if (Input.GetMouseButtonUp (0)) {
		
					chargedShotTimer = 0f;

					if (fireChargedShot) {

						fireChargedShot = false;

						Projectile projectile = Instantiate (chargedShotProjectile, ShootLocationPosition, Quaternion.identity) as Projectile;

						projectile.name = projectile.name + "_ChargedShot";

						projectile.Direction =			directionToMousePositionInWorld;
						projectile.projectileDamage =	DamagePerTick * chargedShotMultiplier;
						projectile.projectileType =		projectileType;
						projectile.weaponLevel =		chargedShotLevel;

						ShootEnd (EnergyCost * chargedShotMultiplier);

					}

				}

			}

		}

	}

}
