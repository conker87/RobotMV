using UnityEngine;
using System.Collections;

public class PowerUpWeaponBasicBlaster : PowerUp {

	public override void Give() {
		
		Player.Current.Weapon_BasicBlaster = true;

		base.Give ();

	}

}