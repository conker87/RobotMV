using UnityEngine;
using System.Collections;

// Removed from features as of 2017-04-22
public class ItemShurikenShield : Item {

	public override void Use () {

		if (stillOnCooldown) {

			return;

		}

		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);
			Vector3 pos = (transform.position - Player.Current.transform.position).normalized * 1f + Player.Current.transform.position;

			ProjectileBase projectile = Instantiate (Projectiles [random], pos, Quaternion.identity) as ProjectileBase;

			projectile.transform.SetParent (transform);
			projectile.WeaponLevel = Level;

			nextShotTime = Time.time + InitialAttackLength;

		}

	}

}