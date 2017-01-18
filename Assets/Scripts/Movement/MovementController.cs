using UnityEngine;
using System.Collections;

public class MovementController : Controller2D {

	[Header("Gravity")]
	public float jumpHeight = 3.5f;
	public float timeToJumpApex = .4f, accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f;

	[Header("Movement")]
	public float moveSpeed = 5f;

	protected float gravity, jumpVelocity;

	[Header("Jumping")]
	[SerializeField]
	protected bool hasJumped = false;

	protected Vector3 velocity;
	protected float velocityXSmoothing, nextShotTime;

	protected override void Start () {

		base.Start ();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		// Debug.Log (this + ": gravity: " + gravity + ", jumpVelocity: " + jumpVelocity);

	}

	protected virtual void Update () {

		SetVelocityToZeroOnCollisionsAboveAndBelow ();

		ResetJumpingVarsOnCollisionBelow();

	}

	protected void SetVelocityToZeroOnCollisionsAboveAndBelow() {

		if (collisions.above || collisions.below) 
		{
			velocity.y = 0;
		}

	}

	protected virtual void ResetJumpingVarsOnCollisionBelow() {

		if (collisions.below) {
			
			hasJumped = false;

		}

	}

	public virtual void Movement(Vector2 input) {

		float targetVelocityX = input.x * moveSpeed;

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);

		velocity.y = (isFlyingEntity) ? input.y * moveSpeed : velocity.y + (gravity * Time.fixedDeltaTime);

		Move (velocity * Time.fixedDeltaTime);

	}

	public virtual void Jump() {

		velocity.y = jumpVelocity;

	}

}
