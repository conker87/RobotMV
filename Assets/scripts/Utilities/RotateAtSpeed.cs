using UnityEngine;
using System.Collections;


public class RotateAtSpeed : MonoBehaviour {

	/// <summary>
	/// Rotational speed in degrees per second.
	/// </summary>
	public float rotationalSpeed = 45f;

	void Update () {
		
		transform.Rotate (0f, 0f, rotationalSpeed * Time.deltaTime); //rotates 50 degrees per second around z axis
	
	}
}
