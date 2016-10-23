using UnityEngine;
using System.Collections;

public class PowerUpEnergyOrb : PowerUp {

	public float value = 10f;

	protected override void Start() {

		PowerUpName = "Energy Orb";

	}

	public override void Give()
	{

		Player.Current.RestoreEnergy (value);

		base.Give ();

	}

}
