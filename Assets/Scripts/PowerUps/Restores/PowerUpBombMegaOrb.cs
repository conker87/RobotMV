using UnityEngine;
using System.Collections;

public class PowerUpBombMegaOrb : PowerUp {

	public int bombs = 1;

	public override void Give() {

		Player.Current.BombsMegaCurrent = (Player.Current.BombsMegaCurrent == Player.Current.BombsMegaMax) ? Player.Current.BombsMegaMax : Player.Current.BombsMegaCurrent + bombs;

		base.Give ();

	}

}
