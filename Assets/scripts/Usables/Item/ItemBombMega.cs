using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	public override void Use () {

		if (Player.Current.inputManager.GetButtonDown("Item")) {

			if (Player.Current.INFINITE_MEGABOMBS || Player.Current.MegaBombsCount > 0) {

				if (Time.time > nextShotTime) {

					int random = Random.Range (0, Projectiles.Length);

					Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity);

					nextShotTime = Time.time + Cooldown;

					Player.Current.MegaBombsCount--;

				}

			}

		}

	}

}