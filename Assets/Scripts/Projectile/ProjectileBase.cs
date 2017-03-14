using UnityEngine;
using System.Collections;

// Refactored 03/02/2017
public class ProjectileBase : MonoBehaviour {

	// TODO: Allow the projectile to have options of going through enemies a certain number of times.

	[Header("Projectile Localisation ID")]
	public string			ProjectileNameLocalisationID = "FIXME";

	[Header("Projectile Movement Settings")]
	public Vector3			Direction;
	public float			MovementSpeed = 1f;
	public bool				ProjectileRotatesToDirection;

	[Header("Projectile Damage Settings")]
	public ProjectileType	ProjectileType;
	public int				ProjectileDamage;
	public int				WeaponLevel;

	[Header("Projectile Other Settings")]
	public bool				IgnoreGeometry = false;
	public bool 			DestroyOnHit = true;
	public bool				DestroyInSecondsOn = true;
	public float			DestroyInSeconds = 3f;

	[Header("Projectile Hit Animation")]
	[Tooltip("The GameObject that is spawned at the hit.point of the collider that it is destroyed on.\n\nThis is used to add a '\"bullet hole\" type object.")]
	public GameObject		ProjectileHitAnimation;

	protected Entity e;

	public void SetSettings(Vector2 direction, float movementSpeed = 3f, bool projectileRotatesToDirection = false, ProjectileType projectileType = ProjectileType.PLAYER, int projectileDamage = 1, int weaponLevel = 0,
		bool ignoreGeometry = false, bool destroyOnHit = true, bool destroyInSecondsOn = true, float destroyInSeconds = 3f) {

		Direction =						direction;
		MovementSpeed = 				movementSpeed;
		ProjectileRotatesToDirection = 	projectileRotatesToDirection;
		ProjectileType = 				projectileType;
		ProjectileDamage = 				projectileDamage;
		WeaponLevel = 					weaponLevel;
		IgnoreGeometry = 				ignoreGeometry;
		DestroyOnHit = 					destroyOnHit;
		DestroyInSecondsOn = 			destroyInSecondsOn;
		DestroyInSeconds = 				destroyInSeconds;

	}

	protected virtual void Start () { 

		if (Direction != Vector3.zero && Direction != Direction.normalized) {

			Direction.Normalize();

		}

		if (ProjectileRotatesToDirection && Direction != Vector3.zero) {

			float angle = Mathf.Atan2 (Direction.x, -Direction.y) * Mathf.Rad2Deg + 180;
			Vector3 euler = transform.eulerAngles;
			euler.z = Mathf.LerpAngle (euler.z, angle, Time.deltaTime * 90f);
			transform.eulerAngles = euler;

		}

		if (DestroyInSecondsOn) {
			Invoke ("DestroyGameObject", DestroyInSeconds);
		}

	}

	protected virtual void Update () {

		if (PauseManager.Current == null || PauseManager.Current.checkIfCurrentlyPaused ()) {

			return;

		}

		if (MovementSpeed > 0) {
			
			transform.position += Direction * Time.fixedDeltaTime * MovementSpeed;

		}

	}

	// Refactored: 02/02/17
	protected virtual void OnTriggerEnter2D(Collider2D other) {

		// If the collider is an EnergyShield & the type is not PLAYER then destroy the projectile as it's an Enemy's.
		// TODO: Needs moving to the EnergyShieldProjectile class.
		if (other.GetComponent<EnergyShieldProjectile> () != null && ProjectileType != ProjectileType.PLAYER) {

			Die (true);
			return;

		}

		// If the projectile either: hits Player & is type PLAYER, hits an Enemy & is type ENEMY, hits a PowerUp,
		//	hits another Projectile or hits an object that is tagged appropriately then this is ignored.
		if (	(other.GetComponent<Player>() != null && ProjectileType == ProjectileType.PLAYER) ||
				(other.GetComponent<Enemy>() != null && ProjectileType == ProjectileType.ENEMY) ||
				(other.GetComponent<PowerUp>()) ||
				(other.GetComponent<ProjectileBase>()) ||
				other.gameObject.tag == "IgnoreCollision") {
			
			return;

		}
			
//		if ((e = other.GetComponentInParent<Entity> ()) != null) {
//			
//			if (e.tag == "Geometry" && gameObject.tag != "DestroyGeometry") {
//
//				return;
//
//			}
//
//			e.DamageHealth(ProjectileDamage);
//
//			if (DestroyOnHit) {
//
//				Die (true);
//
//			}
//
//			return;
//
//		}

		if (!IgnoreGeometry) {

			Die (true);

		}

	}

	protected virtual void DestroyGameObject() {

		Die (false);

	}

	protected virtual void DestroyGameObjectWithHitAnim() {

		Die (true);

	}

	protected virtual void Die() {

		Die (false);

	}

	protected virtual void Die(bool doHitAnimation = true) {

		if (doHitAnimation && ProjectileHitAnimation != null) {
			
			Instantiate (ProjectileHitAnimation, transform.position, Quaternion.identity);

		}

		Destroy(gameObject);

	}

}