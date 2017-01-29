using UnityEngine;
using System.Collections;

public class PowerUpItemBombUpgrade : PowerUp {

	int bombsGiven = 1;

	public override void Give()
	{

		Player.Current.Bombs_Max += bombsGiven;

		base.Give ();

	}

}
