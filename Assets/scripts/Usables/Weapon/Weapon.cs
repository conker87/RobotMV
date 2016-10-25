using UnityEngine;
using System.Collections;

public abstract class Weapon : Usables {
	
	[Header("_DEBUG_")]
	[SerializeField]
	protected float 	nextShotTime = 0f;

	public virtual void Shoot (Vector3 ShootLocationPosition) {

	}

	public void ShootEnd(float energyCost) {

		if (Time.time > nextShotTime) {
			
			Player.Current.DamageEnergy(energyCost);

			nextShotTime = Time.time + AttackSpeed;

		}

	}

}

public enum ProjectileType { PLAYER, ENEMY };