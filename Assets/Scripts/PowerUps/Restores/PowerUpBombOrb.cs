using UnityEngine;
using System.Collections;

public class PowerUpBombOrb : PowerUp {

	public int bombs = 1;

	public override void Give() {

		Player.Current.Bombs_Current = (Player.Current.Bombs_Current == Player.Current.Bombs_Max) ? Player.Current.Bombs_Max : Player.Current.Bombs_Current + bombs;

		base.Give ();

	}

}
