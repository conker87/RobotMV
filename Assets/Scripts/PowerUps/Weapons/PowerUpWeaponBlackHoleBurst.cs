using UnityEngine;
using System.Collections;

public class PowerUpWeaponBlackHoleBurst : PowerUp {
	
	public override void Give() {
		
		Player.Current.Weapon_BlackHoleBurst = true;

		base.Give ();

	}

}