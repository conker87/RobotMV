using UnityEngine;
using System.Collections;

public class WeaponBasicBlaster : Weapon {

	[Header("Basic Blaster Append Settings")]
	public string		BasicBlasterChargedShotID = "FIXME";

	public int			ChargedShotMultiplier = 2;

	public Projectile	ChargedShotProjectile;

	// Cooldown & Attack Length Time.time vars.
	[SerializeField]
	float chargedShotTimer, chargedShotChargeTime = .5f;
	bool fireChargedShot = false;

	public override void ShootMouse (Vector3 ShootLocationPosition, Vector2 Direction) {

		if (!Player.Current.Weapon_BasicBlaster) {

			return;

		}

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.Weapon_BasicBlaster_DamageMod);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;
			projectile.SetSettings (Direction, InitialProjectileMovementSpeed, false, projectileType, CurrentDamage, Level);

			// Prevent firing again until after cooldown time
			CurrentCooldown = InitialCooldown * Player.Current.Weapon_BasicBlaster_CooldownMod;
			cooldownTime = Time.time + CurrentCooldown;

		}

		// Blaster Charged Shot
		if (!Player.Current.Weapon_BasicBlaster_ChargedShot) {

			return;

		}

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

			Projectile projectile = Instantiate (ChargedShotProjectile, ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.name = projectile.name + "_ChargedShot";
			projectile.transform.localScale *= (ChargedShotMultiplier / 1.5f);

			int currentDamage = Mathf.RoundToInt (InitialDamage * ChargedShotMultiplier * Player.Current.Weapon_BasicBlaster_DamageMod);

			projectile.SetSettings (Direction, InitialProjectileMovementSpeed * (ChargedShotMultiplier / 2f), false, projectileType, currentDamage, Level * ChargedShotMultiplier);

		}

	}

}