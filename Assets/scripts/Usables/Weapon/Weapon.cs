using UnityEngine;
using System.Collections;

public abstract class Weapon : Usables {

	protected float nextShotTime = 0f;
	protected float penaltyForHavingNoEnergyInSeconds = 1f, penaltyTime;

	protected bool disabledDueToPenalty = false;

	public virtual void Shoot() {

		if (Player.Current.Energy < MinimumEnergyRequired) {

			disabledDueToPenalty = true;
			penaltyTime = Time.time + penaltyForHavingNoEnergyInSeconds;

		}

		if (Time.time > penaltyTime) {

			disabledDueToPenalty = false;

		}

	}

	public virtual void Shoot (Vector3 ShootLocationPosition) {

		Shoot ();

	}

	public virtual void ShootEnd(float energyCost) {
			
		Player.Current.DamageEnergy(energyCost);

		nextShotTime = Time.time + AttackSpeed;

	}

}

public enum ProjectileType { PLAYER, ENEMY };