using UnityEngine;
using System.Collections;

public class PowerUpItemEnergyShield : PowerUp
{

	protected override void Start() {

		PowerUpName = "Energy Shield";

	}

	public override void Give()
	{
		
		Player.Current.EnergyShield = true;

		base.Give ();

	}

}