using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EckTechGames.FloatingCombatText;

public class Entity : MonoBehaviour {

	// System
	protected float iFramesRemoveTime, nextTickTime;

	public string EntityNameLocalisationID = "Localisation <FIXME>";
	public string EntityDescLocalisationID = "Localisation <FIXME>";

	[Header("Entity Cheats")]
	public bool		CHEAT_HEALTH_INFINITE = false;

	[Header("Health")]
	public int		HealthCurrent = 3;
	public int 		HealthMax = 3;
	public bool  	HealthRegen = false;
	public float 	HealthRegenCooldown = 10f;

	[SerializeField] protected bool dead = false;

	[Header("iFrames")]
	public bool  HasInvincibilityFrames = false;
	public float InvincibilityFramesLength = 0f;
	public bool	 IsCurrentlyInInvincibilityFrames = false;

	[Header("Movement")]
	public float MoveSpeed = 3f;
	public float MaximumMoveSpeed = 3f;

	[Header("Currently Equipped Items")]
	public Item CurrentItem = null;
	public Weapon CurrentWeapon = null;

	protected virtual void Update() {

		if (!dead && HealthCurrent < 1) {
			
			dead = true;
			Debug.Log (this + " has hit 0 health and has been removed. If it was an Enemy then it should spawn Health & Bombs pickups.");

			Die ();

		}

		if (MoveSpeed > MaximumMoveSpeed) {

			MoveSpeed = MaximumMoveSpeed;

		}

		if (HealthCurrent > HealthMax) {

			HealthCurrent = HealthMax;

		}

		if (HasInvincibilityFrames && IsCurrentlyInInvincibilityFrames) {

			if (Time.time > iFramesRemoveTime) {

				IsCurrentlyInInvincibilityFrames = false;
				iFramesRemoveTime = 0f;

			}

		}

	}

	public virtual void Die() {

		Destroy (gameObject);

	}

	#region Variable Functions

	public virtual void RestoreHealth(int restoreHealthValue) {

		HealthCurrent += restoreHealthValue;

	}

	public virtual void RestoreHealthFully() {

		HealthCurrent = HealthMax;

	}

	public virtual void DamageHealth(int damageHealthValue) {

		if (IsCurrentlyInInvincibilityFrames || CHEAT_HEALTH_INFINITE) {

			return;

		}

		Debug.Log ("Entity -- Hitting " + this + " with damage: " + damageHealthValue);

		HealthCurrent -= damageHealthValue;

		if (HasInvincibilityFrames) {

			IsCurrentlyInInvincibilityFrames = true;

			iFramesRemoveTime = Time.time + InvincibilityFramesLength;

		}

	}

	#endregion
}