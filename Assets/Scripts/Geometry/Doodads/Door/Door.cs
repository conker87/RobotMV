using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// TODO: ANIMATIONS REQUIRE THEIR BOX COLLIDERS TO MATCH THEIR SPRITES, CHECK _OLD FOLDER FOR DETAILS
	// TODO: Refactor this base class & it's inherits.

	[Header("Door Details")]
	[Range(0, 20)] public int doorLevel = 0;
	[Range(0, 60)] public float doorOpenLength = 5f;
	protected float timeToClose;
	public bool willDoorStayOpen = false;

	[Header("Door/Player Interaction")]
	public LayerMask circleLayerMask;
	[Range(0, 20)] public float doorCheckDistance = 2f;

	protected float timeToNextCheck;
	protected Animator anim;
	protected Collider2D circle;

	protected Projectile hit;

	protected virtual void Start () {
	
		anim = GetComponent<Animator> ();

	}
	
	protected virtual void Update () {
			
		DoCircleCheck (willDoorStayOpen);

	}

	protected void DoCircleCheck(bool willStayOpen, bool doCheck = true) {

		if (Time.time > timeToNextCheck) {

			circle = Physics2D.OverlapCircle (transform.position, doorCheckDistance, circleLayerMask);

			if (!doCheck || circle == null) {

				if (Time.time > timeToClose && !willStayOpen) {

					CloseDoor();

				}

			} else {

				timeToNextCheck = Time.time + Constants.Tick;

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

		Gizmos.DrawWireSphere (transform.position, doorCheckDistance);

	}

}
