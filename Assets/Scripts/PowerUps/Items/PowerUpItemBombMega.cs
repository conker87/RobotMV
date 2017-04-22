using UnityEngine;
using System.Collections;

public class PowerUpItemBombMega : PowerUp {

	int megaBombsGiven = 1;

	public override void Give() {

		Player.Current.BombsMegaCurrent = Player.Current.BombsMegaMax = megaBombsGiven;

		base.Give ();

	}

}
