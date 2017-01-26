using UnityEngine;
using System.Collections;

public class WeaponBasicBlaster : Weapon {

	[Header("Basic Blaster Append Settings")]
	public string		BasicBlasterChargedShotID = "FIXME";

	public float		chargedShotMultiplier;
	public int			chargedShotLevel;

	public Projectile	chargedShotProjectile;

	// Cooldown & Attack Length Time.time vars.
	[SerializeField]
	float chargedShotTimer, chargedShotChargeTime = .5f;
	bool fireChargedShot = false;

	public override void ShootMouse (Vector3 ShootLocationPosition, Vector2 Direction) {

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, false, projectileType, Damage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + Cooldown;

		}

		// Blaster Charged Shot
		if (Player.Current.CollectablesD[BasicBlasterChargedShotID]) {

			if (InputManager.Current.GetButton("Fire Weapon")) {

				// Checks to see if the timer is less than the time set to fully charge
				if (chargedShotTimer < chargedShotChargeTime && !fireChargedShot) {

					chargedShotTimer += Time.deltaTime;
					fireChargedShot = false;

				}

				// Checks to see if the timer is more than the time.
				if (chargedShotTimer >= chargedShotChargeTime) {

					chargedShotTimer = 0f;

					fireChargedShot = true;

				}

			} else {

				chargedShotTimer = 0f;

			}

			if (InputManager.Current.GetButtonUp("Fire Weapon") && fireChargedShot) {

				chargedShotTimer = 0f;

				fireChargedShot = false;

				Projectile projectile = Instantiate (chargedShotProjectile, ShootLocationPosition, Quaternion.identity) as Projectile;

				projectile.name = projectile.name + "_ChargedShot";
				projectile.transform.localScale *= chargedShotMultiplier / 2f;

				projectile.SetSettings (Direction, InitialProjectileMovementSpeed * (chargedShotMultiplier / 2f), false, projectileType, Mathf.RoundToInt(Damage * chargedShotMultiplier), chargedShotLevel);

			}

		}

	}

}