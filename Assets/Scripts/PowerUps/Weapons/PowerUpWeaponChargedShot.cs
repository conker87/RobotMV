using UnityEngine;
using System.Collections;

public class PowerUpWeaponChargedShot : PowerUp {

	public override void Give() {
		
		Player.Current.Weapon_BasicBlaster_ChargedShot = true;

		base.Give ();

	}

}