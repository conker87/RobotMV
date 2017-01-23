using UnityEngine;
using System.Collections;

public class ProjectileChild : Projectile {

	Projectile p;

	public bool DoOverrideParentMovementSpeed = false;

	protected override void Start() {

		base.Start ();

		p = GetComponentInParent<Projectile> ();

		if (p != null) {

			ProjectileDamage = p.ProjectileDamage;

		}

	}

	protected override void Update() {

		if (PauseManager.Current == null ||PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		base.Update ();

		if (DoOverrideParentMovementSpeed) {

			p.MovementSpeed = MovementSpeed;

		}

	}

}