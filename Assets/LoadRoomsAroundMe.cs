using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoomsAroundMe : MonoBehaviour {

	[SerializeField]
	float loadRange = 200f;

	[SerializeField]
	LayerMask roomLayer;

	[SerializeField]
	float checkUpdateRate = 1f;

	float timeCheckUpdate;

	Collider2D[] circleCollider;

	void Start () {
		
	}
	
	void Update () {

		if (Time.time > timeCheckUpdate) {

			circleCollider = Physics2D.OverlapCircleAll (transform.position, loadRange, roomLayer);

			if (circleCollider.Length > 0) {

				// LOAD

			}
				
			timeCheckUpdate = Time.time + checkUpdateRate;

		}

	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, loadRange);

	}
}
