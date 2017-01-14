using UnityEngine;
using System.Collections;
using SmartLocalization;

public class WeaponBasicBlaster : Weapon {

	public float chargedShotMultiplier;
	public int chargedShotLevel;

	public Projectile	chargedShotProjectile;

	float chargedShotTimer, chargedShotTime = .5f;
	bool fireChargedShot = false;

	protected override void Update ()
	{

		base.Update ();

	}

	public override void Shoot(Vector3 ShootLocationPosition) {

		if (stillCoolingDown) {

			return;

		}

		base.Shoot (ShootLocationPosition);

		// Blaster Shot
		if (Input.GetMouseButtonDown (0)) {

			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.Direction = directionToMousePositionInWorld;
			projectile.projectileDamage =	Damage;
			projectile.weaponLevel = Level;
			projectile.projectileType = projectileType;

			ShootEnd ();

		}

		// Blaster Charged Shot
		if (Player.Current.BasicBlasterChargeShot) {

			if (Input.GetMouseButton (0)) {
					
				if (chargedShotTimer < chargedShotTime && !fireChargedShot) {

					chargedShotTimer += Time.deltaTime;
					fireChargedShot = false;

				}

				if (chargedShotTimer >= chargedShotTime) {
		
					chargedShotTimer = 0f;

					fireChargedShot = true;

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
					projectile.projectileDamage =	Mathf.RoundToInt(Damage * chargedShotMultiplier);
					projectile.projectileType =		projectileType;
					projectile.weaponLevel =		chargedShotLevel;

				}

			}

		}

	}

}
