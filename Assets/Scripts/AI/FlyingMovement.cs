using UnityEngine;
using System.Collections;
using Pathfinding;

//[RequireComponent(typeof(Rigidbody2D))]
////[RequireComponent(typeof(Seeker))]
public class FlyingMovement : EnemyAI {

	// Player
	public Transform target;
	public float playerRange;
	public LayerMask playerMask;

	Collider2D circleCollider;

	// How many times per second the path will update
	public float updateRate = 2f;

	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	// The calculated A* path
	public Path path;
	private int currentWaypoint = 0;

	// Speed vars
	public float moveSpeed = 300f;
	public ForceMode2D fmode;

	[HideInInspector]
	public bool pathIsEnded = false;

	// Max distance between AI and waypoint.
	public float nextWaypointDistance = 1f;

	public override void Start() {

		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (circleCollider = Physics2D.OverlapCircle (transform.position, playerRange, playerMask)) {

			Debug.Log (circleCollider);
			target = circleCollider.transform;

		}

		StartCoroutine (UpdatePath ());

	}

	void FixedUpdate() {

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

	IEnumerator UpdatePath() {

		target = (circleCollider = Physics2D.OverlapCircle (transform.position, playerRange, playerMask)) ? circleCollider.transform : null;

		if (target != null) {
			seeker.StartPath (transform.position, target.position, OnPathComplete);
		}

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());

//		circleCollider = Physics2D.OverlapCircle (transform.position, playerRange, playerMask);
//
//		if (target == null) {
//
//			if (circleCollider) {
//			
//				Debug.Log (circleCollider);
//				target = circleCollider.transform;
//
//			}
//
//		}
//
//		if (target != null) {
//
//			if (circleCollider) {
//
//				target = null;
//
//			} else {
//
//				seeker.StartPath (transform.position, target.position, OnPathComplete);
//
//			}
//
//		}

	}

	public void OnPathComplete(Path p) {

		Debug.Log ("Path error? : " + p.error);

		if (!p.error) {

			path = p;
			currentWaypoint = 0;

		}
	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, playerRange);

	}








//	private Player player;
//	public float playerRange, moveSpeed = 10f;
//
//	public LayerMask playerMask;
//
//	void Start () {
//	
//		player = FindObjectOfType<Player> ();
//
//	}
//
//	void Update () {
//
//		if (Physics2D.OverlapCircle (transform.position, playerRange, playerMask)) {
//			
//			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
//
//		}
//
//	}
//
//	void OnDrawGizmos() {
//
//		Gizmos.DrawWireSphere (transform.position, playerRange);
//
//	}

}
