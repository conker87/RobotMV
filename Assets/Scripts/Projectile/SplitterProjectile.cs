using UnityEngine;
using System.Collections;

// Created 03/02/2017
public class SplitterProjectile : ProjectileBase {

	SplitterProjectileBase splitterBase;

	protected override void Start () {
		
		base.Start ();

		splitterBase = GetComponentInParent<SplitterProjectileBase> ();

		if (splitterBase == null) {

			Debug.LogWarning ("WARNING! " + this + " IS NOT CONNECTED TO A <SplitterProjectileBase> AND WILL FAIL.");
			return;

		}

		// Inherit some values from the Base.
		ProjectileDamage = splitterBase.ProjectileDamage;
		ProjectileHitAnimation = splitterBase.ProjectileHitAnimation;

	}

	new protected void Update() {

		splitterBase.MovementSpeed = MovementSpeed;

	}


}