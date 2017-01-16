using UnityEngine;
using System.Collections;
using Pathfinding;

public class GroundSeekerAI : EnemyAI {

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.GROUND;

	}

	protected override void FixedUpdate() {

		// Always do gravity.
		mc.Movement(new Vector2(0f, 1f));

		if (EnemyMS != EnemyMovementState.SEEKING && canWander && Time.time > wanderingTimeNext) {

			randomPosition = new Vector2 (enemyOriginPosition.position.x + Random.Range (-playerRange, playerRange), enemyOriginPosition.position.y);

		}

		base.FixedUpdate ();

	}

}