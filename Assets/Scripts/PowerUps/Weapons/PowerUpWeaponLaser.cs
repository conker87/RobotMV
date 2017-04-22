using UnityEngine;
using System.Collections;

public class PowerUpWeaponLaser : PowerUp {

	public override void Give() {
		
		Player.Current.WeaponLaser = true;

		base.Give ();

	}

}