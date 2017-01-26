using UnityEngine;
using System.Collections;

public class WeaponSpinner : Weapon {

	[SerializeField]
	float spinnerTimer, spinnerTimerMax = 2f, multiplier;
	bool hasPressed = false;

	public override void Shoot(Vector3 ShootLocationPosition, Vector2 Direction) {

		base.Shoot (ShootLocationPosition, Direction);

		// TODO: Change to InputManager.Current.GetButton("Fire Weapon")
		if (Input.GetMouseButton (0)) {

			if (Time.time > cooldownTime) {
				
				spinnerTimer += Time.deltaTime;
				multiplier = 1f + spinnerTimer;

				hasPressed = true;

			}

		}

		// TODO: Change to InputManager.Current.GetButton("Fire Weapon")
		if (Input.GetMouseButtonUp (0) && hasPressed) {

			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

			spinnerTimer = Mathf.Clamp (spinnerTimer, 0f, spinnerTimerMax);

			foreach (Projectile p in Projectiles) {

				ProjectileSpinner projectile = Instantiate (p, ShootLocationPosition, Quaternion.identity) as ProjectileSpinner;

				projectile.name = projectile.name + "_" + spinnerTimer;

				projectile.transform.SetParent (transform);
				projectile.transform.localScale	*= multiplier;

				bool doesIgnoreGeometry = (spinnerTimer > (spinnerTimerMax / 2f)) ? true : false;

				projectile.SetSettings (directionToMousePositionInWorld, InitialProjectileMovementSpeed * multiplier, false, projectileType, Mathf.RoundToInt (Damage * multiplier * 5f),
					Level, doesIgnoreGeometry, true);
					
				projectile.timesThroughEnemyMax = Mathf.RoundToInt (spinnerTimer * 2f);

				projectile.GetComponent<RotateAtSpeed> ().rotationalSpeed *= (multiplier + 1f);

				// Prevent firing again until after cooldown time
				cooldownTime = Time.time + Cooldown;

			}

			spinnerTimer = 0f;

			hasPressed = false;

		}

		if (!Input.GetMouseButton (0)) {
			spinnerTimer = 0f;
		}

	}

}
