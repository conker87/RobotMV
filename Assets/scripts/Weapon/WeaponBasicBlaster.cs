﻿using UnityEngine;
using System.Collections;

public class WeaponBasicBlaster : Weapon {

	public float chargedShotMultiplier = 3f;

	float chargedShotTimer = 0f, chargedShotTime = .5f;
	bool fireChargedShot = false;

	public override void Awake() {

		base.Awake ();

	}
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		directionToMousePositionInWorld = mousePositionToWorld - (Vector2) ShootLocationPosition;

		// Normal Shot
		if (Input.GetMouseButtonDown (0)) {

			if (Time.time > nextShotTime && Player.Current.Energy > EnergyCost) {

				GameObject projectile = Instantiate (Projectile, ShootLocationPosition, Quaternion.identity) as GameObject;

				if (projectile != null) {

					Projectile projectileComp = projectile.GetComponent<Projectile> ();

					projectileComp.Direction = directionToMousePositionInWorld;
					projectileComp.projectileDamage = damagePerTick;
					projectileComp.weaponLevel = weaponLevel;
					projectileComp.projectileType = projectileType;

				}

				ShootEnd ();

			}

		}
			
		if (Input.GetMouseButton (0)) {

			if (Time.time > nextShotTime && Player.Current.Energy > (EnergyCost * chargedShotMultiplier) && Player.Current.BasicBlasterChargeShot) {

				if (chargedShotTimer >= chargedShotTime) {
					
					chargedShotTimer = 0f;

					fireChargedShot = true;

				}

				if (chargedShotTimer < chargedShotTime && !fireChargedShot) {
					
					chargedShotTimer += Time.deltaTime;
					fireChargedShot = false;

				}

			}

		}

		if (Input.GetMouseButtonUp (0)) {
		
			chargedShotTimer = 0f;

			if (fireChargedShot) {

				GameObject projectile = Instantiate (Projectile, ShootLocationPosition, Quaternion.identity) as GameObject;

				if (projectile != null) {

					Projectile projectileComp = projectile.GetComponent<Projectile> ();

					projectileComp.name = projectileComp.name + "_ChargedShot";

					projectileComp.transform.localScale = new Vector2 (projectileComp.transform.localScale.x * 2, projectileComp.transform.localScale.y * 2);
					projectileComp.Direction = directionToMousePositionInWorld;
					projectileComp.projectileDamage = damagePerTick * chargedShotMultiplier;
					projectileComp.weaponLevel = weaponLevel;
					projectileComp.projectileType = projectileType;

				}

				ShootEnd ();

			}

			fireChargedShot = false;

		}

	}

}
