using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableWall : MonoBehaviour {

	public int wallLevel = 0;
	public GameObject wallObject;

	void OnTriggerEnter2D(Collider2D other) {

		ProjectileBase p;

		if ((p = other.GetComponentInParent<ProjectileBase> ()) != null) {

			if (p.ProjectileType == ProjectileType.PLAYER && p.WeaponLevel >= wallLevel) {

				wallObject.SetActive (false);
				GetComponent<Collider2D> ().enabled = false;

			}

		}

	}

}
