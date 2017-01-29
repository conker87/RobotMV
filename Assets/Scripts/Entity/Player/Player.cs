using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class Player : Entity
{
	GUIStyle style;
	public static string ErrorMessage = "";

	[Header("Weapons")]
	public bool Weapon_Basic_Blaster = false;
	public bool Upgrade_Basic_Blaster_Charged_Shot = false;

	[Header("Items")]

	[Header("PowerUps")]
	public bool PowerUp_Jump = false;
	public bool PowerUp_Double_Jump = false;
	public bool PowerUp_Triple_Jump = false;

	public static Player Current { get; protected set; }

	float timeToNextBomb, timeToNextMegaBomb;

	[Header("Bombs Regen")]
	public float	BombsRegenCooldown = 1f;
	public bool		doBombsRegen = true;

	public float	BombsMegaRegenCooldown = 60f;
	public bool		doBombsMegaRegen = true;

	public bool CanChangeWeapon = true;

	protected override void Awake() {

		base.Awake ();

		// TODO: These should be loaded from the Save File.
//		CollectablesD.Clear ();
//		CollectablesInfoD.Clear ();
//		CollectablesD.Add ("BASIC_BLASTER",					false);
//		CollectablesD.Add ("BASIC_BLASTER_CHARGED_SHOT",	false);
//
//		CollectablesInfo defaultInfo = new CollectablesInfo (1f, 1f, 1f);
//
//		CollectablesInfoD.Add ("BASIC_BLASTER",					defaultInfo); 
//		CollectablesInfoD.Add ("BASIC_BLASTER_CHARGED_SHOT",	defaultInfo); 
//		CollectablesInfoD.Add ("BLACK_HOLE_BURST",				defaultInfo); 
//		CollectablesInfoD.Add ("LASER",							defaultInfo); 
//		CollectablesInfoD.Add ("MISSILE_LAUNCHER",				defaultInfo); 
//		CollectablesInfoD.Add ("SPINNER",						defaultInfo); 
//		CollectablesInfoD.Add ("SPLITTER",						defaultInfo); 
//		CollectablesInfoD.Add ("GLOBAL",						defaultInfo); 
//
//		CollectablesD.Add ("BLACK_HOLE_BURST",				false);
//		CollectablesD.Add ("LASER",							false);
//		CollectablesD.Add ("MISSILE_LAUNCHER",				false);
//		CollectablesD.Add ("SPINNER",						false);
//		CollectablesD.Add ("SPLITTER",						false);
//
//		CollectablesD.Add ("MAGNET",						false);
//
//		CollectablesD.Add ("JUMP",							false);
//		CollectablesD.Add ("JUMP_DOUBLE",					false);
//		CollectablesD.Add ("JUMP_TRIPLE",					false);
//
//		CollectablesD.Add ("BOMBS",							false);
//		CollectablesD.Add ("BOMBS_MEGA",					false);
//
//		CollectablesD.Add ("SHURIKEN_SHIELD",				false);
//		CollectablesD.Add ("ENERGY_SHIELD",					false);
//
//		BombsD.Clear ();
//		BombsD.Add ("BOMBS_CURRENT",						0);
//		BombsD.Add ("BOMBS_MEGA_CURRENT",					0);
//		BombsD.Add ("BOMBS_MAX",							3);
//		BombsD.Add ("BOMBS_MEGA_MAX",						1);
//
//		CheatsD.Clear ();
//		CheatsD.Add ("INFINITE_HEALTH",						false);
//		CheatsD.Add ("INFINITE_SHURIKEN_SHIELD",			false);
//		CheatsD.Add ("INFINITE_ENERGY_SHIELD",				false);
//		CheatsD.Add ("INFINITE_BOMBS",						false);
//		CheatsD.Add ("INFINITE_BOMBS_MEGA",					false);

	}

	void Start() {

		Current = this;

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
			
			DamageHealth (e.DamageOnTouch);

		}

	}

	void OnGUI()
	{
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.magenta;

		GUI.Label(new Rect(10, 10, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 30, 500, 20), "H: " + VitalsD["HEALTH"] + "/" + VitalsD["HEALTH_MAX"] + "|" + Health_RegenOn + ")", style);
		GUI.Label(new Rect(10, 50, 500, 20), "Jumps: " + PowerUp_Jump + "|" + PowerUp_Double_Jump + "|" + PowerUp_Triple_Jump, style);
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
public struct CollectablesInfo {

	public CollectablesInfo(float attackLengthMultiplier, float cooldownMultiplier, float damageMultiplier) {

		this.AttackLengthMultiplier = attackLengthMultiplier;
		this.CooldownMultiplier = cooldownMultiplier;
		this.DamageMultiplier = damageMultiplier;

	}

	public float AttackLengthMultiplier;
	public float CooldownMultiplier;
	public float DamageMultiplier;

}

[System.Serializable]
public struct Cheats {

	public string CheatID;
	public bool isCheatOn;

}