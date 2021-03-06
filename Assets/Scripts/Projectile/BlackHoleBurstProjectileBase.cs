﻿using UnityEngine;
using System.Collections;

// Refactored 03/02/2017
public class BlackHoleBurstProjectileBase : ProjectileBase {

	[Header("Burst Settings")]
	public ProjectileBase[] bursts;
	[Range(0, 10)]
	public float minimumTimeTilNextBurst = 0f, maximumTimeTilNextBurst = 10f;
	[Range(1, 10)]
	public float minimumBurstMovementSpeed = 0f, maximumBurstMovementSpeed = 10f;
	[Range(1, 100)]
	public float chanceToSpawnNextBurst = 75f;

	float nextBurstTime;
	int random;

	protected override void Update () {

		if (PauseManager.Current == null ||PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}
		
		base.Update ();

		if (Time.time > nextBurstTime && Random.value < (chanceToSpawnNextBurst / 100f)) {

			random = Random.Range (0, bursts.Length - 1);

			BlackHoleBurstProjectile projectile = Instantiate (bursts [random], transform.position, Quaternion.identity, transform) as BlackHoleBurstProjectile; 

			Vector2 randomNormalisedDirection = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f));

			projectile.SetSettings (randomNormalisedDirection, Random.Range (minimumBurstMovementSpeed, maximumBurstMovementSpeed), false, ProjectileType, ProjectileDamage);

			nextBurstTime = Time.time + Random.Range (minimumTimeTilNextBurst, maximumTimeTilNextBurst);

		}

	}

}