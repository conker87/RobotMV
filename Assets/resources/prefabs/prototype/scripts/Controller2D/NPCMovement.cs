using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class NPCMovement : MonoBehaviour
{
	Controller2D controller;

	float moveSpeed = 6;

	public float jumpHeight = 4, timeToJumpApex = .4f;
	float gravity, jumpVelocity;
	float accelerationTimeAirbourne = .2f, accelerationTimeGrounded = .1f;
	
	Vector3 velocity;
	float velocityXSmoothing;

	void Start ()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		Debug.Log ("gravity: " + gravity + ", jumpVelocity: " + jumpVelocity);
	}

	void Update ()
	{
		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

		Vector2 input = new Vector2(0.0f, 0.0f); //(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetButtonDown ("Jump") && controller.collisions.below)
		{
			//velocity.y = jumpVelocity;
		}

		float targetVelocityX = input.x * moveSpeed;

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
		                              (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
}
