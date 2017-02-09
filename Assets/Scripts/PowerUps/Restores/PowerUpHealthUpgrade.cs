using UnityEngine;
using System.Collections;

public class PowerUpHealthUpgrade : PowerUp {

	public override void Give() {
		
		Player.Current.Health_Max++;
		Player.Current.Health_Current = Player.Current.Health_Max;

		base.Give ();

	}

}
