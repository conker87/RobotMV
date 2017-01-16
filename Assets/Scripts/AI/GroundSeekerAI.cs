using UnityEngine;
using System.Collections;
using Pathfinding;

public class GroundSeekerAI : EnemyAI {

	protected override void Start ()
	{
		
		base.Start ();

		EnemyMT = EnemyMovementType.GROUND;

	}

	protected override void FixedUpdate() {

		if (target == null || path == null) {

			// Make sure gravity is done on the Entity.
			mc.Movement(new Vector2(0f, 1f));

			return;

		}

		if (currentWaypoint >= path.vectorPath.Count) {

			if (pathIsEnded) {

				return;

			}

			//Debug.Log ("Path complete.");
			pathIsEnded = true;

			return;

		}

		pathIsEnded = false;

		Vector3 direction;

		direction = (path.vectorPath [currentWaypoint] - transform.position).normalized;

		Debug.Log (direction);

//		direction.x = (direction.x == 0f) ? 0f : Mathf.Sign(direction.x) * 1f;
//		direction.y = (direction.y == 0f) ? 0f : Mathf.Sign(direction.y) * 1f;

		mc.Movement (direction);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {

			currentWaypoint++;
			return;

		}

	}

}