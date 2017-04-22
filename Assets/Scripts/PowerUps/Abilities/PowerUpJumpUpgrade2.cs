using UnityEngine;
using System.Collections;

public class PowerUpJumpUpgrade2 : PowerUp {

	public override void Give() {
		
		Player.Current.PowerUpJumpTriple = true;

		base.Give ();

	}

}