using UnityEngine;
using System.Collections;

public class WeaponSpinner : Weapon {

	[SerializeField]
	float spinnerTimer, spinnerTimerMax = 2f, multiplier;
	bool hasPressed = false;

	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (!Player.Current.Weapon_Spinner) {

			return;

		}

		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			hasPressed = true;

		}

		// TODO: Change to InputManager.Current.GetButton("Fire Weapon")
		if (InputManager.Current.GetButton("Fire Weapon") && hasPressed) {

			spinnerTimer += Time.deltaTime;

		}

		// TODO: Change to InputManager.Current.GetButton("Fire Weapon")
		if (InputManager.Current.GetButtonUp("Fire Weapon") && hasPressed) {

			spinnerTimer = Mathf.Clamp (spinnerTimer, 0f, spinnerTimerMax);
			multiplier = 1f + spinnerTimer;

			int random = Random.Range (0, Projectiles.Length);
			CurrentDamage = Mathf.RoundToInt (InitialDamage * Player.Current.Weapon_Spinner_DamageMod);

			SpinnerProjectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as SpinnerProjectile;

			projectile.name = projectile.name + "_" + spinnerTimer;
			projectile.transform.SetParent (transform);
			projectile.transform.localScale	*= multiplier;

			bool doesIgnoreGeometry = (spinnerTimer > (spinnerTimerMax / 2f)) ? true : false;

			projectile.SetSettings (Direction, InitialProjectileMovementSpeed * multiplier, false, projectileType, Mathf.RoundToInt (CurrentDamage * multiplier),
				Level, doesIgnoreGeometry, true);
				
			projectile.timesThroughEnemyMax = Mathf.RoundToInt (spinnerTimer * 2f);

			projectile.GetComponent<RotateAtSpeed> ().rotationalSpeed *= (multiplier + 1f);

			// Prevent firing again until after cooldown time
			CurrentCooldown = InitialCooldown * Player.Current.Weapon_Spinner_CooldownMod;
			cooldownTime = Time.time + CurrentCooldown;

			spinnerTimer = 0f;

			hasPressed = false;

		}

		if (!InputManager.Current.GetButton("Fire Weapon")) {

			spinnerTimer = 0f;

			hasPressed = false;

		}

	}

}
