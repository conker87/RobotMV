using UnityEngine;
using System.Collections;

public class ProjectileBomb : Projectile {

	public GameObject onDeathObjectSpawn;

	protected override void OnDeath() {

		Instantiate (onDeathObjectSpawn, transform.position, Quaternion.identity);

	}

}