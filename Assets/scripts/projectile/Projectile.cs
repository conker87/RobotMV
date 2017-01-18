using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// TODO: Allow the projectile to have options of going through enemies a certain number of times.

	public string	ProjectileNameLocalisationID = "";
	public float	movementSpeed = 1f;
	public bool 	destroyOnHit = true, ignoreGeometry = false;
	public bool		destroyInOn = true;
	public float	destroyIn = 3f;

	public bool projectileRotatesToDirection = false;

	public int	weaponLevel;

	public Vector3 Direction;
	public int projectileDamage;
	public ProjectileType projectileType;

	public GameObject onDeathObjectSpawn;

	protected virtual void Start () { 

		if (Direction != Vector3.zero && Direction != Direction.normalized) {

			Direction.Normalize();

		}

		if (destroyInOn) {
			Invoke ("DestroyGameObject", destroyIn);
		}

	}

	protected virtual void Update () {

		transform.position += Direction * Time.deltaTime * movementSpeed;

		if (projectileRotatesToDirection && Direction != Vector3.zero) {
			
			float angle = Mathf.Atan2 (Direction.x, -Direction.y) * Mathf.Rad2Deg + 180;
				Vector3 euler = transform.eulerAngles;
				euler.z = Mathf.LerpAngle (euler.z, angle, Time.deltaTime * 90f);
			transform.eulerAngles = euler;

		}

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		if ((other.gameObject.tag == "Player" && projectileType == ProjectileType.PLAYER) ||
				(other.gameObject.tag == "Enemy" && projectileType == ProjectileType.ENEMY) || 
				other.gameObject.tag == "IgnoreCollision") {
			
			return;

		}

		if (other.gameObject.tag == "Projectile") {

			return;

		}

		if (other.gameObject.tag == "Geometry") {

			if (!ignoreGeometry) {
				
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

			if (destroyOnHit) {

				OnDeath ();
				Destroy (gameObject);

			}

			if (e.tag == "Geometry" && gameObject.tag != "DestroyGeometry") {

				return;

			}

			e.DamageVital ("HEALTH", projectileDamage);

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