using UnityEngine;
using System.Collections;

public class PowerUpHealthFullOrb : PowerUp {

	public override void Give() {

		Player.Current.RestoreHealthFully ();

		base.Give ();

	}

}
