using UnityEngine;
using System.Collections;

public class ItemBombMega : Item {

	public override void Use () {

		if (Time.time > nextShotTime && Player.Current.inputManager.GetButtonDown("Item") && Player.Current.MegaBombs > 0) {

			Player.Current.MegaBombs--;
			Instantiate (Projectile, Player.Current.position, Quaternion.identity);

			nextShotTime = Time.time + AttackSpeed;

		}
	}

}