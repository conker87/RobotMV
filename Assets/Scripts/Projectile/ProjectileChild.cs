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

		base.Update ();

		if (DoOverrideParentMovementSpeed) {

			p.MovementSpeed = MovementSpeed;

		}

	}

}