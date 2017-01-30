using UnityEngine;
using System.Collections;

public class WeaponLaser : Weapon {

	// TODO: Sort out an animated and wobbly rendered line shader.

	[Header("Laser Settings")]
	public LayerMask geometryLayer;
	public float laserLength = 7f;

	public LineRenderer line;

	bool hasBeenFiring = false;

	[Header("EXTENDED")]
	[SerializeField] Switch s;
	[SerializeField] Entity e;
	[SerializeField] DoorProjectile d;

	protected override void Start () {

		base.Start ();

		line = GetComponent<LineRenderer> ();
		line.sortingLayerName = "Geometry";
		line.sortingOrder = -1;

	}
		
	public override void ShootMouse(Vector3 ShootLocationPosition, Vector2 Direction) {

		if (!Player.Current.Weapon_Laser) {

			return;

		}


		if (InputManager.Current.GetButtonDown("Fire Weapon")) {

			attackLengthTime = Time.time + InitialAttackLength;

		}

		// Debug.Log ("Time.time: " + Time.time + " attackLengthTime: " + attackLengthTime);

		if (Time.time < attackLengthTime) {

			if (InputManager.Current.GetButton("Fire Weapon")) {

				Player.Current.CanChangeWeapon = false;
				hasBeenFiring = true;

				RaycastHit2D hit = Physics2D.Raycast (ShootLocationPosition, Direction, laserLength, geometryLayer);

				line.enabled = true;
				line.sortingLayerName = "Projectiles";
				line.SetPosition (0, ShootLocationPosition);

				if (hit.collider != null) {

					line.numPositions = 3;

					line.SetPosition (1, hit.point);
					line.SetPosition (2, hit.point + (Direction.normalized * .1f));

					if ((e = hit.collider.gameObject.GetComponentInParent<Entity> ()) != null) {

						if (e.tag != "Geometry") {

							// TODO: There hould be a "tick" timer that deals damage a set number of times over
								// the attack length. The attack length will then be reduced via power ups
								// this effectively inccreases DPS by reducing cast time.
								// TODO: damage, speed and cooldown need power ups too.
							e.DamageHealth(InitialDamage);

						}

					}

					if ((s = hit.collider.gameObject.GetComponentInParent<Switch> ()) != null) {

						s.TriggerSwitch ();

					}

					if ((d = hit.collider.gameObject.GetComponent<DoorProjectile> ()) != null) {

						if (Player.Current.CurrentWeapon.Level >= d.doorLevel && d.IsDoorOpen () == false) {

							d.OpenDoor ();

						}

					}

				} else {

					line.numPositions = 2;

					line.SetPosition (1, (Vector3)(ShootLocationPosition + (Vector3)(Direction.normalized * laserLength)));

				}

			} else {

				// Debug.Log ("hasBeenFiring: " + hasBeenFiring);

				if (hasBeenFiring) {

					line.enabled = false;
					Player.Current.CanChangeWeapon = true;

					// Prevent firing again until after cooldown time
					cooldownTime = Time.time + InitialCooldown;

					hasBeenFiring = false;

				}

			}

		} else {

			if (hasBeenFiring) {

				// Debug.Log ("Time.time > attackLengthTime");

				line.enabled = false;
				Player.Current.CanChangeWeapon = true;
				hasBeenFiring = false;

				// Prevent firing again until after cooldown time
				cooldownTime = Time.time + InitialCooldown;

			}

		}



	}

}
