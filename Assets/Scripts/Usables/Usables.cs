using UnityEngine;
using System.Collections;

public class Usables : MonoBehaviour {

	[Header("Usable Settings")]
	public string		UsableName = "";
	public float		AttackSpeed = 0.5f;
	public float		EnergyCost = 10f;
	public float 		DamagePerTick = 2f;
	public int			Level;

	[Header("Spawn settings")]
	public Projectile	Projectile;
	public ProjectileType projectileType;

	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	public virtual void Awake () {



	}

	public virtual void Start () {



	}

	public virtual void Use () {



	}
}
