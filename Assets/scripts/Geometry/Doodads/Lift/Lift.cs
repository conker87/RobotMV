using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour {

	public Vector2[] StopPoints;
	float startTime;

	void Start () {
	
		startTime = Time.time;

	}

	void Update () {
	


	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		Debug.Log (other.gameObject.tag);

	}

}
