using UnityEngine;
using System.Collections;

public class WeaponBasicBlaster : Weapon {

	/// *****************************************************
	/// I've decided to allow the player to collect Charge Shot without the need
	/// for the basic blaster, if people want to glitch, then let them.
	/// *****************************************************

	public float chargedShotMultiplier;
	public int chargedShotLevel;

	public Projectile	chargedShotProjectile;

	float chargedShotTimer, chargedShotTime = .5f;
	bool fireChargedShot = false;
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		// Basic Blaster
		if (Player.Current.BasicBlaster) {

			// Blaster Shot
			if (Input.GetMouseButtonDown (0) && (((Player.Current.EnergyTanks > 1) || (Player.Current.Energy >= EnergyCost)))) {

				if (Time.time > nextShotTime) {

					mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

					foreach (Projectile p in Projectiles) {

						Projectile projectile = Instantiate (p, ShootLocationPosition, Quaternion.identity) as Projectile;

						projectile.Direction = directionToMousePositionInWorld;
						projectile.projectileDamage =	DamagePerTick;
						projectile.weaponLevel = Level;
						projectile.projectileType = projectileType;

						ShootEnd (EnergyCost);

					}

				}

			}

		}

		// Blaster Charged Shot
		if (Player.Current.BasicBlasterChargeShot) {

			if (Input.GetMouseButton (0) && ((Player.Current.EnergyTanks > 1 || Player.Current.Energy >= EnergyCost * chargedShotMultiplier))) {

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

					mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

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
