using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	// System
	protected float iFramesRemoveTime, nextTickTime;

	public string EntityNameLocalisationID = "";

	[Header("Health")]
	public Dictionary<string, int> VitalsD = new Dictionary<string, int> ();

	public int		Health = 3, HealthMaximum = 3;
	public bool  	HealthRegenOn = false;
	public float 	HealthRegenCooldown = 10f; 

	[SerializeField] bool dead = false;

	[Header("iFrames")]
	public bool  hasInvincibilityFrames = false;
	public float invincibilityFramesLength = 0f;
	public bool	 isCurrentlyInInvulnerabilityFrames = false;

	void DoHealth()	{

		return;

		if (HealthRegenOn && !isCurrentlyInInvulnerabilityFrames && Health != HealthMaximum && Time.time > nextTickTime) {

			//EntityVitals["HEALTH++;

		}
	}

	[Header("Movement")]
	public float MoveSpeed = 6;
	public float MaximumMoveSpeed = 6;

	protected virtual void Awake() {

		VitalsD.Clear ();

		VitalsD.Add ("HEALTH",		3);
		VitalsD.Add ("HEALTH_MAX",	3);

	}

	public virtual void Update() {

		if (!dead && Health < 1) {
			
			dead = true;
			Debug.Log (this + " has hit 0 health and has been removed. If it was an Enemy then it should spawn Health and Energy pickups.");

			DoDeath ();

		}

		DoHealth ();

		if (Time.time > nextTickTime) {

			// TODO: This constant value needs to be changed depending on the current difficulty setting.
			nextTickTime = Time.time + 10f;

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

	public virtual void RestoreVital(string ID, int restore) {

		VitalsD [ID] += restore;

	}

	public virtual void RestoreVitalFully(string ID, string MaxID) {

		VitalsD [ID] = VitalsD[MaxID];

	}

	public virtual void DamageVital(string ID, int damage) {

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
	
[System.Serializable]
public struct EntityVitals {

	public EntityVitals(string ID, int val) {

		this.VitalsID = ID;
		this.Value = val;

	}

	public string VitalsID;
	public int Value;

}