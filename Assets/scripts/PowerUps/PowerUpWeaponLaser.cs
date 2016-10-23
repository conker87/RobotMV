using UnityEngine;
using System.Collections;

public class PowerUpWeaponLaser : PowerUp {

	protected override void Start() {

		PowerUpName = "Laser Beam";

	}

	public override void Give()
	{
		
		Player.Current.Laser = true;

		base.Give ();

	}

}