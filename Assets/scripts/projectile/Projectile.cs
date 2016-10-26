using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public string	ProjectileName = "";
	public float	movementSpeed = 1f;
	public bool 	destroyOnHit = true, ignoreGeometry = false;
	public float	destroyIn = 3f;

	public int	weaponLevel;

	public Vector3 Direction;
	public float projectileDamage;
	public ProjectileType projectileType;

	public GameObject onDeathObjectSpawn;

	protected virtual void Start () { 

		if (Direction != Direction.normalized) {

			Direction.Normalize();

		}
			
		Invoke ("DestroyGameObject", destroyIn);

	}

	protected virtual void Update () {

		transform.position += Direction * Time.deltaTime * movementSpeed;

	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {

		if ((other.gameObject.tag == "Player" && projectileType == ProjectileType.PLAYER) ||
				(other.gameObject.tag == "Enemy" && projectileType == ProjectileType.ENEMY) || 
				other.gameObject.tag == "IgnoreCollision") {
			
			return;

		}

		if (other.gameObject.tag == "Geometry") {

			if (!ignoreGeometry) {
				
				OnDeath ();
				Destroy (gameObject);

				return;

			}

		}

		Entity e;

		if ((e = other.gameObject.GetComponentInParent<Entity> ()) != null) {

			e.DamageHealth(projectileDamage);

			if (destroyOnHit) {
				
				OnDeath ();
				Destroy (gameObject);

			}

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