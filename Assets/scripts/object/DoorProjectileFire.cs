using UnityEngine;
using System.Collections;

public class DoorProjectileFire : Door {
	
	// Update is called once per frame
	public override void Update () {
		
		base.Update ();

	}

	void OnTriggerEnter2D(Collider2D other) {

		if ((hit = other.gameObject.GetComponent<Projectile>()) != null) {

			if (hit.weaponDoorLevel >= doorLevel  && isClosed) {

				doOpening = true;

			}

		}

	}
}
