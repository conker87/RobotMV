using UnityEngine;
using System.Collections;

public class PowerUpMagnet : PowerUp {

	protected override void Start() {

		PowerUpName = "Magnet";

	}

	public override void Give()
	{

		Player.Current.Magnet = true;

		base.Give ();

	}

}
