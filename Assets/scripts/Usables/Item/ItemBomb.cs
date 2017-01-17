using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	public override void Use () {

		if (Player.Current.inputManager.GetButtonDown ("Item")) {

			if (!Player.Current.CheatsD ["INFINITE_BOMBS"] || Player.Current.BombsD ["BOMBS_CURRENT"] < 1) {

				return;

			}

			if (Time.time > nextShotTime) {

				int random = Random.Range (0, Projectiles.Length);

				Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity);

				nextShotTime = Time.time + Cooldown;

				Player.Current.BombsD ["BOMBS_CURRENT"]++;

			}

		}

	}

}