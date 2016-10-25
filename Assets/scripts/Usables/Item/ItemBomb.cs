using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	public override void Use () {

		if (Time.time > nextShotTime && Player.Current.inputManager.GetButtonDown("Item") && Player.Current.Bombs > 0) {

			Player.Current.Bombs--;
			Instantiate (Projectile, Player.Current.position, Quaternion.identity);

			nextShotTime = Time.time + AttackSpeed;

		}
	}

}