using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	public GameObject	Projectile;
	public string		WeaponName = "";
	public float		AttackSpeed = 0.05f;	// This is attack per second
	public ProjectileType projectileType;
	public int			weaponLevel;

	public float		EnergyCost = 10f;

	public float 		nextShotTime = 0f;

	public float 		damagePerTick = 2f;

	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	// Use this for initialization
	public virtual void Awake () {

		nextShotTime = 0f;

	}

	public virtual void Shoot (Vector3 ShootLocationPosition) {

	}

	public void ShootEnd() {

		if (Time.time > nextShotTime) {
			
			if (!Player.Current._DEBUG_INFINITE_ENERGY) {
				Player.Current.Energy -= EnergyCost;
			}
			nextShotTime = Time.time + AttackSpeed;

		}

	}

}

public enum ProjectileType { PLAYER, ENEMY };