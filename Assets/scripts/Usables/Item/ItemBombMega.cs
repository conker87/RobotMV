using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	public override void Use () {

		if (!Player.Current.BOMBS_MEGA_INFINITE || Player.Current.Bombs_Mega_Current < 1) {

			return;

		}

		if (Time.time > nextShotTime) {

			int random = Random.Range (0, Projectiles.Length);

			Instantiate (Projectiles [random], Player.Current.transform.position, Quaternion.identity);

			nextShotTime = Time.time + InitialCooldown;

			Player.Current.Bombs_Mega_Current--;

		}

	}

}