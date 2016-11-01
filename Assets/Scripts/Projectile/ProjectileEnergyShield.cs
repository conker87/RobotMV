using UnityEngine;
using System.Collections;

public class ProjectileEnergyShield : Projectile {

	protected override void Update ()
	{

		transform.position = Player.Current.transform.position;

		Player.Current.EnergyRegenOn = false;

	}

	protected override void OnDeath ()
	{
		
		base.OnDeath ();

		Player.Current.EnergyRegenOn = true;

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		Projectile p;

		if ((p = other.gameObject.GetComponentInParent<Projectile> ()) != null) {

			if (p.projectileType == ProjectileType.ENEMY) {

				Player.Current.DamageEnergy (p.projectileDamage);

			}

		}

	}   

	void OnDisable() {

		Player.Current.EnergyRegenOn = true;

	}

}