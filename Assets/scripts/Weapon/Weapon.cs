using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	public GameObject	Projectile;
	public string		WeaponName = "";
	protected float		AttackSpeed = 0.05f;	// This is attack per second

	public float		EnergyCost = 10f;

	public float 		nextShotTime = 0f;

	// Use this for initialization
	public virtual void Awake () {

		nextShotTime = 0f;

	}

	public virtual void Shoot (GameObject shootLocation, Vector3 direction) {

	}

}
