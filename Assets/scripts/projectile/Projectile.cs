using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float	movementSpeed = 1f;
	public  int		weaponLevel;
	public string	sourceWeapon = "";

	public ProjectileType projectileType;

	public float projectileDamage;

	public Vector3 Direction;

	protected virtual void Start () { 

		if (Direction != Direction.normalized) {

			Direction.Normalize();

		}

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

			Destroy (gameObject);

			return;

		}

		Entity e;

		if ((e = other.gameObject.GetComponentInParent<Entity> ()) != null) {

			Destroy (gameObject);
			e.DamageHealth(projectileDamage);

		}

	}

}