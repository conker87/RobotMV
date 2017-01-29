using UnityEngine;
using System.Collections;

public class PowerUpJumpUpgrade2 : PowerUp {

	public override void Give() {
		
		Player.Current.PowerUp_Jump_Triple = true;

		base.Give ();

	}

}