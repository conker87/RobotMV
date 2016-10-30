using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Usables : MonoBehaviour {

	[Header("Usable Settings")]
	public string		UsableName = "Usable <FIXME>";
	public float		AttackSpeed;
	public float		EnergyCost;
	public float 		DamagePerTick;
	public int			Level;
	[SerializeField]
	float damagePerEnergyCost, damagePerSecond;

	[Header("Spawn settings")]
	public Projectile[]	Projectiles;
	public ProjectileType projectileType;

	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	public virtual void Awake () {



	}

	public virtual void Start () {


	}

	[ExecuteInEditMode]
	public virtual void Update() {

		DoDamageStats ();

	}
		
	void DoDamageStats() {

		damagePerEnergyCost	= DamagePerTick / EnergyCost;
		damagePerSecond		= (1f / AttackSpeed) / DamagePerTick;

	}
}
