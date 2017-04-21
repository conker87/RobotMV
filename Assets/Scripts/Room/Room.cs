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

	[SerializeField]
	int roomID = -1;

	RoomState roomState = RoomState.ENEMIES_ENABLED;

	[SerializeField]
	bool isCurrentlyInThisRoom = false, hasShownAreaName = false;

	[SerializeField]
	Text areaNameText;

	// Enemies
	[SerializeField]
	bool isBossRoom = false;

	float disableEnemiesIn = 2f;

	public Transform enemiesSpawnParent;

	public List<EnemySpawns> enemiesToSpawnInRoom = new List<EnemySpawns>();
	public List<Enemy> enemiesInRoom = new List<Enemy>();
	bool hasAlreadyResetEnemies = false;

	public List<BombableWall> bombableWalls = new List<BombableWall>();
	Coroutine disableEnemies = null;

	protected virtual void Start () {

		// There's really no need to add the GameObject into the field for every room, so let's just find the GameObject itself.
		areaNameText = GameObject.Find ("AreaName").GetComponent<Text>();

		roomID = CameraManager.GetAreaIDForRoom (gameObject);

		// Find the transform of the parent.
		if (enemiesSpawnParent == null) {

			enemiesSpawnParent = gameObject.transform.parent;

		}

		// We had issues in Editor mode where enemies spawned in through the following code would persist through sessions.
		Enemy[] enemies = enemiesSpawnParent.GetComponentsInChildren<Enemy> ();
		foreach (Enemy enemy in enemies) {
			
			#if UNITY_EDITOR
			DestroyImmediate (enemy.gameObject);
			#else
			Destroy(enemy.gameObject);
			#endif

		}

		enemies = null;

		// Adds the enemies in the Room to the List that will take care of the enemies.
		enemiesInRoom.Clear ();
		foreach (EnemySpawns enemySpawn in enemiesToSpawnInRoom) {

			if (enemiesSpawnParent == null) {

				break;

			}

			Enemy current = Instantiate (enemySpawn.enemyToSpawn, enemySpawn.spawnLocation, Quaternion.identity, enemiesSpawnParent) as Enemy;
			enemiesInRoom.Add (current);

		}

		EnableBombableWalls ();

	}
	
	// Update is called once per frame
	protected virtual void Update () {

		isCurrentlyInThisRoom = (CameraManager.GetCurrentAreaIndex() == roomID) ? true : false;

		// Debug.Log (RoomNameLocalisationID + ": " + roomState.ToString());

		if (isCurrentlyInThisRoom) {

			if (disableEnemies != null) {

				StopCoroutine (disableEnemies);
				disableEnemies = null;
				Debug.Log ("StopCoroutine (disableEnemies) in " + RoomNameLocalisationID);

			}

			if (roomState == RoomState.ENEMIES_ENABLED) {

				EnableEnemies ();
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
					EnableBombableWalls ();
				}

			}

			if (enemiesInRoom.Count > 0) {
				
				if (hasShownAreaName && roomState == RoomState.ENEMIES_DISABLED) {

					disableEnemies = StartCoroutine (DisableEnemies ());
				}

				if (hasShownAreaName && roomState == RoomState.ENEMIES_RESET) {
				
					ResetEnemies ();

				}

			} else {

				roomState = RoomState.ENEMIES_ENABLED;

			}

			hasShownAreaName = false;

		}

	}

	void EnableBombableWalls() {

		for (int i = 0; i < bombableWalls.Count; i++) {

			bombableWalls [i].gameObject.SetActive (true);

		}

	}

	void EnableEnemies() {

		for (int i = 0; i < enemiesInRoom.Count; i++) {

			if (enemiesToSpawnInRoom [i].hasBeenKilledPerm) {

				continue;

			}

			enemiesInRoom [i].gameObject.SetActive (true);

		}

		roomState = RoomState.WAITING;

	}

	IEnumerator DisableEnemies() {

		roomState = RoomState.ENEMIES_RESET;
		Debug.Log ("StartCoroutine (disableEnemies) in " + RoomNameLocalisationID);

		yield return new WaitForSeconds(disableEnemiesIn);
		DisableEnemiesImmediate ();
		disableEnemies = null;

	}

	void DisableEnemiesImmediate() {

		foreach (Enemy enemy in enemiesInRoom) {

			enemy.gameObject.SetActive (false);

		}

		if (!hasAlreadyResetEnemies) {

			roomState = RoomState.ENEMIES_RESET;

		}
	}

	void ResetEnemies() {

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

		DisableInSeconds disable = areaNameText.GetComponent<DisableInSeconds> ();

		areaNameText.text = name + " ";
		disable.Reset (areanameDestroy);

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