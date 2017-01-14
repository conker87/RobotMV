using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	protected override void Update () {

		base.Update ();

	}

	public override void Shoot(Vector3 ShootLocationPosition) {

		if (stillCoolingDown) {

			return;

		}

		base.Shoot (ShootLocationPosition);

		if (Input.GetMouseButtonDown (0)) {

			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2) ShootLocationPosition;
	
			int random = Random.Range (0, Projectiles.Length);

			Projectile projectile = Instantiate (Projectiles [random], ShootLocationPosition, Quaternion.Euler( directionToMousePositionInWorld )) as Projectile;

			projectile.Direction = directionToMousePositionInWorld;

			//projectile.transform.rotation = Quaternion.Euler( directionToMousePositionInWorld );

			projectile.projectileDamage =	Damage;
			projectile.weaponLevel = Level;
			projectile.projectileType = projectileType;

			ShootEnd ();

		}

	}

}
