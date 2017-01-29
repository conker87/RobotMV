using UnityEngine;
using System.Collections;

public class PowerUpJump : PowerUp {

	public override void Give() {
		
		Player.Current.PowerUp_Jump = true;

		base.Give ();

	}

}