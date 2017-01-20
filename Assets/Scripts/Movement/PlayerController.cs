using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MovementController {

	// The multiplier to the movement speed if the Entity is crouching.
	public float crouchingMovementPenalty = 0.6f;

	Vector2 Direction;

	[SerializeField]
	protected bool hasDoubleJumped = false, hasTripleJumped = false, isCurrentlyCrouching = false, currentlyLookingLeft = false;
	[SerializeField]
	Vector2 input;

	SpriteRenderer sr;

	// TODO: Move this to a location on the gun sprite.
	[Header("TODO: Move this to a location on the gun sprite.")]
	public GameObject ShootLocation;

	public bool isCurrentlyLookingLeft() {

		return currentlyLookingLeft;

	}

	protected override void Start ()
	{

		base.Start ();

		sr = GetComponent<SpriteRenderer> ();

	}

	protected override void Update () {

		if (PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		base.Update ();

		Direction = InputManager.Current.GetShootingDirection (currentlyLookingLeft, isCurrentlyCrouching);

		input = new Vector2 (0f, 0f);

		sr.flipX = (currentlyLookingLeft) ? true : false;

		input = DoMovement ();


		if (collisions.below && (Direction.y == -1 && Direction.x == 0f)) {

			isCurrentlyCrouching = true;
			// Do crouching Anim.

			Direction = new Vector2 (input.x, 0f);

		} else if (collisions.below && (Direction.y == -1 && Direction.x != 0f)) {

			isCurrentlyCrouching = true;
			// Do crouching Anim.

			Vector2 newInput = new Vector2 (input.x * crouchingMovementPenalty, input.y);

			input = newInput;

		} else {

			isCurrentlyCrouching = false;

		}

		Jumping (input);

		if (Direction != Vector2.zero) {

			ShootWeapon (ShootLocation.transform.position, Direction);

		}

		if (input != Vector2.zero) {
			currentlyLookingLeft = (input.x < 0) ? true : false;
		}

		UseItem ();

		Movement (input);
	
}



	public void Jumping(Vector2 input) {

		if (InputManager.Current.GetButtonDown("Jump")) {

			if (!collisions.below || hasJumped) {

				if (Player.Current.CollectablesD ["JUMP_TRIPLE"] && hasJumped && hasDoubleJumped && !hasTripleJumped) {

					velocity.y = jumpVelocity;

					hasTripleJumped = true;

				}

				if (Player.Current.CollectablesD["JUMP_DOUBLE"] && !hasDoubleJumped) {

					velocity.y = jumpVelocity;

					hasDoubleJumped = true;
					hasJumped = true;

				}

			} else if (collisions.below) {

				if (Player.Current.CollectablesD["JUMP"]) {

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
				currentlyLookingLeft = true;

			} else if (InputManager.Current.GetButton ("Right")) {

				movement = (InputManager.Current.GetButton ("Fix Location")) ? 0f : 1f;
				currentlyLookingLeft = false;

			} else {

				movement = 0f;

			}

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
			//Player.Current.CurrentWeapon.ShootAfter ();

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
