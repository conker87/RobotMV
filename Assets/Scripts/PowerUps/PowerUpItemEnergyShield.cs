using UnityEngine;
using System.Collections;

public class PowerUpItemEnergyShield : PowerUp {

	public override void Give() {
		
		Player.Current.Item_EnergyShield = true;

		base.Give ();

	}

}