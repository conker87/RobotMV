using UnityEngine;
using System.Collections;

public class PowerUpItemBomb : PowerUp {

	int bombsGiven = 3;

	public override void Give() {

		Player.Current.Bombs_Current = Player.Current.Bombs_Max = bombsGiven;

		base.Give ();

	}

}
