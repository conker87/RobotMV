using UnityEngine;
using System.Collections;

public class ItemEnergyShield : Item {
	
	ProjectileEnergyShield p;
	Projectile projectile;

	public override void Use () {

		if (Player.Current.inputManager.GetButtonDown ("Item")) {

			if (projectile != null) {

				if (projectile.gameObject.activeSelf) {

					projectile.gameObject.SetActive (false);

				} else {

					projectile.gameObject.SetActive (true);

				}

				return;

			}

			int random = Random.Range (0, Projectiles.Length);

			projectile = Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity) as Projectile;

			projectile.transform.SetParent (transform);

		}

	}



}