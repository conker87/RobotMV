using UnityEngine;
using System.Collections;

public class PowerUpBombMega : PowerUp {

	int megaBombsGiven = 1;

	protected override void Start() {

		PowerUpName = "Mega Bomb";

	}

	public override void Give()
	{

		Player.Current.MegaBombs = Player.Current.MegaBombsMaximum = megaBombsGiven;

		base.Give ();

	}

}
