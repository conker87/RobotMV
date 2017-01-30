using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Usables : MonoBehaviour {

	[Header("Usable Localisation ID")]
	public string		CollectableID = "FIXME";

	public string		UsableNameLocalisationID = "LocalisationID <FIXME>";
	public string		DescriptionLocalisationID = "LocalisationID <FIXME>";

	[Header("Usable Attack Settings")]
	public float		InitialAttackLength, CurrentAttackLength;
	public int 			InitialDamage, CurrentDamage;
	public int			Level;

	[Header("Usable Cooldown Settings")]
	public float		InitialCooldown, CurrentCooldown;

	[Header("Usable Movement Settings")]

	// Cooldown & Attack Length Time.time vars.
	protected float cooldownTime, attackLengthTime;
	protected bool stillCoolingDown = false;

	[Header("Spawn settings")]
	public float 		InitialProjectileMovementSpeed;
	public Projectile[]	Projectiles;
	public ProjectileType projectileType;

	// Usable Direction for Projeciles.
	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	protected virtual void Awake () {



	}

	protected virtual void Start () {

		attackLengthTime = 0f;
		cooldownTime = 0f;

	}

	protected virtual void Update() {

		stillCoolingDown = true;

		if (Time.time > cooldownTime) {

			stillCoolingDown = false;

		}
			

	}

}

public enum ProjectileType { PLAYER, ENEMY };