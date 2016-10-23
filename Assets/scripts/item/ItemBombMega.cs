using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	public override void Use () {

		if (Time.time > nextShotTime && Player.Current.inputManager.GetButtonDown("Bomb") && Player.Current.MegaBombs > 0) {

			Player.Current.MegaBombs--;
			Projectile obj = Instantiate (Projectile, Player.Current.position, Quaternion.identity) as Projectile;

			nextShotTime = Time.time + Cooldown;

		}
	}

}