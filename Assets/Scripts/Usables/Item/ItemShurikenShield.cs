using UnityEngine;
using System.Collections;

public class ItemShurikenShield : Item {

	public override void Use () {

		// TODO: Needs to use Cooldown.

		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);
			Vector3 pos = (transform.position - Player.Current.transform.position).normalized * 1f + Player.Current.transform.position;

			Projectile projectile = Instantiate (Projectiles [random], pos, Quaternion.identity) as Projectile;

			projectile.transform.SetParent (transform);
			projectile.WeaponLevel = Level;

			nextShotTime = Time.time + AttackLength;

		}

	}

}