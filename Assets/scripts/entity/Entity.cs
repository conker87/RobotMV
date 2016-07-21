using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	// System
	public List<string> layers = new List<string>();
	protected float tick = .5f, nextTickTime, iFramesRemoveTime;		// Shouldn't tick be global?

	public float invincibilityFramesLength = 0f;

	bool dead = false;

	// Health & Energy
	public float Health = 100f,		HealthMaximum = 100f,		HealthRegenPerTick = 1f;
	public float Energy = 50f,		EnergyMaximum = 50f,		EnergyRegenPerTick = 5f;
	public bool	 isCurrentlyInInvulnerabilityFrames = false;

	void HealthRegen()	{	Health += HealthRegenPerTick;						}
	void HealthClamp()	{	Health = Mathf.Clamp (Health, 0, HealthMaximum);	}

	void EnergyRegen()	{	Energy += EnergyRegenPerTick;						}
	void EnergyClamp()	{	Energy = Mathf.Clamp (Energy, 0, EnergyMaximum);	}

	public bool _DEBUG_INFINITE_ENERGY = false, _DEBUG_INFINITE_HEALTH = false;

	public bool HealthRegenOn = false, EnergyRegenOn = true;

	// Movement
	public float MoveSpeed = 6, MaximumMoveSpeed = 6;

	void Update() {

		if (!dead && Health == 0) {
			dead = true;
			Debug.Log (this + " has hit 0 health and should be removed, if this is the player it should be removed cleanly.");
		}

		if (Time.time > nextTickTime) {

			if (HealthRegenOn)	{	HealthRegen ();	}
			if (EnergyRegenOn)	{	EnergyRegen ();	}

			nextTickTime = Time.time + tick;

		}

		HealthClamp ();
		EnergyClamp ();

		if (isCurrentlyInInvulnerabilityFrames) {

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
