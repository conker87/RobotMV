using UnityEngine;
using System.Collections;

public class PowerUpWeaponSplitter : PowerUp {

	public override void Give() {
		
		Player.Current.Weapon_Splitter = true;

		base.Give ();

	}

}