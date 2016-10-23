using UnityEngine;
using System.Collections;

public class PowerUpHealthOrb : PowerUp {

	public float health = 10f;

	protected override void Start() {

		PowerUpName = "Health Orb";

	}

	public override void Give()
	{

		Player.Current.RestoreHealth (health);

		base.Give ();

	}

}
