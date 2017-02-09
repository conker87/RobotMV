using UnityEngine;
using System.Collections;

public class PowerUpItemBomb_MaxUpgrade : PowerUp {

	int bombsGiven = 1;

	public override void Give()
	{

		Player.Current.Bombs_Max += bombsGiven;

		base.Give ();

	}

}
