using UnityEngine;
using System.Collections;

public abstract class Weapon : Usables {

	public virtual void Shoot() {

	}

	public virtual void Shoot (Vector3 ShootLocationPosition) {

		Shoot ();

	}

	public virtual void ShootEnd() {
			
		cooldownTime = Time.time + Cooldown;

	}

}