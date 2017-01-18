using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_SetParentMovementSpeed : MonoBehaviour {

	public Projectile p;

	public float movementSpeed;

	void Start() {

		p.MovementSpeed = movementSpeed;

	}

}
