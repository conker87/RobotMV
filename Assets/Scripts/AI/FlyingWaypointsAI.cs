using UnityEngine;
using System.Collections;
using Pathfinding;

public class FlyingWaypointsAI : EnemyAI {

	[Header("Waypoints")]
	public Transform[] waypoints;
	protected int currentAIWaypoint;

	public float waitAtWaitpoint = 2f;
	protected float waitAtWaitpointTime;

	protected override void Start () {
		
		base.Start ();

		EnemyMT = EnemyMovementType.FLYING;
		canWander = false;

		currentAIWaypoint = 0;

	}

	protected override void Update() {

		if (PauseManager.Current == null ||PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		if (target == null) {

			if (EnemyMS == EnemyMovementState.WAITING) {

				waitAtWaitpointTime = Time.time + waitAtWaitpoint;

				EnemyMS = EnemyMovementState.IDLE;

			} else if (EnemyMS == EnemyMovementState.SEEKING) {

				waitingTimeNext = Time.time + waitingTime;

				EnemyMS = EnemyMovementState.HEADING_HOME;

			} else if (EnemyMS == EnemyMovementState.HEADING_HOME && Time.time > waitingTimeNext) {

				seeker.StartPath (transform.position, waypoints [currentAIWaypoint].position, OnPathComplete);
				pathIsEnded = false;

				EnemyMS = EnemyMovementState.IDLE;

			} else if (EnemyMS == EnemyMovementState.IDLE) {

				if (Time.time > waitAtWaitpointTime) {

					if (currentAIWaypoint > waypoints.Length - 1) {

						currentAIWaypoint = 0;

					}

					seeker.StartPath (transform.position, waypoints [currentAIWaypoint].position, OnPathComplete);
					pathIsEnded = false;

					EnemyMS = EnemyMovementState.PATROLLING;

				}

			}

			// Wander has been removed as this EnemyAI cannot wander.


		} else {

			EnemyMS = EnemyMovementState.SEEKING;

		}

		if (path == null) {

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

		Vector3 direction = (path.vectorPath [currentWaypoint] - transform.position).normalized;

		mc.Movement (direction);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {

			currentWaypoint++;

			if (EnemyMS == EnemyMovementState.PATROLLING && currentWaypoint > path.vectorPath.Count - 1) {

				Debug.Log ("currentAIWaypoint: " + currentAIWaypoint + ", currentWaypoint: " + currentWaypoint + ", path.vectorPath.Count: " + path.vectorPath.Count);
				currentAIWaypoint++;

				EnemyMS = EnemyMovementState.IDLE;

			}

			return;

		}

	}

	protected override Vector2 RandomlyGenerateWanderPosition(float xPosition, float yPosition, float range) {

		return new Vector2 (xPosition + Random.Range (-range, range), yPosition + Random.Range (-range, range));

	}

}