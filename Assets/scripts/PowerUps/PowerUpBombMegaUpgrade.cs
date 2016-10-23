using UnityEngine;
using System.Collections;

public class PowerUpBombMegaUpgrade : PowerUp {

	int bombsGiven = 1;

	protected override void Start() {

		PowerUpName = "Mega Bomb Upgrade";

	}

	public override void Give()
	{

		Player.Current.MegaBombsMaximum += bombsGiven;

		base.Give ();

	}

}
