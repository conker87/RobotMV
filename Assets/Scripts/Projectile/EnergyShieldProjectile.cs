using UnityEngine;
using System.Collections;

// Refactored 03/02/2017
public class EnergyShieldProjectile : ProjectileBase {

	protected override void Update () {

		if (PauseManager.Current == null ||PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		transform.position = Player.Current.transform.position;

	}

	protected override void OnTriggerEnter2D(Collider2D other) {



	}

}