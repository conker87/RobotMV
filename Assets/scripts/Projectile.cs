using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float movementSpeed = 1f;

	public Vector3 Direction;

	void Update () {

		transform.position += Direction.normalized * Time.deltaTime * movementSpeed;

	}

}
