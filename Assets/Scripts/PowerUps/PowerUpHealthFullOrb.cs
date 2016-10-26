using UnityEngine;
using System.Collections;

public class PowerUpHealthFullOrb : PowerUp {

	protected override void Start() {

		PowerUpName = "Fully Restore Health Orb";

	}

	public override void Give()
	{

		Player.Current.RestoreHealthFully ();

		base.Give ();

	}

}
