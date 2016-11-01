using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	public override void Use () {

		if (Player.Current.inputManager.GetButtonDown ("Item")) {

			if (Player.Current.Bombs > 0) {

				if (Time.time > nextShotTime) {

					int random = Random.Range (0, Projectiles.Length);

					Projectile projectile = Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity) as Projectile;
				
					nextShotTime = Time.time + AttackSpeed;
					Player.Current.Bombs--;

				}

			}

		}

	}

}