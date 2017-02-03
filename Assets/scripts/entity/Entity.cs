using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	// System
	protected float iFramesRemoveTime, nextTickTime;

	public string EntityNameLocalisationID = "Localisation <FIXME>";
	public string EntityDescLocalisationID = "Localisation <FIXME>";

	[Header("Health")]
	public int		Health_Current = 3;
	public int 		Health_Max = 3;
	public bool  	Health_RegenOn = false;
	public float 	Health_RegenCooldown = 10f;
	public bool		HEALTH_INFINITE = false;

	[SerializeField] protected bool dead = false;

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

	protected virtual void Update() {

		if (!dead && Health_Current < 1) {
			
			dead = true;
			Debug.Log (this + " has hit 0 health and has been removed. If it was an Enemy then it should spawn Health & Bombs pickups.");

			Die ();

		}

		if (MoveSpeed > MaximumMoveSpeed) {

			MoveSpeed = MaximumMoveSpeed;

		}

		if (hasInvincibilityTime && isCurrentlyInInvulnerabilityTime) {

			if (Time.time > iFramesRemoveTime) {

				isCurrentlyInInvulnerabilityTime = false;
				iFramesRemoveTime = 0f;

			}

		}

	}

	public virtual void Die() {

		Destroy (gameObject);

	}

	#region Variable Functions

	public virtual void RestoreHealth(int restore) {

		Health_Current += restore;

	}

	public virtual void RestoreHealthFully() {

		Health_Current = Health_Max;

	}

	public virtual void DamageHealth(int damage) {

		if (isCurrentlyInInvulnerabilityTime || HEALTH_INFINITE) {

			return;

		}

		Debug.Log ("Hitting " + this + " with damage: " + damage);

		Health_Current -= damage;

		if (hasInvincibilityTime) {

			isCurrentlyInInvulnerabilityTime = true;

			iFramesRemoveTime = Time.time + invincibilityTimeLength;

		}

	}

	#endregion
}