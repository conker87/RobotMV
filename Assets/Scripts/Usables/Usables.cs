using UnityEngine;
using System.Collections;
using SmartLocalization;

[ExecuteInEditMode]
public class Usables : MonoBehaviour {

	[Header("Usable Settings")]
	public string		UsableNameLocalisationID = "LocalisationID <FIXME>";
	public string		DescriptionLocalisationID = "LocalisationID <FIXME>";

	public float		AttackLength, Cooldown;
	public int 			Damage;
	public int			Level;
	public int 			Charges;

	protected float cooldownTime;
	protected bool stillCoolingDown = false;

	[Header("Spawn settings")]
	public Projectile[]	Projectiles;
	public ProjectileType projectileType;

	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	protected virtual void Awake () {



	}

	protected virtual void Start () {

		cooldownTime = 0f;

	}

	[ExecuteInEditMode]
	protected virtual void Update() {

		stillCoolingDown = true;

		if (Time.time > cooldownTime) {

			stillCoolingDown = false;

		}
			

	}

}
