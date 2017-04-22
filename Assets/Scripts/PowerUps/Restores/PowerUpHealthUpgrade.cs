using UnityEngine;
using System.Collections;

public class PowerUpHealthUpgrade : PowerUp {

	public override void Give() {
		
		Player.Current.HealthMax++;
		Player.Current.HealthCurrent = Player.Current.HealthMax;

		base.Give ();

	}

}
