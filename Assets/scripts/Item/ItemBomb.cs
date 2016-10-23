using UnityEngine;
using System.Collections;

public class ItemBomb : Item {

	public override void Use () {

		if (Player.Current.inputManager.GetButtonDown("Bomb") && Player.Current.Bombs > 0) {

			Player.Current.Bombs--;
			Instantiate (Projectile, Player.Current.position, Quaternion.identity);

		}
	}

}