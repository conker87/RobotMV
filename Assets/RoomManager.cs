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

		foreach (var item in locationArray) {

			// Populate the children list to itterate through.
			List<Transform> children = new List<Transform>();
			children.Clear ();

			foreach (Transform trans in item.transform) {

				Debug.Log (item.name + ": " + trans);

				if (trans.name == "Doodads" || trans.name == "PowerUps"){
	
					continue;

				}

				children.Add (trans);

			}

		}

		GameObject tiledPrefab = null;
		List<Room> rooms = new List<Room> ();

		for (int i = 0; i < children.Count - 1; i++) {

			if (children [i].GetComponent<Room> () != null) {

				rooms.Add (children [i].GetComponent<Room>());
				continue;

			} else {

				tiledPrefab = children [i].gameObject;

			}

		}

		if (tiledPrefab == null) {

			continue;

		}

		foreach (var room in rooms) {

			PrefabRooms newPrefabRoom = new PrefabRooms (tiledPrefab, room);
			gameRooms.Add (newPrefabRoom);

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