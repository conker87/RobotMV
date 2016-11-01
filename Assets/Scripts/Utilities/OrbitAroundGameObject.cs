using UnityEngine;
using System.Collections;

public class OrbitAroundGameObject : MonoBehaviour 
{
	public Transform center;
	public Vector3 axis = Vector3.up;
	public float radius = 2.0f, radiusChangeMultiplier, minRadius, maxRadius;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 80.0f;

	void Start() {

		center = GameObject.FindObjectOfType<Player> ().transform; //FindGameObjectWithTag ("Player").transform;

		radius = Random.Range (minRadius, maxRadius);

		transform.position = (transform.position - center.position).normalized * radius + center.position;

	}

	void Update() {

		transform.RotateAround (center.position, axis, rotationSpeed * Time.deltaTime);

		Vector3 desiredPosition = (transform.position - center.position).normalized * radius + center.position;

		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
	}

}

