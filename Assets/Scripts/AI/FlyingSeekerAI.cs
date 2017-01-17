using UnityEngine;
using System.Collections;
using Pathfinding;

public class FlyingSeekerAI : EnemyAI {

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.FLYING;

	}

	protected override void FixedUpdate() {

		base.FixedUpdate ();

	}

	protected override Vector2 RandomlyGenerateWanderPosition(float xPosition, float yPosition, float range) {

		return new Vector2 (xPosition + Random.Range (-range, range), yPosition + Random.Range (-range, range));

	}

}