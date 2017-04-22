using UnityEngine;
using System.Collections;

public class PowerUpJumpUpgrade1 : PowerUp {

	public override void Give() {
		
		Player.Current.PowerUpJumpDouble = true;

		base.Give ();

	}

}