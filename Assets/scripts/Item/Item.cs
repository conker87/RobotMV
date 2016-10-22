using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

	[Header("Weapon Settings")]
	public string		ItemName = "";
	public float		AttackSpeed = 0.5f;
	public float 		Cooldown = 2f;
	public float		EnergyCost = 10f;
	public float 		damagePerTick = 2f;
	public int			itemLevel;

	[Header("Projectile settings")]
	public GameObject	Projectile;

	[Header("_DEBUG_")]
	[SerializeField] protected float 	nextShotTime = 0f;

	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	// Use this for initialization
	public virtual void Awake () {



	}

	public virtual void Start () {



	}

	public virtual void Use () {

		Player.Current.DamageEnergy(EnergyCost);

	}

}