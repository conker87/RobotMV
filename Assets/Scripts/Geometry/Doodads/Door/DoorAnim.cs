using UnityEngine;
using System.Collections;

public class DoorAnim : MonoBehaviour {

	[Header("TODO:")]
	[TextArea(1, 2)]
	public string TODO = "ANIMATIONS REQUIRE THEIR BOX COLLIDERS TO MATCH THEIR SPRITES, CHECK _OLD FOLDER FOR DETAILS.";

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

	void Start () {
	
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

					anim.SetBool ("open", false);

				}

			} else {

				timeToNextCheck = Time.time + Constants.Tick;

			}

		}

	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, doorCheckDistance);

	}

}
