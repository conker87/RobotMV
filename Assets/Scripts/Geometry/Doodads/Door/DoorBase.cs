using UnityEngine;
using System.Collections;

// Refactored: 02/02/2017
public class DoorBase : MonoBehaviour {

	// TODO: ANIMATIONS REQUIRE THEIR BOX COLLIDERS TO MATCH THEIR SPRITES, CHECK _OLD FOLDER FOR DETAILS
	// TODO: Refactor this base class & it's inherits.

	// DoorBase should never be directly added to a GameObject, it is the base class for all Doors, as the script itself has no interactions.

	[Header("Door Details")]
	[Range(0, 20)] public int DoorLevel = 0;

	public bool WillDoorStayOpen = false;
	[Tooltip("The length, in seconds, the door will stay open for if the door is set to close.")][Range(0, 60)]
	public float DoorOpenLength = 5f;

	protected float timeToClose;

	[Header("Door/Player Interaction")]

	[Tooltip("The Layermask Collider the door checks around it's surroundings for.")]
	public LayerMask PlayerLayerMask;
	[Tooltip("The distance, in units, the door checks in.")][Range(0, 20)]
	public float DoorCheckDistance = 2f;

	protected float timeToNextCheck;
	protected Animator anim;
	protected Collider2D playerOverlapCircle;

	protected virtual void Start () {
	
		anim = GetComponent<Animator> ();

	}
	
	protected virtual void Update () {
			
		CheckForPlayerInSurrounding ();

	}

	protected virtual void CheckForPlayerInSurrounding(bool doCheck = true) {

		if (!doCheck) {

			return;

		}

		if (Time.time > timeToNextCheck) {

			playerOverlapCircle = Physics2D.OverlapCircle (transform.position, DoorCheckDistance, PlayerLayerMask);

			if (playerOverlapCircle == null) {

				if (Time.time > timeToClose && !WillDoorStayOpen) {

					CloseDoor();

				}

			} else {

				timeToNextCheck = Time.time + Constants.DoorCheckingTick;

			}

		}

	}

	public void OpenDoor() {

		anim.SetBool ("open", true);

	}

	public void CloseDoor() {

		anim.SetBool ("open", false);

	}

	public bool IsDoorOpen() {

		return anim.GetBool ("open");

	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, DoorCheckDistance);

	}

}
