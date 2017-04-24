using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour {

	public string RoomNameLocalisationID = "";
	//public float MaxRoomZoomLevel = 7.5f, LevelZoomTime = 1f;

	[SerializeField]
	float destroyAreaNameUIInSeconds = 3f;

	[SerializeField]
	int roomID = -1;

	RoomState roomState = RoomState.ENEMIES_ENABLED;

	[SerializeField]
	bool isCurrentlyInThisRoom = false, hasShownAreaName = false;

	[SerializeField]
	Text UIAreaNameField;

	// Enemies
	[SerializeField]
	bool isBossRoom = false;

	float disableEnemiesIn = 2f;

	public Transform enemiesSpawnParent;

	[SerializeField]
	List<EnemySpawns> enemiesToSpawnInRoom = new List<EnemySpawns>();
	List<Enemy> enemiesInRoom = new List<Enemy>();
	bool hasAlreadyResetEnemies = false;
	Coroutine disableEnemies = null, resetWalls = null;

	[SerializeField]
	List<BombableWall> bombableWalls = new List<BombableWall>();

	protected virtual void Start () {

		// TODO: There should be a UIManager that manages all of the UI shit.
		UIAreaNameField = GameObject.Find ("AreaName").GetComponent<Text>();

		roomID = CameraManager.GetAreaIDForRoom (gameObject);
		RoomNameLocalisationID = (RoomNameLocalisationID.Equals("")) ? gameObject.name : RoomNameLocalisationID;
		enemiesSpawnParent = (enemiesSpawnParent == null) ? gameObject.transform.parent : enemiesSpawnParent;
		isCurrentlyInThisRoom = (CameraManager.GetCurrentAreaIndex() == roomID) ? true : false;

		hasShownAreaName = false;

		// We had issues in Editor mode where enemies spawned in through the following code would persist through sessions.
		//	Update, 17-04-24: [ExecuteInFuckingEditor]. This is no longer needed.
		//		Enemy[] enemies = enemiesSpawnParent.GetComponentsInChildren<Enemy> ();
		//		foreach (Enemy enemy in enemies) {
		//			
		//			#if UNITY_EDITOR
		//			DestroyImmediate (enemy.gameObject);
		//			#else
		//			Destroy(enemy.gameObject);
		//			#endif
		//
		//		}
		//
		//		enemies = null;

		SpawnEnemiesInitially ();
		EnableBombableWallsInRoomImmediate ();

	}
	
	// Update is called once per frame
	protected virtual void Update () {

		isCurrentlyInThisRoom = (CameraManager.GetCurrentAreaIndex() == roomID) ? true : false;

		if (isCurrentlyInThisRoom) {

			if (disableEnemies != null) {

				StopCoroutine (disableEnemies);
				disableEnemies = null;
				Debug.Log ("StopCoroutine (disableEnemies) in " + RoomNameLocalisationID);

			}

			if (resetWalls != null) {

				StopCoroutine (resetWalls);
				resetWalls = null;
				Debug.Log ("StopCoroutine (resetWalls) in " + RoomNameLocalisationID);

			}

			if (roomState == RoomState.ENEMIES_ENABLED) {

				EnableEnemiesInRoom ();
			}

			//LerpZoomOverTime (LevelZoomTime);

			if (!hasShownAreaName) {

				UI_ShowAreaNameOnScreen (RoomNameLocalisationID); // Localisation.GetText(RoomNameLocalisationID);

			}

		} else {

			if (hasShownAreaName && roomState == RoomState.WAITING) {

				hasAlreadyResetEnemies = false;
				roomState = RoomState.ENEMIES_DISABLED;

				if (bombableWalls.Count > 0) {
					EnableBombableWallsInRoom ();
				}

			}

			if (enemiesInRoom.Count > 0) {
				
				if (hasShownAreaName && roomState == RoomState.ENEMIES_DISABLED) {

					disableEnemies = StartCoroutine (DisableEnemiesInRoomAfter ());
				}

				if (hasShownAreaName && roomState == RoomState.ENEMIES_RESET) {
				
					ResetEnemyLocationsInRoom ();

				}

			} else {

				roomState = RoomState.ENEMIES_ENABLED;

			}

			hasShownAreaName = false;

		}

	}

	IEnumerator EnableBombableWallsInRoom() {

		Debug.Log ("StartCoroutine (EnableBombableWallsInRoom) in " + RoomNameLocalisationID);

		yield return new WaitForSeconds(disableEnemiesIn);
		EnableBombableWallsInRoomImmediate ();
		resetWalls = null;

	}

	void EnableBombableWallsInRoomImmediate() {

		for (int i = 0; i < bombableWalls.Count; i++) {

			// If the current field in the list is null, then skip it. More than likely caused by me deleting unneeded walls and not replacing the list.
			if (bombableWalls [i] == null) {

				continue;

			}

			bombableWalls [i].gameObject.SetActive (true);

		}

	}

	void SpawnEnemiesInitially() {

		enemiesInRoom.Clear ();

		if (enemiesToSpawnInRoom.Count > 0) {

			foreach (EnemySpawns enemySpawn in enemiesToSpawnInRoom) {

				if (enemiesSpawnParent == null) {

					break;

				}

				if (enemySpawn.enemyToSpawn == null) {

					continue;

				}

				Enemy current = Instantiate (enemySpawn.enemyToSpawn, enemySpawn.spawnLocation, Quaternion.identity, enemiesSpawnParent) as Enemy;
				enemiesInRoom.Add (current);

			}

		}

	}

	void EnableEnemiesInRoom() {

		for (int i = 0; i < enemiesInRoom.Count; i++) {

			if (enemiesToSpawnInRoom [i].hasBeenKilledPerm) {

				continue;

			}

			enemiesInRoom [i].gameObject.SetActive (true);

		}

		roomState = RoomState.WAITING;

	}

	IEnumerator DisableEnemiesInRoomAfter() {

		roomState = RoomState.ENEMIES_RESET;
		Debug.Log ("StartCoroutine (disableEnemies) in " + RoomNameLocalisationID);

		yield return new WaitForSeconds(disableEnemiesIn);
		DisableEnemiesInRoomImmediate ();
		disableEnemies = null;

	}

	void DisableEnemiesInRoomImmediate() {

		foreach (Enemy enemy in enemiesInRoom) {

			enemy.gameObject.SetActive (false);

		}

		if (!hasAlreadyResetEnemies) {

			roomState = RoomState.ENEMIES_RESET;

		}
	}

	void ResetEnemyLocationsInRoom() {

		for (int i = 0; i < enemiesInRoom.Count; i++) {

			if (enemiesToSpawnInRoom [i].hasBeenKilledPerm) {

				enemiesInRoom [i].gameObject.SetActive (false);
				continue;

			}

			enemiesInRoom [i].gameObject.transform.position = enemiesToSpawnInRoom [i].spawnLocation;

		}

		roomState = RoomState.ENEMIES_ENABLED;

		hasAlreadyResetEnemies = true;

	}



	void UI_ShowAreaNameOnScreen(string name) {

		UIAreaNameField.text = name + " ";
		UIAreaNameField.GetComponent<DisableInSeconds> ().Reset (destroyAreaNameUIInSeconds);

		hasShownAreaName = true;

	}

	void OnDrawGizmos() {

		if (enemiesToSpawnInRoom.Count > 0) {

			Gizmos.color = Color.cyan;
			float size = .5f;

			foreach (var item in enemiesToSpawnInRoom) {

				Vector3 position = item.spawnLocation;
				Gizmos.DrawLine(position - Vector3.up * size, position + Vector3.up * size);
				Gizmos.DrawLine(position - Vector3.left * size, position + Vector3.left * size);

			}

		}
	}

}

[System.Serializable]
public struct EnemySpawns {

	public EnemySpawns(Vector3 _spawnLocation, 	Enemy _enemyToSpawn, bool _isBoss = false,
		bool _canBeKilledPerm = false, bool _hasBeenKilledPerm = false) {

		spawnLocation = _spawnLocation;
		enemyToSpawn = _enemyToSpawn;
		isBoss = _isBoss;
		canBeKilledPerm = _canBeKilledPerm;
		hasBeenKilledPerm = _hasBeenKilledPerm;

	}

	public Vector3 spawnLocation;
	public Enemy enemyToSpawn;
	public bool isBoss;
	public bool canBeKilledPerm;
	public bool hasBeenKilledPerm;

}

public enum RoomState { WAITING, ENEMIES_ENABLED, ENEMIES_DISABLED, ENEMIES_RESET };