using UnityEngine;
using System.Collections;

public class WeaponClusterSpreader : Weapon {

	// TODO: Decide what to do with this weapon as it will not work in this setting.

	public float shootingAngle;

	public override void ShootMouse(Vector3 ShootLocationPosition) {

		if (Input.GetMouseButton (0)) {

			Player.Current.CanChangeWeapon = false;

			float radians = shootingAngle * (Mathf.PI / 180f);

			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

			int random = Random.Range (0, Projectiles.Length);

			float randomXDirection = Random.Range (directionToMousePositionInWorld.x - radians, directionToMousePositionInWorld.x + radians),
					randomYDirection = Random.Range (directionToMousePositionInWorld.y - radians, directionToMousePositionInWorld.y + radians);
			directionToMousePositionInWorld = new Vector2 (randomXDirection, randomYDirection);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.SetSettings (directionToMousePositionInWorld, InitialProjectileMovementSpeed, false, projectileType, Damage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + Cooldown;

		} else {
			
			Player.Current.CanChangeWeapon = true;

		}

	}

}
