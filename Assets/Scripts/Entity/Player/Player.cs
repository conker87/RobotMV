using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : Entity
{
	GUIStyle style;
	public static string ErrorMessage = "";
	[SerializeField]
	public Dictionary<string, bool> CollectablesD = new Dictionary<string, bool> ();
	public Dictionary<string, int> BombsD = new Dictionary<string, int> ();

	// TODO: Move this to the GameManager once we sort it out.
	public Dictionary<string, bool> CheatsD = new Dictionary<string, bool> ();

	// TODO: This class is the area where the abilities for the Player is stored. It is then saved to the save file via IO.
		// Get/Set or direct changing? Meh.

	public static Player Current { get; protected set; }

	float timeToNextBomb, timeToNextMegaBomb;

	public InputManager inputManager;

	public bool 	INFINITE_HEALTH = false;

	// [Header("Jumping")]
	// public bool 	Jump = false;
	// public bool 	DoubleJump = false, TripleJump = false;

	public float	BombsRegenCooldown = 1f;
	public bool		doBombsRegen = true;

	public float	BombsMegaRegenCooldown = 60f;
	public bool		doBombsMegaRegen = true;

	/* [Header("Items")]
	[Header("Weapons")]
	public bool 	BasicBlaster = false;
	public bool 	BasicBlasterChargeShot = false;
	public bool 	Spinner = false;
	public bool 	ClusterSpreader = false;
	public bool 	MissileLauncher = false;
	public bool 	Laser = false;
	public bool		Bombs = false;
	public bool		MegaBombs = false;
	public bool		Magnet = false;

	[Header("Shields")]
	public bool 	ShurikenShield = false;
	public bool 	EnergyShield = false;

	[Header("Bombs")]
	public bool 	INFINITE_BOMBS = false;
	public int		BombsCount = 0, BombsMaximum = 0;
	public float	BombsRegenCooldown = 1f;
	public bool		doBombsRegen = true;
	public bool 	INFINITE_MEGABOMBS = false;
	public int 		MegaBombsCount = 0, MegaBombsMaximum = 0;
	public float	BombsMegaRegenCooldown = 60f;
	public bool		doBombsMegaRegen = true; */

	[Header("Currently Equipped Items")]
	public Item CurrentItem = null;
	public Weapon CurrentWeapon = null;

	public bool CanChangeWeapon = true;

	protected override void Awake() {

		base.Awake ();

		// TODO: These should be loaded from the Save File.

		CollectablesD.Clear ();
		CollectablesD.Add ("BASIC_BLASTER",					false);
		CollectablesD.Add ("BASIC_BLASTER_CHARGED_SHOT",	false);
		CollectablesD.Add ("SPINNER",						false);
		CollectablesD.Add ("CLUSTER_SPREADER",				false);
		CollectablesD.Add ("MISSILE_LAUNCHER",				false);
		CollectablesD.Add ("LASER",							false);

		CollectablesD.Add ("MAGNET",		false);

		CollectablesD.Add ("JUMP",			false);
		CollectablesD.Add ("JUMP_DOUBLE",	false);
		CollectablesD.Add ("JUMP_TRIPLE",	false);

		CollectablesD.Add ("BOMBS",							false);
		CollectablesD.Add ("BOMBS_MEGA",					false);

		CollectablesD.Add ("SHURIKEN_SHIELD",	false);
		CollectablesD.Add ("ENERGY_SHIELD",		false);

		BombsD.Clear ();
		BombsD.Add ("BOMBS_CURRENT",		0);
		BombsD.Add ("BOMBS_MEGA_CURRENT",	0);
		BombsD.Add ("BOMBS_MAX",			3);
		BombsD.Add ("BOMBS_MEGA_MAX",		1);

		CheatsD.Clear ();
		CheatsD.Add ("INFINITE_HEALTH",				false);
		CheatsD.Add ("INFINITE_SHURIKEN_SHIELD",	false);
		CheatsD.Add ("INFINITE_ENERGY_SHIELD",		false);
		CheatsD.Add ("INFINITE_BOMBS",				false);
		CheatsD.Add ("INFINITE_BOMBS_MEGA",			false);

	}

	void Start() {

		Current = this;
		inputManager = GameObject.FindObjectOfType<InputManager>();

	}

	public void DamageVitalPlayer(string ID, int damage, string CHEAT_ID = null) {

		if (CHEAT_ID != null && CheatsD.ContainsKey(CHEAT_ID) && CheatsD [CHEAT_ID]) {

			return;

		}

		base.DamageVital (ID, damage);

	}

	public override void Update() {

		base.Update();

//		if (Bombs) {
//			// DoBombsRegen ();
//			DoBombsClamp ();
//		}
//
//		if (MegaBombs) {
//			// DoMegaBombsRegen ();
//			DoMegaBombsClamp ();
//		}

	}
		
	void DoBombsClamp() {
		
		// BombsCount = Mathf.Clamp (BombsCount, 0, BombsMaximum);

	}

	void DoBombsRegen() {

//		if (!doBombsRegen || BombsCount == BombsMaximum) {
//
//			return;
//
//		}
//
//		if (doBombsRegen) {
//
//			timeToNextBomb = Time.time + 1f;
//
//		}
//
//		if (Time.time > timeToNextBomb) {
//
//			timeToNextBomb = Time.time + BombsRegenCooldown;
//			BombsCount++;
//
//		}

	}
		
	void DoMegaBombsClamp() {
		
		// MegaBombsCount = Mathf.Clamp (MegaBombsCount, 0, MegaBombsMaximum);

	}

	void DoMegaBombsRegen() {

//		if (!doBombsMegaRegen || MegaBombsCount == MegaBombsMaximum) {
//
//			return;
//
//		}
//
//		if (doBombsMegaRegen) {
//
//			timeToNextMegaBomb = Time.time + BombsMegaRegenCooldown;
//
//		}
//
//		if (Time.time > timeToNextMegaBomb) {
//
//			timeToNextMegaBomb = Time.time + BombsMegaRegenCooldown;
//			MegaBombsCount++;
//
//		}

	}
		
	void OnTriggerStay2D(Collider2D col) {

		Enemy e;

		if ((e = col.gameObject.GetComponentInParent<Enemy> ()) != null) {
			
			DamageVitalPlayer ("HEALTH", e.DamageOnTouch);

		}

	}

	void OnGUI()
	{
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.magenta;

		GUI.Label(new Rect(10, 10, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 30, 500, 20), "H: " + Health + "/" + HealthMaximum + "|" + HealthRegenOn + ")", style);
		GUI.Label(new Rect(10, 50, 500, 20), "Jumps: " + CollectablesD["JUMP"] + "|" + CollectablesD["JUMP_DOUBLE"] + "|" + CollectablesD["JUMP_TRIPLE"], style);
		GUI.Label(new Rect(10, 70, 500, 20), "CW/I: " + (CurrentWeapon == null ? "None" : CurrentWeapon.UsableNameLocalisationID) + "|" + (CurrentItem == null ? "None" : CurrentItem.UsableNameLocalisationID), style);
		GUI.Label(new Rect(10, 90, 500, 20), "Speed: " + MoveSpeed, style);
	}
}

[System.Serializable]
public struct Collectables {
	
	public string ItemID;
	public bool Collected;

}

[System.Serializable]
public struct Cheats {

	public string CheatID;
	public bool isCheatOn;

}