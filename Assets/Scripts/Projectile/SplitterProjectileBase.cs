using UnityEngine;
using System.Collections;

// Created 03/02/2017
public class SplitterProjectileBase : ProjectileBase {

	SplitterProjectile[] splitterProjectiles;

	int nullSplitterProjectiles = 0;

	protected override void Start () {

		base.Start ();

		splitterProjectiles = GetComponentsInChildren<SplitterProjectile> ();

	}

	protected override void Update () {
		
		base.Update ();

		nullSplitterProjectiles = splitterProjectiles.Length;

		foreach (SplitterProjectile p in splitterProjectiles) {

			if (p == null) {

				nullSplitterProjectiles--;

			}

		}

		if (nullSplitterProjectiles < 1) {

			Destroy (gameObject);

		}

	}

}