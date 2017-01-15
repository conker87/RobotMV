using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	/// TODO: Replace Health with int Orbs, replace Energy with Cooldowns for items and shooting.


	// System
	protected float nextTickTime = 0f, iFramesRemoveTime;

	public string EntityNameLocalisationID = "";

	[Header("Health")]
	public bool 	INFINITE_HEALTH = false;
	public int		Health = 3, HealthMaximum = 3;
	public bool  	HealthRegenOn = false;
	public float 	HealthRegenCooldown = 10f; 

	[SerializeField] bool dead = false;

	//[Header("Energy :: DEPRECATED")]
	//public bool INFINITE_ENERGY = false;
	//public float Energy = 50f, EnergyMaximum = 50f;
	//public float EnergyRegenPerTick = 5f;

	//[Range(0, 10)]
	//public int EnergyTanks = 0, EnergyTanksMax = 0;
	//public bool EnergyRegenOn = true;

	[Header("iFrames")]
	public bool  hasInvincibilityFrames = false;
	public float invincibilityFramesLength = 0f;
	public bool	 isCurrentlyInInvulnerabilityFrames = false;

	void DoHealth()	{

		return;

		// Regen
//		if (HealthRegenOn && Time.time > nextTickTime && !(HealthTanks == HealthTanksMax && Health > HealthMaximum)) {
//
//			Health += HealthRegenPerTick;
//
//		}
//
//		// Tanks
//		if (HealthTanks < HealthTanksMax	&&	Health > HealthMaximum)		{	HealthTanks++; Health = Health - HealthMaximum; }  
//		if (HealthTanks > 0 				&&	Health <= 0f)				{	HealthTanks--; Health = Health + HealthMaximum;	}
//
//		HealthTanks = Mathf.Clamp (HealthTanks, 0, HealthTanksMax);
//		if (HealthTanks == HealthTanksMax)	{	Health = Mathf.Clamp (Health, 0, HealthMaximum);	}

	}

	void DoEnergy() {

		// No longer using Energy.
		return;

		// Regen
//		if (EnergyRegenOn && Time.time > nextTickTime && !(EnergyTanks == EnergyTanksMax && Energy >= EnergyMaximum)) {
//
//			Energy += EnergyRegenPerTick;
//
//		}
//
//		if (EnergyTanks < EnergyTanksMax	&&	Energy > EnergyMaximum)		{	EnergyTanks++; Energy = Energy - EnergyMaximum; }  
//		if (EnergyTanks > 0 				&&	Energy <= 0f)				{	EnergyTanks--; Energy = Energy + EnergyMaximum;	}
//
//		EnergyTanks = Mathf.Clamp (EnergyTanks, 0, EnergyTanksMax);
//		if (EnergyTanks == EnergyTanksMax)	{	Energy = Mathf.Clamp (Energy, 0, EnergyMaximum);	}

	}

	[Header("Movement")]
	public float MoveSpeed = 6;
	public float MaximumMoveSpeed = 6;

	public virtual void Update() {

		if (!dead && Health < 1) {
			
			dead = true;
			Debug.Log (this + " has hit 0 health and has been removed. If it was an Enemy then it should spawn Health and Energy pickups.");

			DoDeath ();

		}

		DoHealth ();
		DoEnergy ();

		if (Time.time > nextTickTime) {

			nextTickTime = Time.time + Constants.ResourceTick;

		}

		if (hasInvincibilityFrames && isCurrentlyInInvulnerabilityFrames) {

			if (Time.time > iFramesRemoveTime) {

				isCurrentlyInInvulnerabilityFrames = false;

			}

		}

	}

	public virtual void DoDeath() {

		Destroy (gameObject);

	}

	public virtual void RestoreHealth(int restore) {

		Health += restore;

	}

	public void RestoreHealthFully() {

		Health = HealthMaximum;

	}

	public virtual void DamageHealth(int damage) {

		if (INFINITE_HEALTH) {

			return;

		}

		if (isCurrentlyInInvulnerabilityFrames) {

			return;

		}

		if (hasInvincibilityFrames) {

			isCurrentlyInInvulnerabilityFrames = true;

			iFramesRemoveTime = Time.time + invincibilityFramesLength;

		}

		Health -= damage;

	}

}
