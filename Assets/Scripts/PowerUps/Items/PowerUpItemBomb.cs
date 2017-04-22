using UnityEngine;
using System.Collections;

public class PowerUpItemBomb : PowerUp {

	int bombsGiven = 3;

	public override void Give() {

		Player.Current.BombsCurrent = Player.Current.BombsMax = bombsGiven;

		base.Give ();

	}

}
