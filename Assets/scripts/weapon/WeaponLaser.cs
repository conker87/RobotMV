using UnityEngine;
using System.Collections;

public class WeaponLaser : Weapon {

	public LayerMask geometryLayer;
	LineRenderer line;

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

			RaycastHit2D hit = Physics2D.Raycast (ShootLocationPosition, directionToMousePositionInWorld, Mathf.Infinity, geometryLayer);

			Debug.DrawRay (ShootLocationPosition, directionToMousePositionInWorld);

			if (hit.collider != null) {

				line.SetVertexCount (2);

				line.SetPosition (0, ShootLocationPosition);
				line.SetPosition (1, hit.point);

				//Debug.Log ("hit: " + hit.collider.gameObject.ToString() + ", at point: " + hit.point + ", layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

			}

			if (Time.time > nextShotTime) {

				ShootEnd ();


			}

		} else {

			line.enabled = false;

		}

	}

}
