using UnityEngine;
using System.Collections;

public class DoorProjectileFire : Door {
	
	// Update is called once per frame
	public override void Update () {
		
		base.Update ();

	}

	void OnTriggerEnter2D(Collider2D other) {

		hit = other.gameObject.GetComponent<Projectile> ();

		if (hit != null && hit.projectileType == ProjectileType.PLAYER) {

			if (hit.weaponLevel >= doorLevel && doorState == DoorState.CLOSED) {

				doorState = DoorState.OPEN_BEGIN;

			}

		}

	}

}
