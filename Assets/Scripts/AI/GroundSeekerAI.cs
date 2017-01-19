using UnityEngine;
using System.Collections;
using Pathfinding;

public class GroundSeekerAI : EnemyAI {

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.GROUND;

	}

	protected override void Update() {

		if (PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		// Always do gravity.
		mc.Movement(new Vector2(0f, 1f));

		base.Update ();

	}
		
	protected override Vector2 RandomlyGenerateWanderPosition(float xPosition, float yPosition, float range) {

		return new Vector2 (xPosition + Random.Range (-range, range), yPosition);

	}

}