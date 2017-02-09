using UnityEngine;
using System.Collections;

public class PowerUpJumpUpgrade1 : PowerUp {

	public override void Give() {
		
		Player.Current.PowerUp_Jump_Double = true;

		base.Give ();

	}

}