using UnityEngine;
using System.Collections;

public class WeaponLaser : Weapon {

	[Header("Laser Settings")]
	public LayerMask geometryLayer;
	public float laserLength = 7f;

	public LineRenderer line;

	[Header("EXTENDED")]
	[SerializeField] Switch s;
	[SerializeField] Entity e;
	[SerializeField] DoorProjectile d;

	protected override void Start () {

		base.Start ();

		line = GetComponent<LineRenderer> ();

		if (line == null) {

			line = GameObject.FindGameObjectWithTag ("LaserLineRenderer").GetComponent<LineRenderer> ();
			line.sortingLayerName = "Geometry";
			line.sortingOrder = -1;

			Debug.Log (line);

		}

	}

	protected override void Update () {

		base.Update ();

	}
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		base.Shoot (ShootLocationPosition);

		if (Input.GetMouseButton (0)) {

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

				// TODO: Fix me
				if ((e = hit.collider.gameObject.GetComponentInParent<Entity> ()) != null && (Time.time > AttackLength)) {

					if (e.tag != "Geometry") {

						e.DamageHealth (Damage);

					}

				}

				if ((s = hit.collider.gameObject.GetComponentInParent<Switch> ()) != null) {

					s.TriggerSwitch ();

				}

				if ((d = hit.collider.gameObject.GetComponent<DoorProjectile> ()) != null) {

					if (Player.Current.CurrentWeapon.Level >= d.doorLevel && d.IsDoorOpen() == false) {

						d.OpenDoor ();

					}

				}

			} else {

				line.SetVertexCount (2);

				line.SetPosition (1, (Vector3) (ShootLocationPosition + (Vector3) (directionToMousePositionInWorld.normalized * laserLength)));

			}

			if (Time.time > AttackLength) {
			
				ShootEnd ();

			}

		} else {

		line.enabled = false;
		Player.Current.CanChangeWeapon = true;

		}

	}

}
