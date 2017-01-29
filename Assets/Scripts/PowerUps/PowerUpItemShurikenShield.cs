using UnityEngine;
using System.Collections;

public class PowerUpItemShurikenShield : PowerUp {

	public override void Give() {

		Player.Current.Item_ShurikenShield = true;

		base.Give ();

	}

}
