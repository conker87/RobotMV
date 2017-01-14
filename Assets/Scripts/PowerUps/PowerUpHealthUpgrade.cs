using UnityEngine;
using System.Collections;

public class PowerUpHealthUpgrade : PowerUp {

	protected override void Start() {

		PowerUpName = "Health Tank Upgrade";

	}

	public override void Give() {
		
		Debug.Log ("Player.Current.HealthTanksMax NOT LONGER EXISTS :: PLEASE REMOVE THIS GAMEOBJECT FROM REFERENCES: " + this);

		//Player.Current.HealthTanksMax++;
		//Player.Current.HealthTanks = Player.Current.HealthTanksMax;

		base.Give ();

	}

}
