using UnityEngine;
using System.Collections;

public class PowerUpItemShurikenShield : PowerUp {

	protected override void Start() {

		PowerUpName = "Shuriken Shield";

	}

	public override void Give()
	{

		Player.Current.ShurikenShield = true;

		base.Give ();

	}

}
