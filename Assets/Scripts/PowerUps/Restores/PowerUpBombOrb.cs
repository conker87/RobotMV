using UnityEngine;
using System.Collections;

public class PowerUpBombOrb : PowerUp {

	public int bombs = 1;

	public override void Give() {

		Player.Current.BombsCurrent = (Player.Current.BombsCurrent == Player.Current.BombsMax) ? Player.Current.BombsMax : Player.Current.BombsCurrent + bombs;

		base.Give ();

	}

}
