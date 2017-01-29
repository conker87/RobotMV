using UnityEngine;
using System.Collections;

public class PowerUpItemBombMegaUpgrade : PowerUp {

	int bombsGiven = 1;

	public override void Give() {

		Player.Current.Bombs_Mega_Max += bombsGiven;

		base.Give ();

	}

}
