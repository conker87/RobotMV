using UnityEngine;
using System.Collections;

public class PowerUpJump : PowerUp {

	public override void Give() {
		
		Player.Current.PowerUpJump = true;

		base.Give ();

	}

}