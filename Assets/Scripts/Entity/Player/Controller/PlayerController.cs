using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MovementController {
	
	protected override void Update ()
	{
		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		SetVelocityToZeroOnCollisionsAboveAndBelow ();

		ResetJumpingVarsOnCollisionBelow();

		Movement (input);

		ShootWeapon ();
		UseItem ();

		ReloadLevelWithKey (KeyCode.O);

	}

	protected override void ResetJumpingVarsOnCollisionBelow() {

		if (controller.collisions.below)
		{
			hasJumped = false;
			hasDoubleJumped = false;
			hasTripleJumped = false;
		}

	}

	public override void Movement(Vector2 input) {

		if (controller.collisions.below) {
			
			if (Player.Current.inputManager.GetButtonDown("Jump") && Player.Current.Jump) {

				velocity.y = jumpVelocity;

				hasJumped = true;

			}

		} else {
			
			if (Player.Current.inputManager.GetButtonDown("Jump") && Player.Current.TripleJump && hasJumped) {

				if (hasDoubleJumped && !hasTripleJumped) {

					velocity.y = jumpVelocity;

					hasTripleJumped = true;

				}

			}

			if (Player.Current.inputManager.GetButtonDown("Jump") && Player.Current.DoubleJump && hasJumped) {

				if (!hasDoubleJumped) {

					velocity.y = jumpVelocity;

					hasDoubleJumped = true;

				}

			}

		}

		float targetVelocityX = input.x * Player.Current.MoveSpeed;

		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
			(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

	}

	void ShootWeapon() {

		if (Player.Current.CurrentWeapon != null) {

			Player.Current.CurrentWeapon.Shoot (ShootLocation.transform.position);

		}

	}

	void UseItem() {
		
		if (Player.Current.CurrentItem != null) {

			Player.Current.CurrentItem.Use();
			//Player.Current.CurrentWeapon.ShootAfter ();

		}

	}

	void ReloadLevelWithKey(KeyCode key) {

		if (Input.GetKeyUp(KeyCode.P))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

	}

}
