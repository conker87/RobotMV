using UnityEngine;
using System.Collections;

public class ProjectileChild : Projectile {

	Projectile p;

	protected override void Start() {

		base.Start ();

		p = GetComponentInParent<Projectile> ();

		if (p != null) {

			projectileDamage = p.projectileDamage;

		}

	}

}