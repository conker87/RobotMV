using UnityEngine;
using System.Collections;

public class PowerUpEnergyOrb : PowerUp {

	public float value = 10f;

	protected override void Start() {

		PowerUpName = "Energy Orb";

	}

	public override void Give()
	{

		Debug.Log ("Player.Current.RestoreEnergy NOT LONGER EXISTS :: PLEASE REMOVE THIS GAMEOBJECT FROM REFERENCES: " + this);

		//Player.Current.RestoreEnergy (value);

		base.Give ();

	}

}
