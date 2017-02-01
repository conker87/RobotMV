using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour {

	public string RoomNameLocalisationID = "LocalisationID <FIXME>";

	[SerializeField]
	int roomID = -1;

	[SerializeField]
	bool isCurrentlyInThisRoom = false, hasShownAreaName = false;

	[SerializeField]
	Text areaNameText;
	GameObject AreaName_Panel;

	// Use this for initialization
	[ExecuteInEditMode]
	protected virtual void Start () {

		// There's really no need to add the GameObject into the field for every room, so let's just find the GameObject itself.
		areaNameText = GameObject.Find ("AreaName").GetComponent<Text>();
		AreaName_Panel = GameObject.Find ("AreaName_Panel");

		roomID = CameraManager.GetAreaIDForRoom (gameObject);

	}
	
	// Update is called once per frame
	protected virtual void Update () {

//		This is no longer needed now that CameraManager always loads first.
//		if (roomID < 0) {
//
//			roomID = CameraManager.GetAreaIDForRoom (gameObject);
//
//		}

		isCurrentlyInThisRoom = (CameraManager.GetCurrentAreaIndex() == roomID) ? true : false;

		if (isCurrentlyInThisRoom) {

			if (!hasShownAreaName) {
				
				ShowAreaNameOnScreen (RoomNameLocalisationID); // Localisation.GetText(RoomNameLocalisationID);

			}

		} else {

			hasShownAreaName = false;

		}

	}

	void ShowAreaNameOnScreen(string name) {

		DisableInSeconds disable = areaNameText.GetComponent<DisableInSeconds> ();

		areaNameText.text = "< " + name + " > ";
		disable.Reset (3f);

		hasShownAreaName = true;

	}

}
