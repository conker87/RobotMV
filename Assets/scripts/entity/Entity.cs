using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	// System
	protected float nextTickTime = 0f, iFramesRemoveTime;

	public string EntityName = "";

	[Header("Health")]
	public bool _DEBUG_INFINITE_HEALTH = false;
	public float Health = 100f, HealthMaximum = 100f;
	public float HealthRegenPerTick = 1f;

	[Range(0, 10)]
	public int HealthTanks = 0, HealthTanksMax = 0;
	public bool HealthRegenOn = false;

	[SerializeField] bool dead = false;

	[Header("Energy")]
	public bool _DEBUG_INFINITE_ENERGY = false;
	public float Energy = 50f, EnergyMaximum = 50f;
	public float EnergyRegenPerTick = 5f;

	[Range(0, 10)]
	public int EnergyTanks = 0, EnergyTanksMax = 0;
	public bool EnergyRegenOn = true;

	[Header("iFrames")]
	public bool  hasInvincibilityFrames = false;
	public float invincibilityFramesLength = 0f;
	public bool	 isCurrentlyInInvulnerabilityFrames = false;

	void DoHealth()	{

		// Regen
		if (HealthRegenOn && Time.time > nextTickTime && !(HealthTanks == HealthTanksMax && Health > HealthMaximum)) {

			Health += HealthRegenPerTick;

		}

		// Tanks
		if (HealthTanks < HealthTanksMax	&&	Health > HealthMaximum)		{	HealthTanks++; Health = Health - HealthMaximum; }  
		if (HealthTanks > 0 				&&	Health <= 0f)				{	HealthTanks--; Health = Health + HealthMaximum;	}

		HealthTanks = Mathf.Clamp (HealthTanks, 0, HealthTanksMax);
		if (HealthTanks == HealthTanksMax)	{	Health = Mathf.Clamp (Health, 0, HealthMaximum);	}

	}

	void DoEnergy() {
		
		if (EnergyRegenOn && Time.time > nextTickTime && !(EnergyTanks == EnergyTanksMax && Energy >= EnergyMaximum)) {

			Energy += EnergyRegenPerTick;

		}

		if (EnergyTanks < EnergyTanksMax	&&	Energy > EnergyMaximum)		{	EnergyTanks++; Energy = Energy - EnergyMaximum; }  
		if (EnergyTanks > 0 				&&	Energy <= 0f)				{	EnergyTanks--; Energy = Energy + EnergyMaximum;	}

		EnergyTanks = Mathf.Clamp (EnergyTanks, 0, EnergyTanksMax);
		if (EnergyTanks == EnergyTanksMax)	{	Energy = Mathf.Clamp (Energy, 0, EnergyMaximum);	}

	}

	[Header("Movement")]
	public float MoveSpeed = 6;
	public float MaximumMoveSpeed = 6;

	public virtual void Update() {

		if (!dead && (HealthTanks == 0 && Health == 0f)) {
			
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

	public virtual void RestoreHealth(float restore) {

		Health += restore;

	}

	public void RestoreHealthFully() {

		Health = HealthMaximum;
		HealthTanks = HealthTanksMax;

	}

	public virtual void DamageHealth(float damage) {

		if (_DEBUG_INFINITE_HEALTH) {

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

	public virtual void RestoreEnergy(float restore) {

		Energy += restore;

	}

	public virtual void DamageEnergy(float damage)
	{

		if (_DEBUG_INFINITE_ENERGY) {

			return;

		}

		Energy -= damage;

	}

	public void RestoreEnergyFully() {

		Energy = EnergyMaximum;
		EnergyTanks = EnergyTanksMax;

	}

}
