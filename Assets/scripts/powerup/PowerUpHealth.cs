using UnityEngine;
using System.Collections;

public class PowerUpHealth : PowerUp {

	public float Health = 10f;

	protected override void Start() {

		PowerUpName = "Give " + Health + " Health";

	}

	public override void GivePowerUp()
	{

		Player.Current.RestoreHealth (Health);

		base.GivePowerUp ();

	}

}
