using UnityEngine;
using System.Collections;

public class PowerUpMagnet : PowerUp {

	public override void Give() {

		Player.Current.ItemMagnet = true;

		base.Give ();

	}

}
