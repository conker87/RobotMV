using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour {

	[Header("Animations")]
	public float timeUntilNextFrame = 0.5f;
	public List<GameObject> anims;

	[Header("Door Details")]
	[Range(0, 20)] public float doorOpenLength = 5f;
	[Range(0, 20)] public int doorLevel = 0;
	public bool willDoorStayOpen = false;

	[Header("Door/Player Interaction")]
	[Range(0, 20)] public float doorCheckDistance = 2f;
	public LayerMask circleLayerMask;

	[Header("System")]
	[SerializeField] public DoorState doorState;

	protected float timeToNextCheck, tick = .5f, timeToClose;
	protected Collider2D circle;

	protected bool disableCircleCheck = false;

	protected int i = 0;
	protected Projectile hit;

	protected virtual void Start() {

		doorState = DoorState.CLOSED;

	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, doorCheckDistance);

	}

	public virtual void Update() {

		DoCircleCheck (true);

		UpdateEnd ();

	}

	protected void UpdateEnd() {

		if (circle != null && doorState == DoorState.OPEN) {

			timeToClose = Time.time + 2f;

		}

		if (doorState == DoorState.OPEN_BEGIN) {

			doorState = DoorState.OPENING;
			Invoke ("DoOpen", timeUntilNextFrame);

		}


		if (doorState == DoorState.CLOSE_BEGIN) {

			doorState = DoorState.CLOSING;
			Invoke ("DoClose", timeUntilNextFrame);

		}

	}

	protected void DoCircleCheck(bool enabled) {

		if (enabled && Time.time > timeToNextCheck && doorState == DoorState.OPEN) {

			circle = Physics2D.OverlapCircle (gameObject.transform.position, doorCheckDistance, circleLayerMask);

			if (circle == null) {

				if (Time.time > timeToClose && !willDoorStayOpen) {

					doorState = DoorState.CLOSE_BEGIN;

				}

			} else {

				timeToNextCheck = Time.time + tick;

			}

		}

	}

	void DoOpen() {

		if (i == 1) {

			gameObject.GetComponent<BoxCollider2D> ().enabled = false;

		}

		if (i < anims.Count - 1) {
			
			anims [i].SetActive (false);
			i++;
			anims [i].SetActive (true);

			if (i == anims.Count - 1) {

				doorState = DoorState.OPEN;
				timeToClose = Time.time + doorOpenLength;

			} else {

				Invoke ("DoOpen", timeUntilNextFrame);

			}

		}

	}

	void DoClose() {

		if (i == 1) {

			gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		}

		if (i > 0) {

			anims [i].SetActive (false);
			i--;
			anims [i].SetActive (true);

			if (i == 0) {

				doorState = DoorState.CLOSED;

			} else {

				Invoke ("DoClose", timeUntilNextFrame);

			}

		}

	}

}

public enum DoorState { CLOSED, OPEN_BEGIN, OPENING, OPEN, CLOSE_BEGIN, CLOSING };