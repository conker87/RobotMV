using UnityEngine;
using System.Collections;

public class ItemEnergyShield : Item {
	
	Projectile projectile;

	public override void Use () {

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