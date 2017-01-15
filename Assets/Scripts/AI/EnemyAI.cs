using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

	// Player
	public Transform target;
	public float playerRange;
	public LayerMask playerMask, geometryMask;

	protected Collider2D circleCollider;

	// How many times per second the path will update
	public float updateRate = 2f;

	// Caching
	protected Seeker seeker;
	protected Rigidbody2D rb;

	// The calculated A* path
	public Path path;
	protected int currentWaypoint = 0;

	// Speed vars
	public float moveSpeed = 300f;
	public ForceMode2D fmode;

	[HideInInspector]
	public bool pathIsEnded = false;

	// Max distance between AI and waypoint.
	public float nextWaypointDistance = 1f;

	protected virtual void Start() {

		seeker = GetComponent<Seeker> ();

		StartCoroutine (UpdatePath ());

	}

	protected virtual void FixedUpdate() {

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

	protected Transform DetectPlayerInRadius(Vector2 currentPosition, float range, LayerMask PlayerMask, LayerMask GeometryMask, bool requiresLineOfSight = true) {

		RaycastHit2D ray;

		if (circleCollider = Physics2D.OverlapCircle (transform.position, playerRange, playerMask)) {

			Debug.Log (circleCollider);

			if (!requiresLineOfSight) {

				return circleCollider.transform;

			}

			Vector2 direction = circleCollider.transform.position - transform.position;

			if (ray = Physics2D.Raycast (transform.position, direction, range, geometryMask)) {

				return circleCollider.transform;

			}

			Debug.Log (ray.collider);

		}

		return null;

	}

	protected virtual IEnumerator UpdatePath() {

		target = DetectPlayerInRadius (transform.position, playerRange, playerMask, geometryMask);

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

	void OnDrawGizmos() {

		if (target != null) {

			Gizmos.DrawLine(transform.position, target.position);

		}
		Gizmos.DrawWireSphere (transform.position, playerRange);

	}

}
