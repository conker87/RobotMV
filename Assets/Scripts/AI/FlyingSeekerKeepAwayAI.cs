using UnityEngine;
using System.Collections;
using Pathfinding;

public class FlyingSeekerKeepAwayAI : FlyingSeekerAI {

	public float keepDistanceAwayFromTarget = 2f;

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.FLYING;
		returnToOrigin = true;

	}

	protected override void Update() {

		if (PauseManager.Current == null ||PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		if (target == null) {

			if (EnemyMS == EnemyMovementState.SEEKING || EnemyMS == EnemyMovementState.KEEPING_AWAY) {

				Debug.Log ("Target is null so going to head home soon.");
				waitingTimeNext = Time.time + waitingTime;

				EnemyMS = EnemyMovementState.HEADING_HOME;

			} else if (EnemyMS == EnemyMovementState.HEADING_HOME && returnToOrigin && Time.time > waitingTimeNext) {
				
				seeker.StartPath (transform.position, enemyOriginPosition.position, OnPathComplete);
				pathIsEnded = false;

				EnemyMS = EnemyMovementState.IDLE;


			} else {

				if (canWander && Time.time > wanderingTimeNext) {

					wanderingFailed = false;
					wanderCollisionItteration = 0;

					// TODO: Position needs to check to see if it's not inside geometry and if so, pick again.
					// Debug.Log ("Using new position: " + randomPosition);

					do {

						// If the current itteration of RPP is more than the maximum then break out of the do-while and do not pick another RPP for the cooldown duration.
						if (wanderCollisionItteration > wanderCollisionMaxItterations) {

							wanderingFailed = true;
							wanderingTimeNext = Time.time + waitingTime;
							break;

						}

						// Pick a RPP for the EnemyAI to wander to.
						randomPosition = RandomlyGenerateWanderPosition (enemyOriginPosition.position.x, enemyOriginPosition.position.y, playerRange);

						// Increases the current itteration of the Random Position Picking for Wandering.
						wanderCollisionItteration++;

					} while (Physics2D.OverlapCircle (randomPosition, 0.3f, geometryMask));

					// If the RPP has not failed this cooldown itteration then move the Enemy to that position.
					if (!wanderingFailed) {

						seeker.StartPath (transform.position, randomPosition, OnPathComplete);
						pathIsEnded = false;

						EnemyMS = EnemyMovementState.WANDERING;

						wanderingTimeNext = Time.time + waitingTime;

					}

				}

			}

		} else {

			if (Vector3.Distance (transform.position, target.position) < keepDistanceAwayFromTarget * 1.1f) {

				EnemyMS = EnemyMovementState.KEEPING_AWAY;

				Vector2 heading = transform.position - target.position;
				float distance = heading.magnitude;
				Vector2 directionK = heading / distance;

				Vector2 keepAwayPosition = (Vector2)target.position + (directionK * keepDistanceAwayFromTarget);

				seeker.StartPath (transform.position, keepAwayPosition, OnPathComplete);

			} else {

				EnemyMS = EnemyMovementState.SEEKING;

			}

		}

		if (path == null) {

			//EnemyMS = EnemyMovementState.IDLE;
			return;

		}

		if (currentWaypoint >= path.vectorPath.Count) {

			if (pathIsEnded) {

				return;

			}

			pathIsEnded = true;

			return;

		}

		pathIsEnded = false;

		Vector3 direction;

		direction = (path.vectorPath [currentWaypoint] - transform.position).normalized;

		mc.Movement (direction);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {

			// Debug.Log ("currentWaypoint: " + currentWaypoint + ", path.vectorPath.Count: " + path.vectorPath.Count);

			currentWaypoint++;
			return;

		}

	}

}