using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Room : MonoBehaviour {

	public string RoomNameLocalisationID = "LocalisationID <FIXME>";
	public float MaxRoomZoomLevel = 7.5f, LevelZoomTime = 1f;

	[SerializeField]
	float areanameDestroy = 3f;

	float t = 0.0f;

	PixelPerfectCamera pixel;

	[SerializeField]
	int roomID = -1;

	[SerializeField]
	bool isCurrentlyInThisRoom = false, hasShownAreaName = false;

	[SerializeField]
	Text areaNameText;

	[SerializeField]
	bool isBossRoom = false;

	public List<EnemySpawns> enemiesInRoom = new List<EnemySpawns>();

	protected virtual void Start () {

		// There's really no need to add the GameObject into the field for every room, so let's just find the GameObject itself.
		areaNameText = GameObject.Find ("AreaName").GetComponent<Text>();

//		Debug.Log (this);
		// AreaName_Panel = GameObject.Find ("AreaName_Panel");

		roomID = CameraManager.GetAreaIDForRoom (gameObject);

		pixel = FindObjectOfType<Camera> ().GetComponent<PixelPerfectCamera> ();

	}
	
	// Update is called once per frame
	protected virtual void Update () {

		isCurrentlyInThisRoom = (CameraManager.GetCurrentAreaIndex() == roomID) ? true : false;

		if (isCurrentlyInThisRoom) {

			//pixel.targetCameraHalfWidth = MaxRoomZoomLevel;
			LerpZoomOverTime (LevelZoomTime);

			if (!hasShownAreaName) {

				//pixel.adjustCameraFOV ();				
				ShowAreaNameOnScreen (RoomNameLocalisationID); // Localisation.GetText(RoomNameLocalisationID);

			}

		} else {

			hasShownAreaName = false;

		}

	}

	void ShowAreaNameOnScreen(string name) {

		DisableInSeconds disable = areaNameText.GetComponent<DisableInSeconds> ();

		areaNameText.text = "< " + name + " > ";
		disable.Reset (areanameDestroy);

		hasShownAreaName = true;

	}

	void LerpZoomOverTime(float time) {

		float org = pixel.targetCameraHalfWidth;

		pixel.targetCameraHalfWidth = Mathf.Lerp (org, MaxRoomZoomLevel, t);
		pixel.adjustCameraFOV ();

		t += (Time.deltaTime * (1 / time));

		if (t > 1) {

			t = 0;

		}

	}

	void OnDrawGizmos() {

		if (enemiesInRoom.Count > 0) {

			Gizmos.color = Color.cyan;
			float size = .5f;

			foreach (var item in enemiesInRoom) {

				Vector3 position = item.spawnLocation;
				Gizmos.DrawLine(position - Vector3.up * size, position + Vector3.up * size);
				Gizmos.DrawLine(position - Vector3.left * size, position + Vector3.left * size);

			}

		}
	}

}

[System.Serializable]
public struct EnemySpawns {

	public Vector3 spawnLocation;
	public Enemy enemyToSpawn;

}