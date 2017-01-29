using UnityEngine;
using System.Collections;

public class PowerUpWeaponChargedShot : PowerUp {

	public override void Give() {
		
		Player.Current.Upgrade_BasicBlaster_ChargedShot = true;

		base.Give ();

	}

}