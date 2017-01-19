using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MovementController {

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

		input = new Vector2 (0f, 0f);

		if (!InputManager.Current.GetButton ("FixLocation")) {
		
			if (InputManager.Current.isUsingController) {

				input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

			} else {

				float movement = 0f;

				// TODO: Must check to see if also Crouching (y == -1)

				if (InputManager.Current.GetButton ("Left")) {

					movement = -1f;
					currentlyLookingLeft = true;

				} else if (InputManager.Current.GetButton ("Right")) {

					movement = 1;
					currentlyLookingLeft = false;

				} else {

					movement = 0f;

				}

				input = new Vector2 (movement, 0f);

			}

		}

		Movement (input);

		sr.flipX = (currentlyLookingLeft) ? true : false;

		ShootWeapon ();

		if (InputManager.Current.GetButtonDown ("Item")) {
		
			UseItem ();

		}


		if (InputManager.Current.GetButtonDown ("DEBUG_ResetScene")) {

			ReloadLevel ();

		}

	}

	protected override void ResetJumpingVarsOnCollisionBelow() {

		if (collisions.below) {
			
			hasJumped = false;
			hasDoubleJumped = false;
			hasTripleJumped = false;

		}

	}

	public override void Movement(Vector2 input) {

		if (InputManager.Current.GetButtonDown("Jump") && Player.Current.CollectablesD["JUMP_TRIPLE"] && hasJumped) {

			if (hasDoubleJumped && !hasTripleJumped) {

				velocity.y = jumpVelocity;

				hasTripleJumped = true;

			}

		}

		if (InputManager.Current.GetButtonDown("Jump") && Player.Current.CollectablesD["JUMP_DOUBLE"] && (!collisions.below || hasJumped)) {

			if (!hasDoubleJumped) {

				velocity.y = jumpVelocity;

				hasDoubleJumped = true;
				hasJumped = true;

			}

		}

		if (collisions.below) {
			
			if (InputManager.Current.GetButtonDown("Jump") && Player.Current.CollectablesD["JUMP"]) {

				velocity.y = jumpVelocity;

				hasJumped = true;

			}

		}
			
		if (input != Vector2.zero) {
			currentlyLookingLeft = (input.x < 0) ? true : false;
		}

		base.Movement (input);

	}

	void ShootWeapon() {

		if (Player.Current.CurrentWeapon != null) {

			Player.Current.CurrentWeapon.Shoot (ShootLocation.transform.position, InputManager.Current.GetShootingDirection(currentlyLookingLeft));

		}

	}

	void UseItem() {
		
		if (Player.Current.CurrentItem != null) {

			Player.Current.CurrentItem.Use();
			//Player.Current.CurrentWeapon.ShootAfter ();

		}

	}

	void ReloadLevel() {

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}

}
