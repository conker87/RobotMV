using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(EnemyController))]
public class EnemyAIBase : MonoBehaviour {

	// TODO: AI needs a total rework with proper states.

	// Player
	[Header("Seeker Waypoints")]
	public Transform target;
	[Range(0, 10)]
	public float nextWaypointDistance = 1f;
	[Range(0.01f, 10)]
	public float updateRate = 2f;
	public bool requiresLineOfSight = true;

	[Header("Enemy AI")]
	public bool isFlying = false;
	public bool canSeek = false, canWander = false, usesWaypoints = false;

	[Header("Enemy AI -- Seeker")]
	[Range(0, 20)]
	public float playerRange = 5f;
	public LayerMask playerMask, geometryMask;

	[Header("Enemy AI -- Wander")]
	[Range(0, 10)]
	public float waitingTime = 3f;
	protected bool hasDoneWaitingTimeNext = false, wanderingFailed = false;
	public int wanderCollisionMaxItterations = 4;
	protected int wanderCollisionItteration;
	protected Vector3 randomPosition;

	[Header("Enemy AI -- Waypoints")]
	public Vector3[] waypoints;
	Vector3[] globalWaypoints;

	[Header("Enemy Movement Settings")]
	public bool returnToOrigin = true;
	public Transform enemyOriginPosition;

	protected float wanderingTimeNext, waitingTimeNext;

	protected Collider2D circleCollider;
	protected RaycastHit2D ray;

	protected Vector3 _DEBUG_rayDirection;

	// Caching
	protected Seeker seeker;
	protected MovementController mc;

	// The calculated A* path
	public Path path;
	protected int currentWaypoint = 0;

	[HideInInspector]
	public bool pathIsEnded = false;

	Vector3 heading, direction;
	float distance;

	// Max distance between AI and waypoint.

	protected virtual void Start() {

		seeker = GetComponent<Seeker> ();
		mc = GetComponent<MovementController> ();

		wanderingTimeNext = waitingTimeNext = 0f;

		StartCoroutine (UpdatePath ());

		globalWaypoints = new Vector3[waypoints.Length];

		for (int i =0; i < waypoints.Length; i++) {

			globalWaypoints[i] = waypoints[i] + transform.position;

		}

	}

	/// <summary>
	/// FixedUpdate should be overrided.
	/// </summary>
	protected virtual void Update() {



	}

	protected virtual Vector3 RandomlyGenerateWanderPosition(float xPosition, float yPosition, float range) {

		throw new UnityException("RandomlyGenerateWanderPosition needs to be Overriden inheritly.");

		// return new Vector3(float.MaxValue, -float.MaxValue);
		
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

		// Debug.Log ("Path error? : " + p.error);

		if (!p.error) {

			path = p;
			currentWaypoint = 0;

		}
	}

	protected Transform DetectPlayerInRadius(Vector3 currentPosition, float range, LayerMask PlayerMask, LayerMask GeometryMask, bool requiresLineOfSight = true) {

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

		Gizmos.DrawLine (transform.position, ((Vector3) direction * playerRange) + (Vector3) transform.position);

		Gizmos.DrawWireSphere (transform.position, playerRange);

		if (waypoints != null) {

			Gizmos.color = Color.red;
			float size = .3f;

			for (int i =0; i < waypoints.Length; i ++) {

				Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : waypoints[i] + transform.position;
				Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
				Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);

			}
		}

	}

}