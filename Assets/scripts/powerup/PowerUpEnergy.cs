using UnityEngine;
using System.Collections;

public class PowerUpEnergy : PowerUp {

	public float Energy = 10f;

	protected override void Start() {

		PowerUpName = "Give " + Energy + " Health";

	}

	public override void GivePowerUp()
	{

		Player.Current.RestoreEnergy (Energy);

		base.GivePowerUp ();

	}

}
