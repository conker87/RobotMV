using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	float movementSpeed = 5f;

	public Vector3 Direction;

	void Update () {
	
		transform.position += Direction * Time.deltaTime * movementSpeed;

	}
}
