using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingMovement : EnemyAI {

	protected override void Start() {

		rb = GetComponent<Rigidbody2D> ();

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

		direction *= moveSpeed * Time.fixedDeltaTime;

		rb.AddForce (direction, fmode);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {

			currentWaypoint++;
			return;

		}


	}

}
