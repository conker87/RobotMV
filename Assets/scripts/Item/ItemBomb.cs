using UnityEngine;
using System.Collections;

public class ItemBomb : Item {



	public override void Use () {

		if (Input.GetKeyUp (KeyCode.F)) {
		
			if (Time.time > nextShotTime) {

				base.Use ();

				Instantiate (Projectile, Player.Current.position, Quaternion.identity);

				nextShotTime = Time.time + Cooldown;

			}

		}
	}

}