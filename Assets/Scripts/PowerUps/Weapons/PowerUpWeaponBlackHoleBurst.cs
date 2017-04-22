using UnityEngine;
using System.Collections;

public class PowerUpWeaponBlackHoleBurst : PowerUp {
	
	public override void Give() {
		
		Player.Current.WeaponBlackHoleBurst = true;

		base.Give ();

	}

}