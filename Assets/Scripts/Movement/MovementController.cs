using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class MovementController : MonoBehaviour {

	public GameObject ShootLocation;

	protected Controller2D controller;

	public float jumpHeight = 3.5f, timeToJumpApex = .4f, accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f, moveSpeed = 5f;

	protected float gravity, jumpVelocity;

	[SerializeField]
	protected bool hasJumped = false, hasDoubleJumped = false, hasTripleJumped = false;

	protected Vector3 velocity;
	protected float velocityXSmoothing, nextShotTime;

	protected virtual void Start ()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		//Debug.Log ("gravity: " + gravity + ", jumpVelocity: " + jumpVelocity);
	}

	protected virtual void Update ()
	{

		SetVelocityToZeroOnCollisionsAboveAndBelow ();

		ResetJumpingVarsOnCollisionBelow();

	}

	protected void SetVelocityToZeroOnCollisionsAboveAndBelow() {

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

	}

	protected virtual void ResetJumpingVarsOnCollisionBelow() {

		if (controller.collisions.below)
		{
			
			hasJumped = false;

		}

	}

	public virtual void Movement(Vector2 input) {

		float targetVelocityX = input.x * moveSpeed;

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
			(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
		velocity.y += gravity * Time.fixedDeltaTime;
		controller.Move (velocity * Time.fixedDeltaTime);

	}

	public virtual void Jump() {

		velocity.y = jumpVelocity;

	}

}
