using UnityEngine;
using System.Collections;

public class ProjectileBomb : Projectile {

	protected override void Update () {

		if (PauseManager.Current == null ||PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		base.Update ();

		Player.Current.doBombsRegen = false;

	}

	protected override void OnDeath() {

		base.OnDeath();

		Player.Current.doBombsRegen = true;

	}

}