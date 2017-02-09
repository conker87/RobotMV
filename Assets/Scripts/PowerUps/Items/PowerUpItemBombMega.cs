using UnityEngine;
using System.Collections;

public class PowerUpItemBombMega : PowerUp {

	int megaBombsGiven = 1;

	public override void Give() {

		Player.Current.Bombs_Mega_Current = Player.Current.Bombs_Mega_Max = megaBombsGiven;

		base.Give ();

	}

}
