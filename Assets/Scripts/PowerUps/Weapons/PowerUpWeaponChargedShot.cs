using UnityEngine;
using System.Collections;

public class PowerUpWeaponChargedShot : PowerUp {

	public override void Give() {
		
		Player.Current.WeaponBasicBlasterChargedShot = true;

		base.Give ();

	}

}