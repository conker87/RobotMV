using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float	movementSpeed = 1f;
	[Range(0, 10)]
	public int		weaponDoorLevel;
	public string	sourceWeapon = "";

	public Vector3 Direction;			// DIRECTION NEEDS TO .normalized!!

	void Update () {

		transform.position += Direction * Time.deltaTime * movementSpeed;

	}

}
