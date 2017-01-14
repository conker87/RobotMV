using UnityEngine;
using System.Collections;

public class PowerUpEnergyUpgrade : PowerUp {

	protected override void Start() {

		PowerUpName = "Energy Tank Upgrade";

	}

	public override void Give() {
		
		Debug.Log ("Player.Current.EnergyTanksMax NOT LONGER EXISTS :: PLEASE REMOVE THIS GAMEOBJECT FROM REFERENCES: " + this);

		//Player.Current.EnergyTanksMax++;
		//Player.Current.EnergyTanks = Player.Current.EnergyTanksMax;

		base.Give ();

	}

}
