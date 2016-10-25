using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void Awake() {

		base.Awake ();

	}
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		if (Input.GetMouseButtonDown (0)) {
			
			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2) ShootLocationPosition;

			if (Time.time > nextShotTime && Player.Current.Energy > EnergyCost) {

				GameObject projectile = Instantiate (Projectile, ShootLocationPosition, Quaternion.identity) as GameObject;

				if (projectile != null) {
				
					Projectile projectileComp = projectile.GetComponent<Projectile> ();

					//projectileComp.transform.LookAt (new Vector3(mousePositionToWorld.x, 0f, 0f));
					projectileComp.Direction = directionToMousePositionInWorld;
					projectileComp.projectileDamage = DamagePerTick;
					projectileComp.weaponLevel = Level;
					projectileComp.projectileType = projectileType;

				}

				ShootEnd (EnergyCost);

			}

		}

	}

}
