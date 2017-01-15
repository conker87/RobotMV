using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof (MovementController))]
public class EnemyGroundMovement : EnemyAI {

	MovementController mc;

	protected override void Start() {

		mc = GetComponent<MovementController> ();

		base.Start ();

	}

	protected override void FixedUpdate() {

		if (target == null || path == null) {

			return;

		}

		if (currentWaypoint >= path.vectorPath.Count) {

			if (pathIsEnded) {
				return;
			}

			Debug.Log ("Path complete.");
			pathIsEnded = true;
			return;

		}

		pathIsEnded = false;

		// Next waypoint direction
		Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;

		//Debug.Log ("direction: " + direction);

		if (direction.x < 0) {

			direction.x = -1f;

		} else if (direction.x > 0) {

			direction.x = 1f;

		}

		mc.Movement (direction);

		// Ground movement does not use Rigidbodies.
		// direction *= Time.fixedDeltaTime;
		// rb.AddForce (direction, fmode);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {

			currentWaypoint++;
			return;

		}

	}

	protected override IEnumerator UpdatePath() {

		target = DetectPlayerInRadius (transform.position, playerRange, playerMask, geometryMask, false);

		if (target != null) {
			seeker.StartPath (transform.position, target.position, OnPathComplete);
		}

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());

	}
}