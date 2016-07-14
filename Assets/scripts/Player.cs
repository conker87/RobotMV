using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
//	public static PlayerStats _playerStats;
//
//	GameObject canvas;
//	Text[] textValue;
//
//	public static string currentPossession = "human";
//
//	string foundPres = "";
//	Object prefab = null;
//	bool doPrefabChange = false;
//	GameObject[] search;
//
//	bool hasJustLoaded = false;
//
//	void Awake()
//	{
//		canvas = GameObject.Find("GUICanvas");
//		textValue = canvas.GetComponentsInChildren<Text>();
//
//		doSearchForPrefabs("PLAYER.CS AWAKE()");
//		removeOldPrefabsFromMemory(gameObject.name);
//
//		hasJustLoaded = true;
//	}
//
//	void Start()
//	{
//		if (_playerStats == null)
//		{
//			_playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
//		}
//
//		_playerStats.currentHealth = _playerStats.defaultHealth;
//
//		Debug.Log ("CHealth: " + _playerStats.currentHealth + " DHealth: " + _playerStats.defaultHealth);
//	}
//
//	void Update()
//	{
//		if (hasJustLoaded)
//		{
//			hasJustLoaded = false;
//		}
//
//		// Do Debug Keycodes
//		if (Input.GetKey(KeyCode.Escape))
//		{
//			currentPossession = "human";
//
//			_playerStats.KillPlayer(GetComponent<Player>());
//		}
//
//		if (Input.GetKeyUp(KeyCode.Alpha1) && !gameObject.name.Equals ("PrototypePlayer(Clone)") && !gameObject.name.Equals ("PrototypePlayer"))
//		{
//			prefab = Resources.Load("prefabs/PrototypePlayer", typeof(GameObject));
//			Debug.Log ("Pressed 1");
//
//			currentPossession = "human";
//			doPrefabChange = true;
//		}
//		else if (Input.GetKeyUp(KeyCode.Alpha2) && !gameObject.name.Equals ("PrototypeRabbitPlayer(Clone)"))
//		{
//			prefab = Resources.Load("prefabs/PrototypeRabbitPlayer", typeof(GameObject));
//			Debug.Log ("Pressed 2");
//
//			currentPossession = "rabbit";
//			doPrefabChange = true;
//		}
//		else if (Input.GetKeyUp(KeyCode.Alpha3) && !gameObject.name.Equals ("PrototypeCatPlayer(Clone)"))
//		{
//			prefab = Resources.Load("prefabs/PrototypeCatPlayer", typeof(GameObject));
//			Debug.Log ("Pressed 3");
//
//			currentPossession = "cat";
//			doPrefabChange = true;
//		}
//
//		if (doPrefabChange)
//		{
//			doSearchForPrefabs("BEFORE CHANGE");
//
//			Vector3 oldPos = this.transform.position;
//			GameObject thePlayer = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
//
//			thePlayer.transform.position = oldPos;
//			doPrefabChange = false;
//			
//			textValue[1].text = "Current Prefab: " + prefab.name;
//
//			PlayerStats.justLoaded = true;
//
//			Destroy(gameObject);
//		}
//	}
//
//	void onTriggerEnter2D(Collider2D other)
//	{
//		if (_playerStats.hasPlayerEnteredDeathzone(this.GetComponent<Collider2D>(), other))
//		{
//			_playerStats.KillPlayer(this.GetComponent<Player>());
//		}
//	}
//
//	#region Clean up
//	public void doSearchForPrefabs(string a)
//	{
//		search = GameObject.FindGameObjectsWithTag("Player");
//		
//		foreach (GameObject bob in search)
//		{
//			foundPres += bob.transform.name + "(" + bob.activeInHierarchy.ToString() + "), ";
//		}
//		
//		//Debug.Log ("** " + a + " " + foundPres);
//		
//		foundPres = "";
//		search = null;
//	}
//
//	void removeOldPrefabsFromMemory(string thisPrefabName)
//	{
//		search = GameObject.FindGameObjectsWithTag("Player");
//		
//		foreach (GameObject bob in search)
//		{
//			if (bob.gameObject.name != thisPrefabName)
//			{
//				Debug.Log("removeOldPrefabsFromMemory found rogue prefab: " + bob.gameObject.name);
//				Destroy(bob);
//			}
//		}
//	}
//	#endregion
}
