using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class Player : Entity {
	GUIStyle style;
	public static string ErrorMessage = "";

	[Header("Player Cheats")]
	public bool 	CHEATS = false;
	public bool 	CHEAT_BOMBS_INFINITE = false, CHEAT_BOMBS_NO_COOLDOWN = false;
	public bool 	CHEAT_ENERGY_SHIELD_INFINITE_HEALTH = false, CHEAT_ENERGY_SHIELD_NO_COOLDOWN = false, CHEAT_ENERGY_SHIELD_NO_DURATION_LIMIT = false;
	public bool 	CHEAT_BOMBS_MEGA_INFINITE = false, CHEAT_BOMBS_MEGA_NO_COOLDOWN = false;


	[Header("Weapons")]
	[Header("Basic Blaster")]
	public bool 	WeaponBasicBlaster = false;
	public float 	WeaponBasicBlasterCooldownMod = 1f,
					WeaponBasicBlasterDamageMod = 1f;

	public bool 	WeaponBasicBlasterChargedShot = false;
	public float 	
					WeaponBasicBlasterChargedShotDamageMod = 1f;

	[Header("Black Hole Burst")]
	public bool 	WeaponBlackHoleBurst = false;
	public float 	WeaponBlackHoleBurstAttackLengthMod = 1f,
					WeaponBlackHoleBurstCooldownMod = 1f,
					WeaponBlackHoleBurstDamageMod = 1f;

	[Header("LASER")]
	public bool 	WeaponLaser = false;
	public float 	WeaponLaserAttackLengthMod = 1f,
					WeaponLaserCooldownMod = 1f,
					WeaponLaserDamageMod = 1f;

	[Header("Missile Launcher")]
	public bool	 	WeaponMissileLauncher = false;
	public float 	// Weapon_MissileLauncher_AttackLengthMod = 1f,
					WeaponMissileLauncherCooldownMod = 1f,
					WeaponMissileLauncherDamageMod = 1f;

	[Header("Phazer Beam")]
	// TODO: Needs a PowerUp prefab
	public bool 	WeaponPhazerBeam = false;
	public float 	WeaponPhazerBeamCooldownMod = 1f,
					WeaponPhazerBeamDamageMod = 1f;

	[Header("Spinner")]
	public bool 	WeaponSpinner = false;
	public float 	WeaponSpinnerAttackLengthMod = 1f,
					WeaponSpinnerCooldownMod = 1f,
					WeaponSpinnerDamageMod = 1f;

	[Header("Splitter")]
	public bool 	WeaponSplitter = false;
	public float 	WeaponSplitterCooldownMod = 1f,
					WeaponSplitterDamageMod = 1f;

	[Header("Items")]
	public bool		ItemMagnet = false;

	public bool 	ItemEnergyShield = false;
	public float 	ItemEnergyShieldAttackLengthMod = 1f,
					ItemEnergyShieldCooldownMod = 1f,
					ItemEnergyShieldDamageMod = 1f;

	public int 		Keys = 0;

	[Header("Jump")]
	public bool		PowerUpJump = false;
	public bool		PowerUpJumpDouble = false;
	public bool 	PowerUpJumpTriple = false;

	public static Player Current { get; protected set; }

	[Header("Bombs")]
	public int		BombsCurrent = 0;
	public int 		BombsMax = 0;
	public float 	ItemBombAttackLengthMod = 1f,
					ItemBombCooldownMod = 1f,
					ItemBombDamageMod = 1f;

	[Header("Mega Bombs")]
	public int 		BombsMegaCurrent = 0;
	public int 		BombsMegaMax = 0;
	public float 	ItemBombMegaAttackLengthMod = 1f,
					ItemBombMegaCooldownMod = 1f,
					ItemBombMegaDamageMod = 1f;

	float timeToNextBomb, timeToNextMegaBomb;

	public bool CanChangeWeapon = true;

	void Start() {

		Current = this;

	}
		
	void OnTriggerEnter2D(Collider2D other) {

		ProjectileBase p;

		if ((p = other.GetComponentInParent<ProjectileBase> ()) != null && p.ProjectileType == ProjectileType.ENEMY) {

			DamageHealth (p.ProjectileDamage);

		}

	}

	void OnGUI() {
		style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.magenta;

		GUI.Label(new Rect(10, 10, 500, 20), ErrorMessage, style);
		GUI.Label(new Rect(10, 30, 500, 20), "H: " + HealthCurrent + "/" + HealthMax + "|" + HealthRegen + ")", style);
		GUI.Label(new Rect(10, 50, 500, 20), "CW/I: " + (CurrentWeapon == null ? "None" : CurrentWeapon.UsableNameLocalisationID) + "|" + (CurrentItem == null ? "None" : CurrentItem.UsableNameLocalisationID), style);

	}

}

//	public bool 	ItemShurikenShield = false;
//	public float 	ItemShurikenShieldAttackLengthMod = 1f,
//					ItemShurikenShieldCooldownMod = 1f,
//					ItemShurikenShieldDamageMod = 1f;
//	public bool 	CHEAT_SHURIKEN_SHIELD_NO_COOLDOWN = false, CHEAT_SHURIKEN_SHIELD_NO_DURATION_LIMIT = false;