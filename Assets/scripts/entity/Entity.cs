using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	// System
	protected float tick = .5f, nextTickTime;

	// Health & Energy
	public float Health = 100f,		HealthMaximum = 100f,		HealthRegenPerTick = 1f;
	public float Energy = 50f,		EnergyMaximum = 50f,		EnergyRegenPerTick = 5f;

	void HealthRegen()	{	Health += HealthRegenPerTick;						}
	void HealthClamp()	{	Health = Mathf.Clamp (Health, 0, HealthMaximum);	}

	void EnergyRegen()	{	Energy += EnergyRegenPerTick;						}
	void EnergyClamp()	{	Energy = Mathf.Clamp (Energy, 0, EnergyMaximum);	}

	public bool _DEBUG_INFINITE_ENERGY = false, _DEBUG_INFINITE_HEALTH = false;

	public bool HealthRegenOn = false, EnergyRegenOn = true;

	// Movement
	public float MoveSpeed = 6, MaximumMoveSpeed = 6;

	void Update() {

		if (Time.time > nextTickTime) {

			if (HealthRegenOn)	{	HealthRegen ();	}
			if (EnergyRegenOn)	{	EnergyRegen ();	}

			nextTickTime = Time.time + tick;

		}

		HealthClamp ();
		EnergyClamp ();

	}

}
