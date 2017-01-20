﻿using UnityEngine;
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
	public bool  hasInvincibilityTime = false;
	public float invincibilityTimeLength = 0f;
	public bool	 isCurrentlyInInvulnerabilityTime = false;

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

		if (MoveSpeed > MaximumMoveSpeed) {

			MoveSpeed = MaximumMoveSpeed;

		}

		if (Time.time > nextTickTime) {

			// TODO: This constant value needs to be changed depending on the current difficulty setting.
			nextTickTime = Time.time + 10f;

		}

		if (hasInvincibilityTime && isCurrentlyInInvulnerabilityTime) {

			if (Time.time > iFramesRemoveTime) {

				isCurrentlyInInvulnerabilityTime = false;

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

		if (isCurrentlyInInvulnerabilityTime) {

			return;

		}

		VitalsD["HEALTH"] -= damage;

		if (hasInvincibilityTime) {

			isCurrentlyInInvulnerabilityTime = true;

			iFramesRemoveTime = Time.time + invincibilityTimeLength;

		}

	}

}