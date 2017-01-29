using UnityEngine;
using System.Collections;

public class PowerUpWeaponSpinner : PowerUp {

	public override void Give()
	{
		
		Player.Current.Weapon_Spinner = true;

		base.Give ();

	}

}