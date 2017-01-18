﻿using UnityEngine;
using System.Collections;

public class DoorProjectile : Door {

	void OnTriggerEnter2D(Collider2D other) {

		hit = other.gameObject.GetComponent<Projectile> ();

		if (hit != null && hit.ProjectileType == ProjectileType.PLAYER) {

			if (hit.WeaponLevel >= doorLevel) {

				anim.SetBool ("open", true);
				timeToClose = Time.time + doorOpenLength;

			}

		}

	}

}
