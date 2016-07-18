using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour
{
	
	public Projectile projectile;
	public GameObject ShootLocation, CollectionRadius;

	Controller2D controller;

	public float jumpHeight = 3.5f, timeToJumpApex = .4f, accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f;
	float gravity, jumpVelocity;

	bool hasJumped = false, hasDoubleJumped = false, hasTripleJumped = false;
	
	Vector3 velocity;
	float velocityXSmoothing, nextShotTime;

	void Start ()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		//Debug.Log ("gravity: " + gravity + ", jumpVelocity: " + jumpVelocity);
	}

	void Update ()
	{
		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		SetVelocityToZeroOnCollisionsAboveAndBelow ();

		ResetJumpingVarsOnCollisionBelow();

		Jumping (input);

		Shoot ();

		ReloadLevelWithKey (KeyCode.P);

	}

	void SetVelocityToZeroOnCollisionsAboveAndBelow() {

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

	}

	void ResetJumpingVarsOnCollisionBelow() {

		if (controller.collisions.below)
		{
			hasJumped = false;
			hasDoubleJumped = false;
			hasTripleJumped = false;
		}

	}

	void Jumping(Vector2 input) {

		if (Input.GetButtonDown("Jump") &&
			(Player.Current.TripleJump&& !hasTripleJumped && hasDoubleJumped) &&
			(hasJumped || !controller.collisions.below) )
		{

			velocity.y = jumpVelocity;

			hasTripleJumped = true;

		}

		if (Input.GetButtonDown ("Jump") &&
			(Player.Current.DoubleJump && !hasDoubleJumped) &&
			(hasJumped || !controller.collisions.below)) {

			velocity.y = jumpVelocity;

			hasDoubleJumped = true;

		}

		if (Input.GetButton("Jump") &&
			Player.Current.Jump &&
			controller.collisions.below)
		{

			velocity.y = jumpVelocity;

			hasJumped = true;

		}

		float targetVelocityX = input.x * Player.Current.MoveSpeed;

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
			(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

	}

	void Shoot() {

		if (Player.Current.CurrentWeapon != null) {

			Player.Current.CurrentWeapon.Shoot (ShootLocation.transform.position);

		}

	}

	void ReloadLevelWithKey(KeyCode key) {

		if (Input.GetKeyUp(KeyCode.P))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

	}

}
