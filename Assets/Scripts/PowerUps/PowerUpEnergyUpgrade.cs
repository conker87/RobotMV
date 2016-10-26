using UnityEngine;
using System.Collections;

public class PowerUpEnergyUpgrade : PowerUp {

	protected override void Start() {

		PowerUpName = "Energy Tank Upgrade";

	}

	public override void Give() {
		
		Player.Current.EnergyTanksMax++;
		Player.Current.EnergyTanks = Player.Current.EnergyTanksMax;

		base.Give ();

	}

}
