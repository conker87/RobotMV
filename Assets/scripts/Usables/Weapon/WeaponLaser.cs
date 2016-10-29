using UnityEngine;
using System.Collections;

public class WeaponLaser : Weapon {

	[Header("Laser Settings")]
	public LayerMask geometryLayer;
	public float laserLength = 7f;

	public LineRenderer line;

	[Header("_DEBUG_EXTENDED")]
	[SerializeField] Switch s;
	[SerializeField] Entity e;
	[SerializeField] Door d;

	public override void Start () {

		base.Start ();

		line = GetComponent<LineRenderer> ();

		if (line == null) {

			line = GameObject.FindGameObjectWithTag ("LaserLineRenderer").GetComponent<LineRenderer> ();
			line.sortingLayerName = "Geometry";
			line.sortingOrder = -1;

			Debug.Log (line);

		}

	}
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		if (Player.Current.Laser) {

			if (Input.GetMouseButton (0) && ((Player.Current.EnergyTanks > 1) || (Player.Current.Energy >= EnergyCost))) {

				Player.Current.CanChangeWeapon = false;

				mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				directionToMousePositionInWorld = mousePositionToWorld - (Vector2)ShootLocationPosition;

				RaycastHit2D hit = Physics2D.Raycast (ShootLocationPosition, directionToMousePositionInWorld, laserLength, geometryLayer);

				Debug.DrawRay (ShootLocationPosition, directionToMousePositionInWorld);

				line.enabled = true;
				line.SetPosition (0, ShootLocationPosition);

				if (hit.collider != null) {

					line.SetVertexCount (3);

					line.SetPosition (1, hit.point);
					line.SetPosition (2, hit.point + (directionToMousePositionInWorld.normalized * .1f));

					if ((s = hit.collider.gameObject.GetComponentInParent<Switch> ()) != null) {

						s.TriggerSwitch ();

					}

					if ((e = hit.collider.gameObject.GetComponentInParent<Entity> ()) != null && (Time.time > nextShotTime)) {

						e.DamageHealth (DamagePerTick);

					}

					if ((d = hit.collider.gameObject.GetComponent<DoorProjectileFire> ()) != null) {

						if (Player.Current.CurrentWeapon.projectileType == ProjectileType.PLAYER) {

							if (Player.Current.CurrentWeapon.EnergyCost >= d.doorLevel && d.doorState == DoorState.CLOSED) {

								d.doorState = DoorState.OPEN_BEGIN;

							}

						}

					}

				} else {

					line.SetVertexCount (2);

					line.SetPosition (1, (Vector3)(ShootLocationPosition + (Vector3)(directionToMousePositionInWorld.normalized * laserLength)));

				}

				if (Time.time > nextShotTime) {
					
					ShootEnd (EnergyCost);

				}

			} else {

				line.enabled = false;
				Player.Current.CanChangeWeapon = true;

			}

		}

	}

}
