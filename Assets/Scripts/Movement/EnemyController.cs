using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyAI))]
public class EnemyController : MovementController {

	EnemyAI AI;

	protected override void Start() {

		base.Start ();

		AI = GetComponent<EnemyAI> ();

		isFlyingEntity = (AI.EnemyMT == EnemyMovementType.FLYING) ? true : false;

	}

}
