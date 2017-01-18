using UnityEngine;
using System.Collections;

public class ProjectileChild : Projectile {

	Projectile p;

	void Start() {

		p = GetComponentInParent<Projectile> ();

		if (p != null) {

			projectileDamage = p.projectileDamage;

		}

	}

}