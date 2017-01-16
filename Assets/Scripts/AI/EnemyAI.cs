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

		StartCoroutine (UpdatePath ());

	}

	/// <summary>
	/// FixedUpdate should be overrided.
	/// </summary>
	protected virtual void FixedUpdate() {

		throw new UnityException ("FixedUpdate should be overrided.");

//		if (target == null || path == null) {
//
//			// Make sure gravity is done on the Entity.
//			DoGravity();
//
//			return;
//
//		}
//
//		DoWaypath ();
//
//		DoMovement (target);
//
//		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
//
//			currentWaypoint++;
//			return;
//
//		}

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

	void OnDrawGizmos() {

		Gizmos.DrawLine (transform.position, ((Vector2) direction * playerRange) + (Vector2) transform.position);

		Gizmos.DrawWireSphere (transform.position, playerRange);

	}

}

public enum EnemyMovementType { FLYING, GROUND };
