using UnityEngine;
using System.Collections;

public class PowerUpEnergyFullyOrb : PowerUp {

	protected override void Start() {

		PowerUpName = "Fully Restore Energy Orb";

	}

	public override void Give()
	{

		Player.Current.RestoreEnergyFully ();

		base.Give ();

	}

}
