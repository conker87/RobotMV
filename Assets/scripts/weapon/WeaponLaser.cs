using UnityEngine;
using System.Collections;

public class WeaponLaser : Weapon {

	public LayerMask geometryLayer;
	LineRenderer line;

	public float laserLength = 7f;

	public override void Awake() {

		base.Awake ();

		line = GetComponent<LineRenderer> ();
		line.sortingLayerName = "Geometry";

	}
		
	public override void Shoot(Vector3 ShootLocationPosition) {

		if (Input.GetMouseButton (0) && Player.Current.Energy > EnergyCost) {

			line.enabled = true;

			mousePositionToWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			directionToMousePositionInWorld = mousePositionToWorld - (Vector2) ShootLocationPosition;

			RaycastHit2D hit = Physics2D.Raycast (ShootLocationPosition, directionToMousePositionInWorld, laserLength, geometryLayer);

			Debug.DrawRay (ShootLocationPosition, directionToMousePositionInWorld);

			line.SetPosition (0, ShootLocationPosition);

			if (hit.collider != null) {

				line.SetVertexCount (3);

				line.SetPosition (1, hit.point);
				line.SetPosition (2, hit.point + (directionToMousePositionInWorld.normalized * .1f));

			} else {

				line.SetVertexCount (2);

				line.SetPosition (1, (Vector3) (ShootLocationPosition + (Vector3) (directionToMousePositionInWorld.normalized * laserLength)));

			}


			if (Time.time > nextShotTime) {

				ShootEnd ();


			}

		} else {

			line.enabled = false;

		}

	}

	public override void ShootAfter() {

		if (!Input.GetMouseButton (0)) {

			line.enabled = false;

		}

	}

}
