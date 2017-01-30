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
	public bool 	Weapon_BasicBlaster = false;
	public float 	// Weapon_BasicBlaster_AttackLengthMod = 1f,
					Weapon_BasicBlaster_CooldownMod = 1f,
					Weapon_BasicBlaster_DamageMod = 1f;

	public bool 	Weapon_BasicBlaster_ChargedShot = false;
	public float 	// Weapon_BasicBlaster_ChargedShot_AttackLengthMod = 1f,
					// Weapon_BasicBlaster_ChargedShot_CooldownMod = 1f,
					Weapon_BasicBlaster_ChargedShot_DamageMod = 1f;

	public bool 	Weapon_BlackHoleBurst = false;
	public float 	Weapon_BlackHoleBurst_AttackLengthMod = 1f,
					Weapon_BlackHoleBurst_CooldownMod = 1f,
					Weapon_BlackHoleBurst_DamageMod = 1f;

	public bool 	Weapon_Laser = false;
	public float 	Weapon_Laser_AttackLengthMod = 1f,
					Weapon_Laser_CooldownMod = 1f,
					Weapon_Laser_DamageMod = 1f;

	public bool	 	Weapon_MissileLauncher = false;
	public float 	// Weapon_MissileLauncher_AttackLengthMod = 1f,
					Weapon_MissileLauncher_CooldownMod = 1f,
					Weapon_MissileLauncher_DamageMod = 1f;

	public bool 	Weapon_Spinner = false;
	public float 	Weapon_Spinner_AttackLengthMod = 1f,
					Weapon_Spinner_CooldownMod = 1f,
					Weapon_Spinner_DamageMod = 1f;

	public bool 	Weapon_Splitter = false;
	public float 	// Weapon_Splitter_AttackLengthMod = 1f,
					Weapon_Splitter_CooldownMod = 1f,
					Weapon_Splitter_DamageMod = 1f;

	[Header("Items")]
	public bool		Item_Magnet = false;

	public bool 	Item_ShurikenShield = false;
	public float 	Item_ShurikenShield_AttackLengthMod = 1f,
					Item_ShurikenShield_CooldownMod = 1f,
					Item_ShurikenShield_DamageMod = 1f;
	public bool SHURIKEN_SHIELD_INFINITE = false;

	public bool 	Item_EnergyShield = false;
	public float 	Item_EnergyShield_AttackLengthMod = 1f,
					Item_EnergyShield_CooldownMod = 1f,
					Item_EnergyShield_DamageMod = 1f;
	public bool ENERGY_SHIELD_INFINITE = false;

	[Header("PowerUps")]
	public bool PowerUp_Jump = false;
	public bool PowerUp_Jump_Double = false;
	public bool PowerUp_Jump_Triple = false;

	public static Player Current { get; protected set; }

	[Header("Bombs")]
	public int		Bombs_Current = 0;
	public int 		Bombs_Max = 0;
	public bool 	BOMBS_INFINITE = false;

	public int 		Bombs_Mega_Current = 0, Bombs_Mega_Max = 0;
	public bool 	BOMBS_MEGA_INFINITE = false;

	float timeToNextBomb, timeToNextMegaBomb;

	public bool CanChangeWeapon = true;

	void Start() {

		Current = this;

	}

	public override void Update() {

		base.Update();

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
		GUI.Label(new Rect(10, 30, 500, 20), "H: " + Health_Current + "/" + Health_Max + "|" + Health_RegenOn + ")", style);
		GUI.Label(new Rect(10, 50, 500, 20), "Jumps: " + PowerUp_Jump + "|" + PowerUp_Jump_Double + "|" + PowerUp_Jump_Triple, style);
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