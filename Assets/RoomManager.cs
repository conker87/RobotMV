using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public GameObject worldParent;

	[SerializeField]
	public List<PrefabRooms> gameRooms = new List<PrefabRooms> ();

	// Use this for initialization
	void Start () {

		PopulateRooms ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void PopulateRooms() {

		gameRooms.Clear ();
		SetLocationBasedOnGameObjectName[] locationArray;

		locationArray = worldParent.GetComponentsInChildren<SetLocationBasedOnGameObjectName> ();

		List<Transform> children = new List<Transform>();

		foreach (var item in locationArray) {

			children.Clear ();

			// Populate the children list to itterate through.
			foreach (Transform trans in item.transform) {

				// Debug.Log (item.name + ": " + trans);

				if (!(trans.name.Contains("ROOM") || trans.name.Contains("TiledPrefab"))) {
	
					continue;

				}

				children.Add (trans);

			}

			GameObject tiledPrefab = null;
			List<GameObject> rooms = new List<GameObject> ();

			foreach (var child in children) {
				
				if (child.GetComponent<Room> () != null) {

					rooms.Add (child.gameObject);

				} else {

					tiledPrefab = child.gameObject;

				}

			}

			if (tiledPrefab == null) {

				continue;

			}

			foreach (var room in rooms) {

				PrefabRooms newPrefabRoom = new PrefabRooms (tiledPrefab, room.GetComponent<Room>());
				gameRooms.Add (newPrefabRoom);

			}

		}


	}

}


[System.Serializable]
public struct PrefabRooms {

	public PrefabRooms(GameObject pre, Room r) { 

		this.TiledPrefab = pre;
		this.Rooms = r;

	}

	public GameObject TiledPrefab;
	public Room Rooms; 


}