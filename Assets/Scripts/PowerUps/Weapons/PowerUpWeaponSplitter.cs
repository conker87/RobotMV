using UnityEngine;
using System.Collections;

public class PowerUpWeaponSplitter : PowerUp {

	public override void Give() {
		
		Player.Current.WeaponSplitter = true;

		base.Give ();

	}

}