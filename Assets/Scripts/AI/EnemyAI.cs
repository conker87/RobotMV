using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(EnemyController))]
public class EnemyAI : MonoBehaviour {

	// Player
	[Header("Waypoint")]
	public Transform target;
	public float nextWaypointDistance = 1f;
	public float updateRate = 2f;
	public bool requiresLineOfSight = true;

	[Header("Player & Masks")]
	public float playerRange = 5f;
	public LayerMask playerMask, geometryMask;

	[Header("Enemy Movement Type")]
	public EnemyMovementType EnemyMT;

	[Header("Enemy Movement State")]
	[SerializeField]
	protected EnemyMovementState EnemyMS;
	public Transform enemyOriginPosition;
	public bool canWander = false, returnToOrigin = true;
	public float waitingTime = 3f;
	protected bool hasDoneWaitingTimeNext = false;
	protected Vector2 randomPosition;

	protected float wanderingTimeNext, waitingTimeNext;

	protected Collider2D circleCollider;
	protected RaycastHit2D ray;

	protected Vector2 _DEBUG_rayDirection;

	// Caching
	protected Seeker seeker;
	protected MovementController mc;

	// The calculated A* path
	public Path path;
	protected int currentWaypoint = 0;

	[HideInInspector]
	public bool pathIsEnded = false;

	Vector2 heading, direction;
	float distance;

	// Max distance between AI and waypoint.

	protected virtual void Start() {

		seeker = GetComponent<Seeker> ();
		mc = GetComponent<MovementController> ();

		wanderingTimeNext = waitingTimeNext = 0f;

		StartCoroutine (UpdatePath ());

	}

	/// <summary>
	/// FixedUpdate should be overrided.
	/// </summary>
	protected virtual void FixedUpdate() {

		// throw new UnityException ("FixedUpdate should be overrided.");

		if (target == null) {

			if (EnemyMS == EnemyMovementState.SEEKING) {

				waitingTimeNext = Time.time + waitingTime;
				EnemyMS = EnemyMovementState.HEADING_HOME;

			} else if (EnemyMS == EnemyMovementState.HEADING_HOME && returnToOrigin && Time.time > waitingTimeNext) {


				seeker.StartPath (transform.position, enemyOriginPosition.position, OnPathComplete);
				pathIsEnded = false;

				EnemyMS = EnemyMovementState.IDLE;

			} else {

				if (canWander && Time.time > wanderingTimeNext) {

					// TODO: Position needs to check to see if it's not inside geometry and if so, pick again.
					Debug.Log ("Using new position: " + randomPosition);

					seeker.StartPath (transform.position, randomPosition, OnPathComplete);
					pathIsEnded = false;

					EnemyMS = EnemyMovementState.WANDERING;

					wanderingTimeNext = Time.time + waitingTime;

				}

			}

		} else {

			EnemyMS = EnemyMovementState.SEEKING;

		}

		if (path == null) {

			EnemyMS = EnemyMovementState.IDLE;
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

			currentWaypoint++;
			return;

		}

	}

	protected virtual IEnumerator UpdatePath() {

		target = DetectPlayerInRadius (transform.position, playerRange, playerMask, geometryMask, requiresLineOfSight);

		if (target != null) {
			
			seeker.StartPath (transform.position, target.position, OnPathComplete);

		}

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());

	}

	protected virtual void OnPathComplete(Path p) {

		Debug.Log ("Path error? : " + p.error);

		if (!p.error) {

			path = p;
			currentWaypoint = 0;

		}
	}

	protected Transform DetectPlayerInRadius(Vector2 currentPosition, float range, LayerMask PlayerMask, LayerMask GeometryMask, bool requiresLineOfSight = true) {

		if (circleCollider = Physics2D.OverlapCircle (transform.position, range, playerMask)) {

			if (!requiresLineOfSight) {

				return circleCollider.transform;

			}

			heading = circleCollider.transform.position - transform.position;
			distance = heading.magnitude;
			direction = heading / distance;

			ray = Physics2D.Raycast (transform.position, direction, range, geometryMask);

			// Debug.Log ("Inside Circle? " + circleCollider + " -- Ray: " + ray);

			if (ray.transform == null) {

				return circleCollider.transform;

			}

		}

		return null;

	}

	protected virtual void OnDisable () {
		// Abort calculation of path
		if (seeker != null && !seeker.IsDone()) seeker.GetCurrentPath().Error();

		// Release current path
		//if (path != null) path.Release(this);
		path = null;

		// Make sure we receive callbacks when paths complete
		seeker.pathCallback -= OnPathComplete;
	}

	void OnDrawGizmos() {

		Gizmos.DrawLine (transform.position, ((Vector2) direction * playerRange) + (Vector2) transform.position);

		Gizmos.DrawWireSphere (transform.position, playerRange);

	}

}

public enum EnemyMovementType { FLYING, GROUND };
public enum EnemyMovementState { IDLE, WANDERING, SEEKING, WAITING, HEADING_HOME }