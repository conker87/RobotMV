using UnityEngine;
using System.Collections;

public class PowerUpEnergyFullyOrb : PowerUp {

	protected override void Start() {

		PowerUpName = "Fully Restore Energy Orb";

	}

	public override void Give()
	{

		Debug.Log ("Player.Current.RestoreEnergyFully NOT LONGER EXISTS :: PLEASE REMOVE THIS GAMEOBJECT FROM REFERENCES: " + this);

		//Player.Current.RestoreEnergyFully ();

		base.Give ();

	}

}
