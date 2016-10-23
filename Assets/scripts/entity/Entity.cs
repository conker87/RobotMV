using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	// System
	protected float tick = .5f, resourceTick = 0.05f, nextTickTime = 0f, iFramesRemoveTime;		// Shouldn't tick be global?

	public string EntityName = "";

	[Header("Health")]
	public bool _DEBUG_INFINITE_HEALTH = false;
	public float Health = 100f;
	public float HealthMaximum = 100f,	HealthRegenPerTick = 1f;
	public int HealthTanks = 1, HealthTanksMax = 1;

	public bool HealthRegenOn = false;

	[SerializeField] bool dead = false;

	[Header("Energy")]
	public bool _DEBUG_INFINITE_ENERGY = false;
	public float Energy = 50f;
	public float EnergyMaximum = 50f,		EnergyRegenPerTick = 5f;

	public int EnergyTanks = 1, EnergyTanksMax = 1;

	public bool EnergyRegenOn = true;

	[Header("iFrames")]
	public bool  hasInvincibilityFrames = false;
	public float invincibilityFramesLength = 0f;
	public bool	 isCurrentlyInInvulnerabilityFrames = false;

	void DoHealthRegen()	{	Health += HealthRegenPerTick;						}
	void DoHealthClamp()	{	Health = Mathf.Clamp (Health, 0, HealthMaximum);	}

	void DoEnergyRegen()	{	Energy += EnergyRegenPerTick;						}
	void DoEnergyClamp()	{	Energy = Mathf.Clamp (Energy, 0, EnergyMaximum);	}

	[Header("Movement")]
	public float MoveSpeed = 6;
	public float MaximumMoveSpeed = 6;

	public virtual void Update() {

		if (!dead && Health == 0) {
			
			dead = true;
			Debug.Log (this + " has hit 0 health and has been removed. If it was an Enemy then it should spawn Health and Energy pickups.");

			Destroy (gameObject);

			return;

		}

		if (Time.time > nextTickTime) {

			if (HealthRegenOn)	{	DoHealthRegen ();	}
			if (EnergyRegenOn)	{	DoEnergyRegen ();	}

			nextTickTime = Time.time + resourceTick;

		}

		DoHealthClamp ();
		DoEnergyClamp ();

		if (hasInvincibilityFrames && isCurrentlyInInvulnerabilityFrames) {

			if (Time.time > iFramesRemoveTime) {

				isCurrentlyInInvulnerabilityFrames = false;

			}

		}

	}

	public virtual void RestoreHealth(float restore) {

		Health += restore;

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

	public virtual void DamageEnergy(float damage) {

		if (_DEBUG_INFINITE_ENERGY) {

			return;

		}

		Energy -= damage;

	}

}
