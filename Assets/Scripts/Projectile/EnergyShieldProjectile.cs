using UnityEngine;
using System.Collections;

// Refactored 03/02/2017
public class EnergyShieldProjectile : ProjectileBase {

	public ItemEnergyShield itemEnergyShield;

	protected override void Start() {

		base.Start ();

		itemEnergyShield = GameObject.FindObjectOfType<ItemEnergyShield> ();

	}

}