using UnityEngine;
using System.Collections;

public abstract class Weapon : Usables {

	protected float nextShotTime = 0f;

	public virtual void Shoot (Vector3 ShootLocationPosition) {



	}

	public virtual void ShootEnd(float energyCost) {
			
		Player.Current.DamageEnergy(energyCost);

		nextShotTime = Time.time + AttackSpeed;

	}

}

public enum ProjectileType { PLAYER, ENEMY };