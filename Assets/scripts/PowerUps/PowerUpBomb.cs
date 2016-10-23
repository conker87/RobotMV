using UnityEngine;
using System.Collections;

public class PowerUpBomb : PowerUp {

	int bombsGiven = 3;

	protected override void Start() {

		PowerUpName = "Bomb";

	}

	public override void Give()
	{

		Player.Current.Bombs = Player.Current.BombsMaximum = bombsGiven;

		base.Give ();

	}

}
