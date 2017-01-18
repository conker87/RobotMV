using UnityEngine;
using System.Collections;

public class ProjectileEnergyShield : Projectile {

	protected override void Update ()
	{

		transform.position = Player.Current.transform.position;

	}

	protected override void OnDeath ()
	{
		
		base.OnDeath ();

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		Projectile p;

		if ((p = other.gameObject.GetComponentInParent<Projectile> ()) != null) {

			if (p.ProjectileType == ProjectileType.ENEMY) {



			}

		}

	}

}