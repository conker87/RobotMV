using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MovementController {

	// The multiplier to the movement speed if the Entity is crouching.
	public float crouchingMovementPenalty = 0.6f;

	[SerializeField]
	Vector2 input, lookDirection;

	[SerializeField]
	protected bool hasDoubleJumped = false, hasTripleJumped = false, isCurrentlyCrouching = false, isCurrentlyLookingLeft = false;

	SpriteRenderer sr;

	// TODO: Move this to a location on the gun sprite.
	[Header("TODO: Move this to a location on the gun sprite.")]
	public GameObject ShootLocation;

	public bool currentlyLookingLeft() {

		return isCurrentlyLookingLeft;

	}

	protected override void Start () {

		base.Start ();

		sr = GetComponent<SpriteRenderer> ();

	}

	protected override void Update () {

		if (PauseManager.Current == null || PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		input = Vector2.zero;

		UseItem ();

		Jump ();
		Movement (DoMovement ());
		FindLookDirection ();

		base.Update ();
	
}

	protected void FindLookDirection() {
		
		lookDirection = GetLookDirection ();

		if (input != Vector2.zero) { isCurrentlyLookingLeft = (input.x < 0); }

		sr.flipX = isCurrentlyLookingLeft;

		if (lookDirection != Vector2.zero) {

			ShootWeapon (ShootLocation.transform.position, lookDirection);

		}

	}

	Vector2 GetLookDirection() {

		float x = 0f, y = 0f;

		if (InputManager.Current.isUsingController) {

			x = InputManager.Current.GetAxis ("Horizontal");
			y = InputManager.Current.GetAxis ("Vertical");

			x = (x != 0f) ? Mathf.Sign(x) * 1: x;
			y = (y != 0f) ? Mathf.Sign(y) * 1: y;

		} else {

			if (InputManager.Current.GetButton ("Down")) {

				y = -1f;

			} else if (InputManager.Current.GetButton ("Up")) {

				y = 1f;

			}

			if (InputManager.Current.GetButton ("Left")) {

				x = -1f;
				isCurrentlyLookingLeft = true;

			} else if (InputManager.Current.GetButton ("Right")) {

				x = 1f;
				isCurrentlyLookingLeft = false;

			} else {

				if (!InputManager.Current.GetButton ("Up") && !InputManager.Current.GetButton ("Down")) {

					x = (isCurrentlyLookingLeft) ? -1f : 1f;

				} else {

					x = 0f;

				}

			}

		}

		return new Vector2 (x, y);

	}

	protected override void Jump() {

		if (InputManager.Current.GetButtonDown("Jump")) {

			if (!collisions.below || hasJumped) {

				if (Player.Current.PowerUpJumpTriple && !hasTripleJumped && hasDoubleJumped && hasJumped) {

					velocity.y = jumpVelocity;

					hasTripleJumped = true;

				}

				if (Player.Current.PowerUpJumpDouble && !hasTripleJumped && !hasDoubleJumped) {

					velocity.y = jumpVelocity;

					hasDoubleJumped = true;
					hasJumped = true;

				}

			} else if (collisions.below) {

				if (Player.Current.PowerUpJump) {

					velocity.y = jumpVelocity;

					hasJumped = true;

				}

			}

		}

	}

	protected Vector2 DoMovement() {
		
		float movement = 0f;
		isCurrentlyCrouching = false;

		if (!InputManager.Current.isUsingController) {

			if (InputManager.Current.GetButton ("Left")) {

				movement = (InputManager.Current.GetButton ("Fix Location")) ? 0f : -1f;

			} else if (InputManager.Current.GetButton ("Right")) {

				movement = (InputManager.Current.GetButton ("Fix Location")) ? 0f : 1f;

			} else {

				movement = 0f;

			}

			input = new Vector2 (movement, 0f);

		} else {

			input = (InputManager.Current.GetButton ("Fix Location")) ? new Vector2 (0f, Input.GetAxisRaw ("Vertical")) :
				new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		}

		if (collisions.below && (lookDirection.y == -1)) {

			isCurrentlyCrouching = true;

			input = new Vector2 (input.x * crouchingMovementPenalty, input.y);

		}

		return input;

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
