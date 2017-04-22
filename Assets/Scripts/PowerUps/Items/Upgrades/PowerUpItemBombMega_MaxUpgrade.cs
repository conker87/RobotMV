using UnityEngine;
using System.Collections;

public class PowerUpItemBombMega_MaxUpgrade : PowerUp {

	int bombsGiven = 1;

	public override void Give() {

		Player.Current.BombsMegaMax += bombsGiven;

		base.Give ();

	}

}
