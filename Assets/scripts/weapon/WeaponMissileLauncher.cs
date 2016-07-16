using UnityEngine;
using System.Collections;

public class WeaponMissileLauncher : Weapon {

	public override void Awake() {

		base.Awake ();

	}
		
	public override void Shoot(GameObject shootLocation, Vector3 direction) {

		Debug.Log (Time.time + (1 / AttackSpeed));

		if (Time.time > nextShotTime) {

			GameObject projectile = Instantiate (Projectile, shootLocation.transform.position, Quaternion.identity) as GameObject;

			Debug.Log ("shootLocation: " + shootLocation.transform.position.ToString() + ", direction: " + direction.ToString());

			if (projectile != null) {

				projectile.GetComponent<Projectile>().Direction = direction.normalized;

			}

			nextShotTime = Time.time + AttackSpeed;

		}

	}

}
