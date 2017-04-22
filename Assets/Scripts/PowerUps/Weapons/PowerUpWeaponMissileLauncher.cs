using UnityEngine;
using System.Collections;

public class PowerUpWeaponMissileLauncher : PowerUp {
	
	public override void Give() {
		
		Player.Current.WeaponMissileLauncher = true;

		base.Give ();

	}

}