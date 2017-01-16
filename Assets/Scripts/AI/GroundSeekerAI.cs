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

		// Always do gravity.
		mc.Movement(new Vector2(0f, 1f));

		if (target == null) {

			if (EnemyMS == EnemyMovementState.SEEKING) {



			} else {

				if (canWander && Time.time > wanderingTimeNext) {

					// TODO: Position needs to check to see if it's not inside geometry and if so, pick again.
					Vector2 randomPosition = new Vector2 (enemyOriginPosition.position.x + Random.Range (-playerRange, playerRange), transform.position.y);

					seeker.StartPath (transform.position, randomPosition, OnPathComplete);
					pathIsEnded = false;

					EnemyMS = EnemyMovementState.WANDERING;

					wanderingTimeNext = Time.time + waitingTime;

				}

				if (pathIsEnded) {

					return;

				}

			}

		} else {

			EnemyMS = EnemyMovementState.IDLE;

		}

		if (path == null) {

			return;

		}

		EnemyMS = EnemyMovementState.SEEKING;

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

		mc.Movement (direction);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {

			currentWaypoint++;
			return;

		}

	}

}