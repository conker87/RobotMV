using UnityEngine;
using System.Collections;

public class PowerUpWeaponSpinner : PowerUp {

	public override void Give()
	{
		
		Player.Current.WeaponSpinner = true;

		base.Give ();

	}

}