using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void ShootMouse(Vector3 ShootLocationPosition) {

		if (Input.GetMouseButtonDown (0)) {

			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2) ShootLocationPosition;
	
			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.identity) as Projectile;

			projectile.SetSettings (directionToMousePositionInWorld, InitialProjectileMovementSpeed, true, projectileType, Damage, Level);

			// Prevent firing again until after cooldown time
			cooldownTime = Time.time + Cooldown;

		}

	}

}
