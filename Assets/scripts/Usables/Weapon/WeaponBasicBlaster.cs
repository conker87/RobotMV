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

	public override void ShootMouse (Vector3 ShootLocationPosition) {
		
		base.ShootMouse (ShootLocationPosition);

		// Blaster Charged Shot
		if (Player.Current.CollectablesD[BasicBlasterChargedShotID]) {

			if (Input.GetMouseButton (0)) {

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

			if (Input.GetMouseButtonUp (0) && fireChargedShot) {

				chargedShotTimer = 0f;

				fireChargedShot = false;

				mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

				Projectile projectile = Instantiate (chargedShotProjectile, ShootLocationPosition, Quaternion.identity) as Projectile;

				projectile.name = projectile.name + "_ChargedShot";
				projectile.transform.localScale *= chargedShotMultiplier / 2f;

				projectile.SetSettings (directionToMousePositionInWorld, InitialProjectileMovementSpeed * (chargedShotMultiplier / 2f), false, projectileType, Mathf.RoundToInt(Damage * chargedShotMultiplier), chargedShotLevel);

			}

		}

	}

}