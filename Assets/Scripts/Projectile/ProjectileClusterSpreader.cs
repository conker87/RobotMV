using UnityEngine;
using System.Collections;

public class ProjectileClusterSpreader : Projectile {

	protected override void Start() {

		base.Start ();

//		float randomXDirection = Random.Range(Direction.x - 1f, Direction.x + 1f), randomYDirection = Random.Range(Direction.y - 1f, Direction.y + 1f);
//
//		Direction = new Vector3 (randomXDirection, randomYDirection, Direction.z);

	}

	protected override void Update () {

		base.Update ();

	}

}