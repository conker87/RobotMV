using UnityEngine;
using System.Collections;
using SmartLocalization;

[ExecuteInEditMode]
public class Usables : MonoBehaviour {

	[Header("Usable Settings")]
	public string		UsableName = "Usable <FIXME>";

	[TextArea(1,10)]
	public string		Description = "Desc <FIXME>";
	public float		AttackSpeed;
	public float		EnergyCost, MinimumEnergyRequired;
	public float 		DamagePerTick;
	public int			Level;
	[SerializeField]
	float damagePerEnergyCost, damagePerSecond, energyUsePerSecond;

	[Header("Spawn settings")]
	public Projectile[]	Projectiles;
	public ProjectileType projectileType;

	protected Vector2 mousePositionToWorld, directionToMousePositionInWorld;

	protected virtual void Awake () {



	}

	protected virtual void Start () {


	}

	[ExecuteInEditMode]
	protected virtual void Update() {

		DoDamageStats ();

}
		
	void DoDamageStats() {

		damagePerEnergyCost	= DamagePerTick / EnergyCost;
		damagePerSecond		= (1f / AttackSpeed) / DamagePerTick;
		energyUsePerSecond	= (1f / AttackSpeed) - (0.2f / Constants.ResourceTick);

	}
}
