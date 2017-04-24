using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoomsAroundMe : MonoBehaviour {

	RoomManager rm;

	[SerializeField]
	float loadRange = 200f;

	[SerializeField]
	LayerMask roomLayer;

	[SerializeField]
	float checkUpdateRate = 1f;

	float timeCheckUpdate;

	Collider2D[] circleCollider;

	void Start () {

		rm = FindObjectOfType<RoomManager> ();

	}
	
	void Update () {

		if (Time.time > timeCheckUpdate) {

			Debug.Log ("Checking...");

			circleCollider = Physics2D.OverlapCircleAll (transform.position, loadRange, roomLayer);

			GameObject previousPrefab = null;

			foreach (var room in rm.gameRooms) {

				room.TiledPrefab.SetActive (true);

				foreach (var col in circleCollider) {

					if (previousPrefab != null && (previousPrefab.GetInstanceID() == room.TiledPrefab.GetInstanceID())) {

						break;

					}

					previousPrefab = room.TiledPrefab;

					Debug.Log (col.gameObject.GetInstanceID() + " checking against: " + room.Rooms.gameObject.GetInstanceID());

					if (col.gameObject.GetInstanceID() != room.Rooms.gameObject.GetInstanceID()) {

						room.TiledPrefab.SetActive (false);

					}

				}

			}
				
			timeCheckUpdate = Time.time + checkUpdateRate;

		}

	}

	void OnDrawGizmos() {

		Gizmos.DrawWireSphere (transform.position, loadRange);

	}
}
