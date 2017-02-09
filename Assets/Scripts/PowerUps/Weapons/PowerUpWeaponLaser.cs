using UnityEngine;
using System.Collections;

public class PowerUpWeaponLaser : PowerUp {

	public override void Give() {
		
		Player.Current.Weapon_Laser = true;

		base.Give ();

	}

}