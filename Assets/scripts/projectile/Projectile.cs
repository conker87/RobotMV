using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float	movementSpeed = 1f;
	public  int		weaponLevel;
	public string	sourceWeapon = "";

	public bool		doNotDestroyGeometry = false;

	public bool 	doNotMoveForward = false;

	public ProjectileType projectileType;

	public float projectileDamage;

	public Vector3 Direction;

	void Update () {

		// Checks to see if Direction is Normalized() by simply checking to see if itself is normalized, one cannot normalize a direction
		// to 1 again.
		if (Direction != Direction.normalized) {

			Direction.Normalize();

		}

		if (!doNotMoveForward) {
			
			transform.position += Direction * Time.deltaTime * movementSpeed;

		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if ((other.gameObject.tag == "Player" && projectileType == ProjectileType.PLAYER) ||
				(other.gameObject.tag == "Enemy" && projectileType == ProjectileType.ENEMY) || 
				other.gameObject.tag == "IgnoreCollision") {
			
			return;

		}

		if (other.gameObject.tag == "Geometry" && !doNotDestroyGeometry) {

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