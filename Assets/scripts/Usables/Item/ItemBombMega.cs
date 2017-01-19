using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	public override void Use () {

		if (!Player.Current.CheatsD ["INFINITE_BOMBS_MEGA"] || Player.Current.BombsD ["BOMBS_MEGA_CURRENT"] < 1) {

			return;

		}

		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);

			Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity);

			nextShotTime = Time.time + Cooldown;

			Player.Current.BombsD ["BOMBS_MEGA_CURRENT"]++;

		}

	}

}