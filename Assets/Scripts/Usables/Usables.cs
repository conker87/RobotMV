using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Usables : MonoBehaviour {

	[Header("Usable Settings")]
	public string		UsableNameLocalisationID = "LocalisationID <FIXME>";
	public string		DescriptionLocalisationID = "LocalisationID <FIXME>";

	public float		AttackLength, Cooldown;
	public int 			Damage;
	public int			Level;
	public int 			Charges;

	[SerializeField]
	protected float cooldownTime, attackLengthTime;
	protected bool stillCoolingDown = false;

	[Header("Spawn settings")]
	public Projectile[]	Projectiles;
	public ProjectileType projectileType;

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