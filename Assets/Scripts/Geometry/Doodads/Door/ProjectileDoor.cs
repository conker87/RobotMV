using UnityEngine;
using System.Collections;

// Refactored: 02/02/2017
public class ProjectileDoor : DoorBase {

	protected ProjectileBase projectileHit;

	void OnTriggerEnter2D(Collider2D other) {

		// Checks to see if the Collider is a Projectile, then checks if the Projectile is the Player's,
		//	then checks if the WeaponLevel of the Projectie is more than/equal to the current DoorLevel,
		//	if this is true then the door should open.
		if (	(projectileHit = other.gameObject.GetComponent<ProjectileBase> ()) != null &&
				projectileHit.ProjectileType == ProjectileType.PLAYER &&
				projectileHit.WeaponLevel >= DoorLevel) {

			OpenDoor ();

			if (!WillDoorStayOpen) {
			
				timeToClose = Time.time + DoorOpenLength;

			}

		}

	}

}
