using UnityEngine;
using System.Collections;

public class WeaponSpinner : Weapon {

	[SerializeField]
	float spinnerTimer, spinnerTimerMax = 2f;
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		if (Player.Current.Spinner) {
			
			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2) ShootLocationPosition;

			// Spinner 
			if (Input.GetMouseButton (0) && ((Player.Current.EnergyTanks > 1) || (Player.Current.Energy >= EnergyCost))) {

				spinnerTimer += Time.deltaTime;

			}

			if (Input.GetMouseButtonUp (0)) {
	
				spinnerTimer = Mathf.Clamp (spinnerTimer, 0f, spinnerTimerMax);

				float multiplier = 1f + spinnerTimer;

				ProjectileSpinner projectile = Instantiate (Projectile, ShootLocationPosition, Quaternion.identity) as ProjectileSpinner;

				projectile.name = projectile.name + "_" + spinnerTimer;

				projectile.transform.SetParent (transform);

				projectile.transform.localScale	*=	multiplier;
				projectile.Direction =				directionToMousePositionInWorld;
				projectile.projectileDamage =		DamagePerTick * multiplier;
				projectile.weaponLevel =			Level;
				projectile.projectileType =			projectileType;
				projectile.timesThroughEnemyMax = 	Mathf.RoundToInt (spinnerTimer);
				projectile.movementSpeed *=			multiplier;

				projectile.GetComponent<RotateAtSpeed> ().rotationalSpeed *= multiplier;

				spinnerTimer = 0f;

				nextShotTime = Time.time + AttackSpeed;
				Player.Current.DamageEnergy (EnergyCost);

			}

		}

	}

}
