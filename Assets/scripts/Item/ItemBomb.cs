using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	public override void Use () {

		if (Time.time > nextShotTime && Player.Current.inputManager.GetButtonDown("Bomb") && Player.Current.Bombs > 0) {

			Player.Current.Bombs--;
			Projectile obj = Instantiate (Projectile, Player.Current.position, Quaternion.identity) as Projectile;

			nextShotTime = Time.time + Cooldown;

		}
	}

}