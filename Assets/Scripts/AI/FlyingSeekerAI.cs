using UnityEngine;
using System.Collections;
using Pathfinding;

public class FlyingSeekerAI : EnemyAI {

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.FLYING;

	}

	protected override void FixedUpdate() {

		if (EnemyMS != EnemyMovementState.SEEKING && canWander && Time.time > wanderingTimeNext) {
			
			randomPosition = new Vector2 (enemyOriginPosition.position.x + Random.Range (-playerRange, playerRange), enemyOriginPosition.position.y + Random.Range (-playerRange, playerRange));

		}

		base.FixedUpdate ();

	}

}