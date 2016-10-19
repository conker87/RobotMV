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

	public bool EnergyRegenOn = false;

	[Header("iFrames")]
	public bool  hasInvincibilityFrames = false;
	public float invincibilityFramesLength = 0f;
	public bool	 isCurrentlyInInvulnerabilityFrames = false;

	void HealthRegen()	{	Health += HealthRegenPerTick;						}
	void HealthClamp()	{	Health = Mathf.Clamp (Health, 0, HealthMaximum);	}

	void EnergyRegen()	{	Energy += EnergyRegenPerTick;						}
	void EnergyClamp()	{	Energy = Mathf.Clamp (Energy, 0, EnergyMaximum);	}

	[Header("Movement")]
	public float MoveSpeed = 6;
	public float MaximumMoveSpeed = 6;

	void Update() {

		if (!dead && Health == 0) {
			
			dead = true;
			Debug.Log (this + " has hit 0 health and should be removed, if this is the player it should be removed cleanly.");

			return;

		}

		if (Time.time > nextTickTime) {

			if (HealthRegenOn)	{	HealthRegen ();	}
			if (EnergyRegenOn)	{	EnergyRegen ();	}

			nextTickTime = Time.time + resourceTick;

		}

		HealthClamp ();
		EnergyClamp ();

		if (hasInvincibilityFrames && isCurrentlyInInvulnerabilityFrames) {

			if (Time.time > iFramesRemoveTime) {

				isCurrentlyInInvulnerabilityFrames = false;

			}

		}

	}

	public virtual void DamageHealth(float damage) {

		Health -= damage;

		//OverlayCanvasController.instance.ShowCombatText(gameObject, CombatTextType.Hit, damage.ToString());

	}

}
