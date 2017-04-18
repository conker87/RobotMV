using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableWall : MonoBehaviour {

	public int wallLevel = 0;

	void OnTriggerEnter2D(Collider2D other) {

		ProjectileBase p;

		if ((p = other.GetComponentInParent<ProjectileBase> ()) != null && 
				p.ProjectileType == ProjectileType.PLAYER &&
					p.WeaponLevel >= wallLevel) {

				gameObject.SetActive (false);

		}

	}

}