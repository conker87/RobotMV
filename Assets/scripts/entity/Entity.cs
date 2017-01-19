using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	// System
	protected float iFramesRemoveTime, nextTickTime;

	public string EntityNameLocalisationID = "Localisation <FIXME>";
	public string EntityDescLocalisationID = "Localisation <FIXME>";

	public Dictionary<string, int> VitalsD = new Dictionary<string, int> ();

	[Header("Health")]
	public bool  	HealthRegenOn = false;
	public float 	HealthRegenCooldown = 10f; 

	[SerializeField] bool dead = false;

	[Header("iFrames")]
	public bool  hasInvincibilityFrames = false;
	public float invincibilityFramesLength = 0f;
	public bool	 isCurrentlyInInvulnerabilityFrames = false;

	void DoHealth()	{

		return;

		if (HealthRegenOn && !isCurrentlyInInvulnerabilityFrames && VitalsD["HEALTH"] != VitalsD["HEALTH_MAX"] && Time.time > nextTickTime) {

			VitalsD ["HEALTH"]++;

		}

	}

	[Header("Movement")]
	public float MoveSpeed = 3f;
	public float MaximumMoveSpeed = 3f;

	[Header("Currently Equipped Items")]
	public Item CurrentItem = null;
	public Weapon CurrentWeapon = null;

	protected virtual void Awake() {

		VitalsD.Clear ();

		VitalsD.Add ("HEALTH",		3);
		VitalsD.Add ("HEALTH_MAX",	3);

	}

	public virtual void Update() {

		if (!dead && VitalsD["HEALTH"] < 1) {
			
			dead = true;
			Debug.Log (this + " has hit 0 health and has been removed. If it was an Enemy then it should spawn Health and Energy pickups.");

			DoDeath ();

		}

		DoHealth ();

		if (MoveSpeed > MaximumMoveSpeed) {

			MoveSpeed = MaximumMoveSpeed;

		}

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

		VitalsD["HEALTH"] -= damage;

		if (hasInvincibilityFrames) {

			isCurrentlyInInvulnerabilityFrames = true;

			iFramesRemoveTime = Time.time + invincibilityFramesLength;

		}

	}

}