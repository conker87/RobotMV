using UnityEngine;
using System.Collections;
using Pathfinding;

public class FlyingSeekerAI : EnemyAI {

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.FLYING;

	}

	protected override void FixedUpdate() {

		if (target == null) {

			if (EnemyMS == EnemyMovementState.SEEKING) {

				waitingTimeNext = Time.time + waitingTime;

				if (returnToOrigin && Time.time > waitingTimeNext) {

					path = null;
					seeker.StartPath (transform.position, enemyOriginPosition.position, OnPathComplete);

				}

			} else {

				if (canWander && Time.time > wanderingTimeNext) {

					// TODO: Position needs to check to see if it's not inside geometry and if so, pick again.
					Vector2 randomPosition = new Vector2 (enemyOriginPosition.position.x + Random.Range (-playerRange, playerRange), enemyOriginPosition.position.y + Random.Range (-playerRange, playerRange));

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