﻿using UnityEngine;
using System.Collections;

public class WeaponSpinner : Weapon {

	[SerializeField]
	float spinnerTimer, spinnerTimerMax = 2f, multiplier;
	bool hasPressed = false, startEnergy = false;
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		base.Shoot (ShootLocationPosition);

		if (disabledDueToPenalty) {

			return;

		}

		if (Input.GetMouseButton (0)) {

			if (Time.time > nextShotTime) {
				
				if (startEnergy || (Player.Current.EnergyTanks > 0 || Player.Current.Energy >= (EnergyCost * multiplier))) {
				
					spinnerTimer += Time.deltaTime;
					multiplier = 1f + spinnerTimer;

					hasPressed = true;
					startEnergy = true;

				}

			}

		}

		if (Input.GetMouseButtonUp (0) && hasPressed) {

			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

			spinnerTimer = Mathf.Clamp (spinnerTimer, 0f, spinnerTimerMax);

			foreach (Projectile p in Projectiles) {

				ProjectileSpinner projectile = Instantiate (p, ShootLocationPosition, Quaternion.identity) as ProjectileSpinner;

				projectile.name = projectile.name + "_" + spinnerTimer;

				projectile.transform.SetParent (transform);

				projectile.transform.localScale		*= multiplier;
				projectile.Direction 				= directionToMousePositionInWorld;
				projectile.projectileDamage 		= DamagePerTick * multiplier;
				projectile.weaponLevel 				= Level;
				projectile.projectileType 			= projectileType;
				projectile.timesThroughEnemyMax 	= Mathf.RoundToInt (spinnerTimer * 2f);
				projectile.ignoreGeometry 			= (spinnerTimer > (spinnerTimerMax / 2f)) ? true : false;
				projectile.movementSpeed 			*=	multiplier;

				projectile.GetComponent<RotateAtSpeed> ().rotationalSpeed *= (multiplier + 1f);

				ShootEnd (EnergyCost);

			}

			spinnerTimer = 0f;

			nextShotTime = Time.time + AttackSpeed;
			Player.Current.DamageEnergy (EnergyCost * multiplier);

			hasPressed = false;
			startEnergy = false;

		}

		if (!Input.GetMouseButton (0)) {
			spinnerTimer = 0f;
		}

	}

}
