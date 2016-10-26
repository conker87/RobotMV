using UnityEngine;
using System.Collections;

public class ProjectileBomb : Projectile {

	protected override void Update () {

		base.Update ();

		Player.Current.doBombsRegen = false;

	}

	protected override void OnDeath() {

		base.OnDeath();

		Player.Current.doBombsRegen = true;

	}

}