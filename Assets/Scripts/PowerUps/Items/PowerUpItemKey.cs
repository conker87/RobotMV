using UnityEngine;
using System.Collections;

public class PowerUpItemKey : PowerUp {

	public override void Give() {
		
		Player.Current.Keys++;

		base.Give ();

	}

}