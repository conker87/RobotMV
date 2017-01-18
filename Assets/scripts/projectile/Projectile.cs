using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// TODO: Allow the projectile to have options of going through enemies a certain number of times.

	[Header("Projectile Localisation ID")]
	public string			ProjectileNameLocalisationID = "";

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

	[Header("Projectile Other Settings")]
	public GameObject onDeathObjectSpawn;

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

		if (DestroyInSecondsOn) {
			Invoke ("DestroyGameObject", DestroyInSeconds);
		}

	}

	protected virtual void Update () {

		if (MovementSpeed > 0) {
			
			transform.position += Direction * Time.deltaTime * MovementSpeed;

		}

		if (ProjectileRotatesToDirection && Direction != Vector3.zero) {
			
			float angle = Mathf.Atan2 (Direction.x, -Direction.y) * Mathf.Rad2Deg + 180;
			Vector3 euler = transform.eulerAngles;
			euler.z = Mathf.LerpAngle (euler.z, angle, Time.deltaTime * 90f);
			transform.eulerAngles = euler;

		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		if ((other.gameObject.tag == "Player" && ProjectileType == ProjectileType.PLAYER) ||
				(other.gameObject.tag == "Enemy" && ProjectileType == ProjectileType.ENEMY) || 
				other.gameObject.tag == "IgnoreCollision") {
			
			return;

		}

		if (other.gameObject.tag == "Projectile") {

			return;

		}

		if (other.gameObject.tag == "Geometry") {

			if (!IgnoreGeometry) {
				
				OnDeath ();
				Destroy (gameObject);

				return;

			}

		}

		if (other.GetComponent<ProjectileEnergyShield> () != null) {

			//OnDeath ();
			//Destroy (gameObject);

		}

		Entity e;

		if ((e = other.GetComponentInParent<Entity> ()) != null) {

			if (DestroyOnHit) {

				OnDeath ();
				Destroy (gameObject);

			}

			if (e.tag == "Geometry" && gameObject.tag != "DestroyGeometry") {

				return;

			}

			e.DamageVital ("HEALTH", ProjectileDamage);

			// e.DamageHealth(projectileDamage);

		}

	}

	protected virtual void DestroyGameObject() {

		OnDeath ();
		Destroy(gameObject);

	}

	protected virtual void OnDeath() {

		if (onDeathObjectSpawn != null) {
			
			Instantiate (onDeathObjectSpawn, transform.position, Quaternion.identity);

		}

	}

}