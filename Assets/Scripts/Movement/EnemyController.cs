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

	protected override void Update () {
		
		if (PauseManager.Current == null ||PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		if (connectedEntity != null) {

			if (moveSpeed != connectedEntity.MoveSpeed) {

				moveSpeed = connectedEntity.MoveSpeed;

			}

		}

		if (!isFlyingEntity) {
		
			SetVelocityToZeroOnCollisionsAboveAndBelow ();

		}

		ResetJumpingVarsOnCollisionBelow();

	}

}
