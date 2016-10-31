using UnityEngine;
using System.Collections;

public class PowerUpItemBombUpgrade : PowerUp {

	int bombsGiven = 1;

	protected override void Start() {

		PowerUpName = "Bomb Upgrade";

	}

	public override void Give()
	{

		Player.Current.BombsMaximum += bombsGiven;

		base.Give ();

	}

}
