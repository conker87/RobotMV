using UnityEngine;
using System.Collections;

public class PowerUpWeaponBasicBlaster : PowerUp {

	public override void Give() {
		
		Player.Current.WeaponBasicBlaster = true;

		base.Give ();

	}

}