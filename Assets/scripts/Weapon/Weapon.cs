using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	[Header("Weapon Settings")]
	public string		WeaponName = "";
	public float		AttackSpeed = 0.05f;
	public float		EnergyCost = 10f;
	public float 		damagePerTick = 2f;
	public int			weaponLevel;

	[Header("Projectile settings")]
	public GameObject	Projectile;
	public ProjectileType projectileType;

	[Header("_DEBUG_")]
	[SerializeField] protected float 	nextShotTime = 0f;


	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	// Use this for initialization
	public virtual void Awake () {

		nextShotTime = 0f;

	}

	public virtual void Start () {



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