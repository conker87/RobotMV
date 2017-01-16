using UnityEngine;
using System.Collections;
using Pathfinding;

public class FlyingSeekerAI : EnemyAI {

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.FLYING;

	}

	protected override void FixedUpdate() {

		if (target == null || path == null) {

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

		//direction.x = (direction.x == 0f) ? 0f : Mathf.Sign(direction.x) * 1f;
		//direction.y = (direction.y == 0f) ? 0f : Mathf.Sign(direction.y) * 1f;

		Debug.Log (direction);

		mc.Movement (direction);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {

			currentWaypoint++;
			return;

		}

	}

}