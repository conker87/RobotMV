using UnityEngine;
using System.Collections;

public class PowerUpMagnet : PowerUp {

	public override void Give() {

		Player.Current.Item_Magnet = true;

		base.Give ();

	}

}
