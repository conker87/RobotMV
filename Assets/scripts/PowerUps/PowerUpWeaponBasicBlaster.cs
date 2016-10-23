using UnityEngine;
using System.Collections;

public class PowerUpWeaponBasicBlaster : PowerUp
{

	protected override void Start() {

		PowerUpName = "Basic Blaster";

	}

	public override void Give()
	{
		
		Player.Current.BasicBlaster = true;

		base.Give ();

	}

}