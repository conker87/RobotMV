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

		UsableName = LanguageManager.Instance.GetTextValue ("ITEM_BasicBlasterName");
		Description = LanguageManager.Instance.GetTextValue ("ITEM_BasicBlasterDescription");

	}

	public override void Shoot(Vector3 ShootLocationPosition) {

		base.Shoot (ShootLocationPosition);

		if (disabledDueToPenalty) {

			return;

		}

		// Blaster Shot
		if (Input.GetMouseButtonDown (0)) {

			if (Time.time > nextShotTime) {

				if (Player.Current.EnergyTanks > 0 || Player.Current.Energy >= EnergyCost) {

					mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

					int random = Random.Range (0, Projectiles.Length);

					Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

					projectile.Direction = directionToMousePositionInWorld;
					projectile.projectileDamage =	DamagePerTick;
					projectile.weaponLevel = Level;
					projectile.projectileType = projectileType;

					ShootEnd (EnergyCost);

				}

			}

		}

		// Blaster Charged Shot
		if (Player.Current.BasicBlasterChargeShot) {

			if (Input.GetMouseButton (0)) {

				if (Time.time > nextShotTime) {

					if (Player.Current.EnergyTanks > 1 || Player.Current.Energy >= EnergyCost * chargedShotMultiplier) {
					
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
