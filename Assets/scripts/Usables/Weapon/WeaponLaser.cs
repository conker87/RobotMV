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
		
	public override void ShootMouse(Vector3 ShootLocationPosition) {

		if (Input.GetMouseButtonDown (0)) {

			attackLengthTime = Time.time + AttackLength;

		}

		// Debug.Log ("Time.time: " + Time.time + " attackLengthTime: " + attackLengthTime);

		if (Time.time < attackLengthTime) {

			if (Input.GetMouseButton (0)) {

				Player.Current.CanChangeWeapon = false;
				hasBeenFiring = true;

				mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

				RaycastHit2D hit = Physics2D.Raycast (ShootLocationPosition, directionToMousePositionInWorld, laserLength, geometryLayer);

				Debug.DrawRay (ShootLocationPosition, directionToMousePositionInWorld);

				line.enabled = true;
				line.sortingLayerName = "Projectiles";
				line.SetPosition (0, ShootLocationPosition);

				if (hit.collider != null) {

					line.numPositions = 3;

					line.SetPosition (1, hit.point);
					line.SetPosition (2, hit.point + (directionToMousePositionInWorld.normalized * .1f));

					// TODO: Fix me
					if ((e = hit.collider.gameObject.GetComponentInParent<Entity> ()) != null) {

						if (e.tag != "Geometry") {

							e.DamageVital ("HEALTH", Damage);

							// e.DamageHealth (Damage);

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

					line.SetPosition (1, (Vector3)(ShootLocationPosition + (Vector3)(directionToMousePositionInWorld.normalized * laserLength)));

				}

			} else {

				// Debug.Log ("hasBeenFiring: " + hasBeenFiring);

				if (hasBeenFiring) {

					line.enabled = false;
					Player.Current.CanChangeWeapon = true;

					// Prevent firing again until after cooldown time
					cooldownTime = Time.time + Cooldown;

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
				cooldownTime = Time.time + Cooldown;

			}

		}



	}

}
