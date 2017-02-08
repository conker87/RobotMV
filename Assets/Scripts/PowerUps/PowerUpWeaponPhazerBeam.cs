using UnityEngine;
using System.Collections;

public class PowerUpWeaponPhazerBeam : PowerUp {

	public override void Give() {
		
		Player.Current.Weapon_PhazerBeam = true;

		base.Give ();

	}

}