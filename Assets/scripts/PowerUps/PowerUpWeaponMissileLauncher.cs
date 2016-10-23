using UnityEngine;
using System.Collections;

public class PowerUpWeaponMissileLauncher : PowerUp
{

	protected override void Start() {

		PowerUpName = "Missile Launcher";

	}

	public override void Give()
	{
		
		Player.Current.MissileLauncher = true;

		base.Give ();

	}

}