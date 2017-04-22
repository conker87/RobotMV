using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MovementController {

	// The multiplier to the movement speed if the Entity is crouching.
	public float crouchingMovementPenalty = 0.6f;

	Vector2 lookDirection;

	[SerializeField]
	protected bool hasDoubleJumped = false, hasTripleJumped = false, isCurrentlyCrouching = false, isCurrentlyLookingLeft = false;
	[SerializeField]
	Vector2 input;

	SpriteRenderer sr;

	// TODO: Move this to a location on the gun sprite.
	[Header("TODO: Move this to a location on the gun sprite.")]
	public GameObject ShootLocation;

	public bool currentlyLookingLeft() {

		return isCurrentlyLookingLeft;

	}

	protected override void Start ()
	{

		base.Start ();

		sr = GetComponent<SpriteRenderer> ();

	}

	protected override void Update () {

		if (PauseManager.Current == null || PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		lookDirection = InputManager.Current.GetShootingDirection (isCurrentlyLookingLeft, isCurrentlyCrouching);

		input = Vector2.zero;

		sr.flipX = (isCurrentlyLookingLeft) ? true : false;

		if (collisions.below && (lookDirection.y == -1 && lookDirection.x == 0f)) {

			isCurrentlyCrouching = true;
			// Do crouching Anim.

			lookDirection = new Vector2 (input.x, 0f);

		} else if (collisions.below && (lookDirection.y == -1 && lookDirection.x != 0f)) {

			isCurrentlyCrouching = true;
			// Do crouching Anim.

			Vector2 newInput = new Vector2 (input.x * crouchingMovementPenalty, input.y);

			input = newInput;

		} else {

			isCurrentlyCrouching = false;

		}

		Jumping (input);

		if (lookDirection != Vector2.zero) {

			ShootWeapon (ShootLocation.transform.position, lookDirection);

		}

		if (input != Vector2.zero) {
			isCurrentlyLookingLeft = (input.x < 0) ? true : false;
		}

		UseItem ();

		input = DoMovement ();

		Movement (input);

		base.Update ();
	
}



	public void Jumping(Vector2 input) {

		if (InputManager.Current.GetButtonDown("Jump")) {

		 NO LONGER WORKS -- FIX ME
		
			if (!collisions.below || hasJumped) {

				if (Player.Current.PowerUp_Jump_Triple && hasJumped && hasDoubleJumped && !hasTripleJumped) {

					velocity.y = jumpVelocity;

					hasTripleJumped = true;

				}

				if (Player.Current.PowerUp_Jump_Double && !hasDoubleJumped) {

					velocity.y = jumpVelocity;

					hasDoubleJumped = true;
					hasJumped = true;

				}

			} else if (collisions.below) {

				if (Player.Current.PowerUp_Jump) {

					velocity.y = jumpVelocity;

					hasJumped = true;

				}

			}

		}

	}

	protected Vector2 DoMovement() {
		
		float movement = 0f;

		if (!InputManager.Current.isUsingController) {

			if (InputManager.Current.GetButton ("Left")) {

				movement = (InputManager.Current.GetButton ("Fix Location")) ? 0f : -1f;
				isCurrentlyLookingLeft = true;

			} else if (InputManager.Current.GetButton ("Right")) {

				movement = (InputManager.Current.GetButton ("Fix Location")) ? 0f : 1f;
				isCurrentlyLookingLeft = false;

			} else {

				movement = 0f;

			}

			//return input = new Vector2 (hardInput.GetAxis ("Forward", "Backward", gravity), 0f);

			return input = new Vector2 (movement, 0f);

		} else {

			return input = (InputManager.Current.GetButton ("Fix Location")) ? new Vector2 (0f, Input.GetAxisRaw ("Vertical")) :
				new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		}

	}

	void ShootWeapon(Vector2 ShootPosition, Vector2 ShootDirection) {

		if (Player.Current.CurrentWeapon != null) {

			Player.Current.CurrentWeapon.Shoot (ShootPosition, ShootDirection);

		}

	}

	void UseItem() {
		
		if (InputManager.Current.GetButtonDown ("Use Item") && Player.Current.CurrentItem != null) {

			Player.Current.CurrentItem.Use();

		}

	}

	protected override void ResetJumpingVarsOnCollisionBelow() {

		if (collisions.below) {

			hasJumped = false;
			hasDoubleJumped = false;
			hasTripleJumped = false;

		}

	}

}
