using UnityEngine;
using System.Collections;

public class PowerUpItemEnergyShield : PowerUp {

	public override void Give() {
		
		Player.Current.ItemEnergyShield = true;

		base.Give ();

	}

}