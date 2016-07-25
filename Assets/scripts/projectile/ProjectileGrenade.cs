using UnityEngine;
using System.Collections;

public class ProjectileGrenade : Projectile {

	protected override void Update () {

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		base.OnTriggerEnter2D (other);

	}

}