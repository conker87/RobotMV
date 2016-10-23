using UnityEngine;
using System.Collections;

public class PowerUpWeaponChargedShot : PowerUp
{

	protected override void Start() {

		PowerUpName = "Charged Shot";

	}

	public override void Give()
	{
		
		Player.Current.BasicBlasterChargeShot = true;

		base.Give ();

	}

}