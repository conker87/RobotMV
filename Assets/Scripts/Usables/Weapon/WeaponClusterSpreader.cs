using UnityEngine;
using System.Collections;

public class WeaponClusterSpreader : Weapon {

	public float shootingAngle;

	public override void Shoot(Vector3 ShootLocationPosition) {

		base.Shoot (ShootLocationPosition);

		if (disabledDueToPenalty) {

			return;

		}

		if (Input.GetMouseButton (0)) {

			Player.Current.CanChangeWeapon = false;

			if (Time.time > nextShotTime) {

				if (Player.Current.EnergyTanks > 0 || Player.Current.Energy >= EnergyCost) {

					float radians = shootingAngle * (Mathf.PI / 180f);

					mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

					int random = Random.Range (0, Projectiles.Length);

					Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

					float randomXDirection = Random.Range (directionToMousePositionInWorld.x - radians, directionToMousePositionInWorld.x + radians),
					randomYDirection = Random.Range (directionToMousePositionInWorld.y - radians, directionToMousePositionInWorld.y + radians);

					directionToMousePositionInWorld = new Vector2 (randomXDirection, randomYDirection);

					projectile.Direction = directionToMousePositionInWorld;
					projectile.projectileDamage =	DamagePerTick;
					projectile.weaponLevel = Level;
					projectile.projectileType = projectileType;

					ShootEnd (EnergyCost);

				}

			}

		} else {
			
			Player.Current.CanChangeWeapon = true;

		}

	}

}
