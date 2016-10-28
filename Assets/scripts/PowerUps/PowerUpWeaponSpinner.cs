using UnityEngine;
using System.Collections;

public class PowerUpWeaponSpinner : PowerUp
{

	protected override void Start() {

		PowerUpName = "Energy Spinner";

	}

	public override void Give()
	{
		
		Player.Current.Spinner = true;

		base.Give ();

	}

}