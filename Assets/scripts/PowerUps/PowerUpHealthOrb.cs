using UnityEngine;
using System.Collections;

public class PowerUpHealthOrb : PowerUp {

	public int health = 1;

	protected override void Start() {

		PowerUpName = "Health Orb";

	}

	public override void Give()
	{

		Player.Current.RestoreHealth (health);

		base.Give ();

	}

}
