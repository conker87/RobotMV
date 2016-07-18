using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenDoorOnProjectileHit : MonoBehaviour {

	[Range(0, 10)]
	public int doorLevel;
	public float doorCheckDistance = 2f;
	public float timeUntilNextFrame = 0.5f;

	public LayerMask circleLayerMask;

	public List<GameObject> anims;

	float timeToNextCheck, checkEverySeconds = 1f;
	Collider2D circle;


	bool doOpening = false, doClosing = false, isClosed = true, isOpen = false;
	int i = 0;

	Projectile hit;

	void Update() {

		if (Time.time > timeToNextCheck && isOpen) {

			circle = Physics2D.OverlapCircle (gameObject.transform.position, doorCheckDistance, circleLayerMask);

			if (circle == null) {

				isOpen = false;
				Invoke ("DoClose", timeUntilNextFrame);

			}

			timeToNextCheck = Time.time + checkEverySeconds;

		} 

		if (doOpening) {

			isClosed = doOpening = false;
			Invoke ("DoOpen", timeUntilNextFrame);

		}

	}

	void OnTriggerEnter2D(Collider2D other) {

		if ((hit = other.gameObject.GetComponent<Projectile>()) != null) {

			if (hit.weaponDoorLevel >= doorLevel  && isClosed) {
				
				doOpening = true;

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

				isOpen = true;
				isClosed = !isOpen;

			}

			Invoke ("DoOpen", timeUntilNextFrame);

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

				isClosed = true;
				isOpen = !isClosed;

			}

			Invoke ("DoClose", timeUntilNextFrame);

		}

	}

}
